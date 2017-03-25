using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Newtonsoft.Json;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Business.Models;
using Nidan.Data.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;


namespace Nidan.Business
{
    public partial class NidanBusinessService : INidanBusinessService, ITenantOrganisationService
    {
        private INidanDataService _nidanDataService;
        private ICacheProvider _cacheProvider;
        private ITemplateService _templateService;
        private IEmailService _emailService;
        //   private IDocumentServiceRestClient _documentServiceAPI;
        private enum ShowColour
        {
            Company = 1,
            Division,
            Department
        };

        private const string OrganisationCacheKey = "Organisations";
        private const string OrganisationEmploymentsTreeKey = "OrganisationEmploymentsTree";
        private const string AbsenceStatusTemplateKey = "HRAbsenceStatus";
        private object lockObject = new object();
        readonly string PersonnelPhotoKey = "PersonnelPhoto";
        readonly string PersonnelProfileCategory = "ProfileImage";
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month, DateTime.UtcNow.Date.Day, 0, 0, 0);

        public NidanBusinessService(INidanDataService nidanDataService, ICacheProvider cacheProvider,
            ITemplateService templateService, IEmailService emailService)
        {
            _nidanDataService = nidanDataService;
            _cacheProvider = cacheProvider;
            _templateService = templateService;
            _emailService = emailService;
            //_documentServiceAPI = documentServiceAPI;
        }

        #region // Create

        public Personnel CreatePersonnel(int organisationId, Personnel personnel)
        {
            return _nidanDataService.CreatePersonnel(organisationId, personnel);
        }

        public ValidationResult<AbsenceType> CreateAbsenceType(int organisationId, AbsenceType absenceType)
        {
            var validationResult = AbsenceTypeAlreadyExists(organisationId, null, absenceType.Name);
            if (!validationResult.Succeeded)
                return validationResult;

            try
            {
                validationResult.Entity = _nidanDataService.CreateAbsenceType(organisationId, absenceType);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public ValidationResult<AreaOfInterest> CreateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest)
        {
            var validationResult = AreaOfInterestAlreadyExists(organisationId, null, areaOfInterest.Name);
            if (!validationResult.Succeeded)
                return validationResult;

            try
            {
                validationResult.Entity = _nidanDataService.CreateAreaOfInterest(organisationId, areaOfInterest);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public Centre CreateCentre(int organisationId, Centre centre)
        {
            return _nidanDataService.CreateCentre(organisationId, centre);
        }

        public Batch CreateBatch(int organisationId, Batch batch)
        {
            return _nidanDataService.CreateBatch(organisationId, batch);
        }

        public Question CreateQuestion(int organisationId, Question question)
        {
            return _nidanDataService.CreateQuestion(organisationId, question);
        }

        public Mobilization CreateMobilization(int organisationId, Mobilization mobilization)
        {
            var data = _nidanDataService.Create<Mobilization>(organisationId, mobilization);
            var followUp = new FollowUp
            {
                CentreId = mobilization.CentreId,
                FollowUpDateTime = mobilization.FollowUpDate.Value,
                MobilizationId = data.MobilizationId,
                Remark = mobilization.Remark,
                Name = mobilization.Name,
                IntrestedCourseId = mobilization.InterestedCourseId,
                Mobile = mobilization.Mobile,
                AlternateMobile = mobilization.AlternateMobile,
                CreatedDateTime = DateTime.UtcNow.Date,
                FollowUpType = "Mobilization",
                Close = "No",
                FollowUpURL = string.Format("/Mobilization/Edit/{0}", data.MobilizationId),
                ReadDateTime = _today.AddYears(-100)
            };
            _nidanDataService.Create<FollowUp>(organisationId, followUp);
            return data;
        }

        public Enquiry CreateEnquiry(int organisationId, int personnelId, Enquiry enquiry)
        {
            var data = _nidanDataService.Create<Enquiry>(organisationId, enquiry);
            //Update student code
            data.StudentCode = GenerateStudentCode(organisationId, data.EnquiryId, enquiry.CentreId);
            _nidanDataService.UpdateEntityEntry(data);
            //Create Counselling
            var conselling = new Counselling()
            {
                CentreId = enquiry.CentreId,
                CourseOfferedId = enquiry.IntrestedCourseId,
                EnquiryId = data.EnquiryId,
                FollowUpDate = _today.AddDays(2),
                OrganisationId = organisationId,
                PersonnelId = personnelId,
                ConversionProspect = enquiry.ConversionProspect,
                SectorId = enquiry.SectorId,
                Close = enquiry.Close
            };
            _nidanDataService.Create<Counselling>(organisationId, conselling);
            var followUp = new FollowUp
            {
                CentreId = data.CentreId,
                FollowUpDateTime = data.FollowUpDate.Value,
                EnquiryId = data.EnquiryId,
                Remark = data.Remarks,
                Name = data.CandidateName,
                IntrestedCourseId = data.IntrestedCourseId,
                Mobile = data.Mobile,
                CreatedDateTime = DateTime.UtcNow.Date,
                FollowUpType = "Enquiry",
                FollowUpURL = string.Format("/Enquiry/Edit/{0}", data.EnquiryId),
                AlternateMobile = enquiry.AlternateMobile,
                Close = "No",
                ReadDateTime = _today.AddYears(-100)
            };
            _nidanDataService.Create<FollowUp>(organisationId, followUp);
            return data;
        }

        private string GenerateStudentCode(int organisationId, int enquiryId, int centreId)
        {
            var centre = RetrieveCentre(organisationId, centreId, e => true);
            return enquiryId.ToString(); //string.Format("{0}{1}", centre.Name.Substring(0, 3), enquiryId);
        }

        public void UploadMobilization(int organisationId, int centreId, int eventId, int personnelId, DateTime generateDateTime, List<Mobilization> mobilizations)
        {
            var interestedCourses = RetrieveCourses(organisationId, c => true);
            var qualifications = RetrieveQualifications(organisationId, q => true);
            var mobilizationType =
                RetrieveMobilizationTypes(organisationId, e => e.Name.ToLower() == "event").FirstOrDefault();
            var followUpList = new List<FollowUp>();
            foreach (var item in mobilizations)
            {
                var interestedCourseId =
                    interestedCourses.FirstOrDefault(
                        e => e.Name.Trim().ToLower() == item.InterestedCourse.Trim().ToLower())?.CourseId ??
                    interestedCourses.First(e => e.Name.ToLower() == "others").CourseId;
                var qualificationId =
                    qualifications.FirstOrDefault(
                        q => q.Name.Trim().ToLower() == item.HighestQualification.Trim().ToLower())?.QualificationId ??
                    qualifications.First(e => e.Name.ToLower() == "others").QualificationId;

                var mobilizer = new Mobilization()
                {
                    InterestedCourseId = interestedCourseId,
                    CentreId = centreId,
                    GeneratedDate = generateDateTime,
                    EventId = eventId,
                    QualificationId = qualificationId,
                    Mobile = item.Mobile,
                    Name = item.Name,
                    OrganisationId = organisationId,
                    Remark = item.Remark,
                    StudentLocation = item.StudentLocation,
                    MobilizationTypeId = mobilizationType.MobilizationTypeId,
                    PersonnelId = personnelId,
                    Close = "No",
                    FollowUpDate = DateTime.UtcNow.Date.AddDays(2),
                    OtherInterestedCourse = item.OtherInterestedCourse
                };
                var data = _nidanDataService.CreateMobilization(organisationId, mobilizer);

                followUpList.Add(new FollowUp
                {
                    CentreId = centreId,
                    FollowUpDateTime = DateTime.UtcNow.Date.AddDays(2),
                    MobilizationId = data.MobilizationId,
                    Remark = item.Remark,
                    Name = item.Name,
                    IntrestedCourseId = interestedCourseId,
                    Mobile = item.Mobile,
                    CreatedDateTime = DateTime.UtcNow.Date,
                    ReadDateTime = _today.AddYears(-100),
                    Close = "No",
                    FollowUpType = "Mobilization",
                    FollowUpURL = string.Format("/Mobilization/Edit/{0}", data.MobilizationId)
                });

            }

            _nidanDataService.Create<FollowUp>(organisationId, followUpList);
        }

        public Admission CreateAdmission(int organisationId, Admission admission)
        {
            return _nidanDataService.CreateAdmission(organisationId, admission);
        }

        public Counselling CreateCounselling(int organisationId, Counselling counselling)
        {
            var data = _nidanDataService.Create<Counselling>(organisationId, counselling);
            var enquiry = RetrieveEnquiry(organisationId, data.EnquiryId);
            var followUp = new FollowUp
            {
                CentreId = data.CentreId,
                FollowUpDateTime = data.FollowUpDate.Value,
                EnquiryId = data.EnquiryId,
                Remark = data.Remarks,
                Name = enquiry.CandidateName,
                IntrestedCourseId = data.CourseOfferedId,
                Mobile = enquiry.Mobile,
                CreatedDateTime = DateTime.UtcNow.Date,
                FollowUpType = "Counselling",
                FollowUpURL = string.Format("/Counselling/Edit/{0}", data.CounsellingId),
                ReadDateTime = _today.AddYears(-100)
            };
            _nidanDataService.Create<FollowUp>(organisationId, followUp);
            return data;
        }

        public RegistrationPaymentReceipt CreateRegistrationPaymentReceipt(int organisationId, RegistrationPaymentReceipt registrationPaymentReceipt)
        {
            var enquirydata = RetrieveEnquiry(organisationId, registrationPaymentReceipt.EnquiryId);
            enquirydata.SectorId = registrationPaymentReceipt.Enquiry.SectorId;
            enquirydata.IntrestedCourseId = registrationPaymentReceipt.Enquiry.IntrestedCourseId;
            enquirydata.BatchTimePreferId = registrationPaymentReceipt.Enquiry.BatchTimePreferId;
            var counsellingdata=_nidanDataService.RetrieveCounsellings(organisationId,e=>e.EnquiryId==registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
            var course = _nidanDataService.RetrieveCourse(organisationId, registrationPaymentReceipt.CourseId, e => true);
            registrationPaymentReceipt.Particulars = string.Format(registrationPaymentReceipt.Fees + " Rupees Paid Against " + course.Name);
            registrationPaymentReceipt.FollowUpDate = registrationPaymentReceipt.FollowUpDate ?? DateTime.Now.AddDays(2);
            if (counsellingdata != null) registrationPaymentReceipt.CounsellingId = counsellingdata.CounsellingId;
            var data = _nidanDataService.CreateRegistrationPaymentReceipt(organisationId, registrationPaymentReceipt);
            
            enquirydata.Registered = data != null;
            enquirydata.EnquiryStatus = "Registration";
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquirydata);
            if (counsellingdata != null) counsellingdata.Registered = data != null;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counsellingdata);

            var registrationFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
            if (registrationFollowUp != null)
            {
                registrationFollowUp.RegistrationPaymentReceiptId = data?.RegistrationPaymentReceiptId;
                registrationFollowUp.Remark = data?.Remarks;
                registrationFollowUp.FollowUpDateTime = registrationPaymentReceipt.FollowUpDate ?? DateTime.Now.AddDays(2);
                registrationFollowUp.FollowUpURL = string.Format("/RegistrationPaymentReceipt/Edit/{0}", data?.EnquiryId);
                registrationFollowUp.FollowUpType = "Registered";
            }

            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationFollowUp);
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquirydata);
            return data;
        }

        public Enquiry CreateEnquiryFromMobilization(int organisationId, int centreId, int mobilizationId)
        {
            //var followUp = RetrieveFollowUps(organisationId, e => e.MobilizationId == mobilizationId).Items.FirstOrDefault();
            var mobilization = RetrieveMobilization(organisationId, mobilizationId);
            //Delete follow Up
            //_nidanDataService.Delete<FollowUp>(organisationId, e => e.FollowUpId == followUp.FollowUpId);
            return new Enquiry
            {
                OrganisationId = organisationId,
                CandidateName = mobilization == null ? string.Empty : mobilization.Name,
                Mobile = mobilization.Mobile,
                AlternateMobile = mobilization.AlternateMobile,
                EducationalQualificationId = mobilization.QualificationId,
                Address = mobilization?.StudentLocation ?? string.Empty,
                IntrestedCourseId = mobilization.InterestedCourseId,
                FollowUpDate = DateTime.Today.AddDays(2),
                Close = "No",
                CentreId = centreId,
            };
        }


        public Course CreateCourse(int organisationId, Course course)
        {
            return _nidanDataService.CreateCourse(organisationId, course);
        }

        public CourseInstallment CreateCourseInstallment(int organisationId, CourseInstallment courseInstallment)
        {
            return _nidanDataService.CreateCourseInstallment(organisationId, courseInstallment);
        }
        
        public Event CreateEvent(int organisationId, Event eventplan)
        {
            return _nidanDataService.CreateEvent(organisationId, eventplan);
        }

        public Brainstorming CreateBrainstorming(int organisationId, Brainstorming brainstorming)
        {
            return _nidanDataService.CreateBrainstorming(organisationId, brainstorming);
        }

        public Planning CreatePlanning(int organisationId, Planning planning)
        {
            return _nidanDataService.CreatePlanning(organisationId, planning);
        }

        public Budget CreateBudget(int organisationId, Budget budget)
        {
            return _nidanDataService.CreateBudget(organisationId, budget);
        }

        public Eventday CreateEventday(int organisationId, Eventday eventday)
        {
            return _nidanDataService.CreateEventday(organisationId, eventday);
        }

        public Postevent CreatePostevent(int organisationId, Postevent postevent)
        {
            return _nidanDataService.CreatePostevent(organisationId, postevent);
        }

        #endregion

        #region // Retrieve

        public Personnel RetrievePersonnel(int organisationId, int personnelId)
        {
            var personnel = _nidanDataService.RetrievePersonnel(organisationId, personnelId, p => true);
            return personnel;
        }

        public PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy,
            Paging paging)
        {
            return _nidanDataService.RetrievePersonnel(organisationId, p => p.CentreId == centreId, orderBy, paging);
        }

        public Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate)
        {
            return _nidanDataService.RetrieveEvent(organisationId, eventId, predicate);
        }

        public PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEvents(organisationId, predicate, orderBy, paging);
        }

        public Enquiry RetrieveEnquiry(int organisationId, int id)
        {
            return _nidanDataService.RetrieveEnquiry(organisationId, id, p => true);
        }

        public PagedResult<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiries(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<Mobilization> RetrieveMobilizations(int organisationId, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizations(organisationId, predicate, orderBy, paging);
        }

        public Mobilization RetrieveMobilization(int organisationId, int mobilizationId,
            Expression<Func<Mobilization, bool>> predicate)
        {
            var mobilization = _nidanDataService.RetrieveMobilization(organisationId, mobilizationId, p => true);
            return mobilization;
        }

        public Mobilization RetrieveMobilization(int organisationId, int id)
        {
            return _nidanDataService.RetrieveMobilization(organisationId, id, p => true);
        }

        public Enquiry RetrieveEnquiry(int organisationId, int enquiryId, Expression<Func<Enquiry, bool>> predicate)
        {
            var enquiry = _nidanDataService.RetrieveEnquiry(organisationId, enquiryId, p => true);
            return enquiry;
        }

        private ValidationResult<AbsenceType> AbsenceTypeAlreadyExists(int organisationId, int? absenceTypeId,
            string name)
        {
            var alreadyExists =
                _nidanDataService.RetrieveAbsenceTypes(organisationId,
                        at => at.Name.ToLower() == name.Trim().ToLower() && at.AbsenceTypeId != (absenceTypeId ?? -1))
                    .Items.Any();
            return new ValidationResult<AbsenceType>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Absence type already exists." } : null
            };
        }

        private ValidationResult<AreaOfInterest> AreaOfInterestAlreadyExists(int organisationId, int? areaOfInterestId,
            string name)
        {
            var alreadyExists =
                _nidanDataService.RetrieveAreaOfInterests(organisationId,
                        at =>
                            at.Name.ToLower() == name.Trim().ToLower() &&
                            at.AreaOfInterestId != (areaOfInterestId ?? -1))
                    .Items.Any();
            return new ValidationResult<AreaOfInterest>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Area Of Interest already exists." } : null
            };
        }

        private ValidationResult<Enquiry> EnquiryAlreadyExists(int organisationId, int? enquiryId, string name)
        {
            var alreadyExists =
                _nidanDataService.RetrieveEnquiries(organisationId,
                        at => at.CandidateName.ToLower() == name.Trim().ToLower() && at.EnquiryId != (enquiryId ?? -1))
                    .Items.Any();
            return new ValidationResult<Enquiry>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Enquiry already exists." } : null
            };
        }

        public AbsenceType RetrieveAbsenceType(int organisationId, int id)
        {
            return _nidanDataService.RetrieveAbsenceType(organisationId, id, p => true);
        }

        public PagedResult<AbsenceType> RetrieveAbsenceTypes(int organisationId, List<OrderBy> orderBy, Paging paging)
        {
            return _nidanDataService.RetrieveAbsenceTypes(organisationId, p => true, orderBy, paging);
        }

        public IEnumerable<Colour> RetrieveColours()
        {
            return _nidanDataService.RetrieveColours(p => true);
        }

        public AreaOfInterest RetrieveAreaOfInterest(int organisationId, int id)
        {
            return _nidanDataService.RetrieveAreaOfInterest(organisationId, id, p => true);
        }

        public PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, List<OrderBy> orderBy,
            Paging paging)
        {
            return _nidanDataService.RetrieveAreaOfInterests(organisationId, p => true, orderBy, paging);
        }

        //private Document RetrieveDocument(int organisationId, int personnelId)
        //{
        //    var organisation = RetrieveOrganisation(organisationId);
        //    return _documentServiceAPI.RetrieveDocuments(organisation.Name, PersonnelProfileCategory,
        //                        new DocumentAttribute
        //                        {
        //                            Key = PersonnelPhotoKey,
        //                            Value = personnelId.ToString()
        //                        }).FirstOrDefault();
        //}

        //public string RetrievePhoto(int organisationId, int personnelId)
        //{

        //    var document = RetrieveDocument(organisationId, personnelId);

        //    if (document == null)
        //    {
        //        return string.Empty;
        //    }

        //    var documentBytes = _documentServiceAPI.Download(document.DocumentGuid);

        //    return documentBytes.Bytes;
        //}

        public Organisation RetrieveOrganisation(int organisationId)
        {
            EnsureOrganisationCache();
            var organisation = (List<Organisation>)_cacheProvider.Get(OrganisationCacheKey);
            return organisation.FirstOrDefault(o => o.OrganisationId == organisationId);
        }

        public Organisation RetrieveOrganisation(string name)
        {
            EnsureOrganisationCache();
            var organisation = (List<Organisation>)_cacheProvider.Get(OrganisationCacheKey);
            return organisation.FirstOrDefault(o => o.Name == name);
        }

        private void EnsureOrganisationCache()
        {
            lock (lockObject)
            {
                if (_cacheProvider.IsSet(OrganisationCacheKey))
                    return;

                var organisation = _nidanDataService.RetrieveOrganisations().ToList();
                _cacheProvider.Set(OrganisationCacheKey, organisation, ConfigHelper.CacheTimeout);
            }
        }

        public PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId,
            string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrievePersonnelBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        }

        //public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    return _nidanDataService.RetrieveEnquiryBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        //}

        public IEnumerable<Personnel> RetrieveReportsToPersonnel(int organisationId, int personnelId)
        {
            return _nidanDataService.RetrievePersonnel(organisationId, p => p.PersonnelId != personnelId).Items;
        }

        public IEnumerable<TenantOrganisation> RetrieveTenantOrganisations()
        {
            var hosts = _nidanDataService.RetrieveHosts();

            return hosts
                .Select(h => new TenantOrganisation
                {
                    OrganisationId = h.OrganisationId,
                    Name = h.Organisation.Name,
                    HostName = h.HostName
                })
                .ToList();
        }

        public IAuthorisation RetrieveUserAuthorisation(string aspNetUserId)
        {
            var userAuthorisation = _nidanDataService.RetrieveUserAuthorisation(aspNetUserId);
            if (userAuthorisation == null)
                return null;

            return new Authorisation
            {
                OrganisationId = userAuthorisation.OrganisationId,
                RoleId = int.Parse(userAuthorisation.RoleId)
            };
        }

        public Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId,
            int? personnelId = null)
        {
            var isManagerOf = true;
            var isPerson = userPersonnelId == personnelId;
            var personnelNode = true;
            var personnelIsTerminated = false;

            return new Permissions
            {
                IsAdmin = isAdmin,
                IsManager = isManagerOf,
                CanViewProfile = isAdmin || isManagerOf || isPerson,
                CanEditProfile = isAdmin || (!personnelIsTerminated && isPerson),
                CanCreateAbsence = isAdmin || (!personnelIsTerminated && (isManagerOf || isPerson)),
                CanEditAbsence = isAdmin || isManagerOf || (!personnelIsTerminated && isPerson),
                CanCancelAbsence = isAdmin || isManagerOf || (!personnelIsTerminated && isPerson),
                CanApproveAbsence = isAdmin || isManagerOf,
                CanEditEntitlements = isAdmin,
                CanEditEmployments = isAdmin
            };
        }

        public PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate,
            List<OrderBy> orderBy, Paging paging)
        {
            return _nidanDataService.RetrieveQuestions(organisationId, predicate, orderBy, paging);
        }

        public Question RetrieveQuestion(int organisationId, int questionId, Expression<Func<Question, bool>> predicate)
        {
            return _nidanDataService.RetrieveQuestion(organisationId, questionId, predicate);
        }

        public List<EventActivityType> RetrieveActivityTypes(int organisationId)
        {
            return _nidanDataService.Retrieve<EventActivityType>(organisationId, e => true);
        }

        public PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUps(organisationId, predicate, orderBy, paging);
        }

        public FollowUp RetrieveFollowUp(int organisationId, int followUpId)
        {
            return _nidanDataService.RetrieveFollowUp(organisationId, followUpId, p => true);
        }

        public List<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Course>(organisationId, predicate);
        }

        public List<Qualification> RetrieveQualifications(int organisationId,
            Expression<Func<Qualification, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Qualification>(organisationId, e => true);
        }

        public List<Religion> RetrieveReligions(int organisationId, Expression<Func<Religion, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Religion>(organisationId, e => true);
        }

        public List<Scheme> RetrieveSchemes(int organisationId, Expression<Func<Scheme, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Scheme>(organisationId, e => true);
        }

        public List<Sector> RetrieveSectors(int organisationId, Expression<Func<Sector, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Sector>(organisationId, predicate);
        }

        public List<BatchTimePrefer> RetrieveBatchTimePrefers(int organisationId,
            Expression<Func<BatchTimePrefer, bool>> predicate)
        {
            return _nidanDataService.Retrieve<BatchTimePrefer>(organisationId, e => true);
        }

        public List<StudentType> RetrieveStudentTypes(int organisationId, Expression<Func<StudentType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<StudentType>(organisationId, e => true);
        }

        public List<EnquiryType> RetrieveEnquiryTypes(int organisationId, Expression<Func<EnquiryType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<EnquiryType>(organisationId, e => true);
        }

        public List<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Enquiry>(organisationId, e => true);
        }

        public List<State> RetrieveStates(int organisationId, Expression<Func<State, bool>> predicate)
        {
            return _nidanDataService.Retrieve<State>(organisationId, e => true);
        }

        public List<District> RetrieveDistricts(int organisationId, Expression<Func<District, bool>> predicate)
        {
            return _nidanDataService.Retrieve<District>(organisationId, e => true);
        }

        public List<Taluka> RetrieveTalukas(int organisationId, Expression<Func<Taluka, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Taluka>(organisationId, e => true);
        }


        public List<EventFunctionType> RetrieveEventFunctionTypes(int organisationId, Expression<Func<EventFunctionType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<EventFunctionType>(organisationId, e => true);
        }
        
        public List<PaymentMode> RetrievePaymentModes(int organisationId, Expression<Func<PaymentMode, bool>> predicate)
        {
            return _nidanDataService.Retrieve<PaymentMode>(organisationId, e => true);
        }

        public List<CasteCategory> RetrieveCasteCategories(int organisationId,
            Expression<Func<CasteCategory, bool>> predicate)
        {
            return _nidanDataService.Retrieve<CasteCategory>(organisationId, e => true);
        }

        public List<HowDidYouKnowAbout> RetrieveHowDidYouKnowAbouts(int organisationId,
            Expression<Func<HowDidYouKnowAbout, bool>> predicate)
        {
            return _nidanDataService.Retrieve<HowDidYouKnowAbout>(organisationId, e => true);
        }

        public List<Occupation> RetrieveOccupations(int organisationId, Expression<Func<Occupation, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Occupation>(organisationId, e => true);
        }

        public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiryBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        public List<MobilizationType> RetrieveMobilizationTypes(int organisationId,
            Expression<Func<MobilizationType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<MobilizationType>(organisationId, e => true);
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCentres(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Batch> RetrieveBatches(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatches(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Mobilization, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizationBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        //public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate,
        //    List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    return _nidanDataService.RetrieveEnquiryBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        //}

        public List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Centre>(organisationId, e => true);
        }

        public PagedResult<Counselling> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellingBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        public Brainstorming RetrieveBrainstorming(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBrainstorming(organisationId, id, p => true);
        }

        public Brainstorming RetrieveBrainstorming(int organisationId, int brainstormingId, Expression<Func<Brainstorming, bool>> predicate)
        {
            var brainstorming = _nidanDataService.RetrieveBrainstorming(organisationId, brainstormingId, p => true);
            return brainstorming;
        }

        public PagedResult<Brainstorming> RetrieveBrainstormings(int organisationId, Expression<Func<Brainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBrainstormings(organisationId, p => true, orderBy, paging);
        }

        public Planning RetrievePlanning(int organisationId, int id)
        {
            return _nidanDataService.RetrievePlanning(organisationId, id, p => true);
        }

        public Planning RetrievePlanning(int organisationId, int planningId, Expression<Func<Planning, bool>> predicate)
        {
            var planning = _nidanDataService.RetrievePlanning(organisationId, planningId, p => true);
            return planning;
        }

        public PagedResult<Planning> RetrievePlannings(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrievePlannings(organisationId, p => true, orderBy, paging);
        }

        public Budget RetrieveBudget(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBudget(organisationId, id, p => true);
        }

        public Budget RetrieveBudget(int organisationId, int budgetId, Expression<Func<Budget, bool>> predicate)
        {
            var budget = _nidanDataService.RetrieveBudget(organisationId, budgetId, p => true);
            return budget;
        }

        public PagedResult<Budget> RetrieveBudgets(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBudgets(organisationId, p => true, orderBy, paging);
        }

        public Eventday RetrieveEventday(int organisationId, int id)
        {
            return _nidanDataService.RetrieveEventday(organisationId, id, p => true);
        }

        public Eventday RetrieveEventday(int organisationId, int eventdayId, Expression<Func<Eventday, bool>> predicate)
        {
            var eventday = _nidanDataService.RetrieveEventday(organisationId, eventdayId, p => true);
            return eventday;
        }

        public PagedResult<Eventday> RetrieveEventdays(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEventdays(organisationId, p => true, orderBy, paging);
        }

        public Postevent RetrievePostevent(int organisationId, int id)
        {
            return _nidanDataService.RetrievePostevent(organisationId, id, p => true);
        }

        public Postevent RetrievePostevent(int organisationId, int posteventId, Expression<Func<Postevent, bool>> predicate)
        {
            var postevent = _nidanDataService.RetrievePostevent(organisationId, posteventId, p => true);
            return postevent;
        }

        public PagedResult<Postevent> RetrievePostevents(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrievePostevents(organisationId, p => true, orderBy, paging);
        }
        
        public PagedResult<RegistrationPaymentReceipt> RetrieveRegistrationPaymentReceipts(int organisationId, Expression<Func<RegistrationPaymentReceipt, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveRegistrationPaymentReceipts(organisationId, predicate, orderBy, paging);
        }

        public RegistrationPaymentReceipt RetrieverRegistrationPaymentReceipt(int organisationId, int registrationPaymentReceiptId,
            Expression<Func<RegistrationPaymentReceipt, bool>> predicate)
        {
            var registrationPaymentReceipt = _nidanDataService.RetrieveRegistrationPaymentReceipt(organisationId, registrationPaymentReceiptId, p => true);
            return registrationPaymentReceipt;
        }

        public RegistrationPaymentReceipt RetrieveRegistrationPaymentReceipt(int organisationId, int id)
        {
            return _nidanDataService.RetrieveRegistrationPaymentReceipt(organisationId, id, p => true);

        }

        public Course RetrieveCourse(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCourse(organisationId, id, p => true);
        }

        public PagedResult<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCourses(organisationId, predicate, orderBy, paging);
        }

        public Course RetrieveCourse(int organisationId, int courseId, Expression<Func<Course, bool>> predicate)
        {
            var course = _nidanDataService.RetrieveCourse(organisationId, courseId, p => true);
            return course;
        }

        public PagedResult<CourseInstallment> RetrieveCourseInstallments(int organisationId, Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCourseInstallments(organisationId, predicate, orderBy, paging);
        }

        public CourseInstallment RetrieveCourseInstallment(int organisationId, int courseInstallmentId, Expression<Func<CourseInstallment, bool>> predicate)
        {
            var courseInstallment = _nidanDataService.RetrieveCourseInstallment(organisationId, courseInstallmentId, p => true);
            return courseInstallment;
        }

        public CourseInstallment RetrieveCourseInstallment(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCourseInstallment(organisationId, id, p => true);
        }

        #endregion

        #region // Update

        //public void UploadPhoto(int organisationId, int personnelId, byte[] photo)
        //{
        //    var personnel = _nidanDataService.RetrievePersonnel(organisationId, personnelId, x => true);
        //    var organisation = RetrieveOrganisation(organisationId);
        //    var document = RetrieveDocument(organisationId, personnelId);

        //    if (document != null)
        //    {
        //        //DeletePhoto(document.DocumentGuid);
        //    }

        //    //_documentServiceAPI.CreateDocument(
        //    //            new Document
        //    //            {
        //    //                Product = organisation.Name,
        //    //                Category = PersonnelProfileCategory,
        //    //                Content = photo,
        //    //                Description = "Profile Picture",
        //    //                //FileName = personnel.Fullname + ".jpg",
        //    //                PayrollId = personnelId.ToString(),
        //    //                //EmployeeName = personnel.Fullname,
        //    //                CreatedBy = personnelId.ToString(),
        //    //                DocumentAttribute = new List<DocumentAttribute>
        //    //                {
        //    //                    new DocumentAttribute
        //    //                    {
        //    //                        Key = PersonnelPhotoKey,
        //    //                        Value = personnelId.ToString()
        //    //                    }
        //    //                }
        //    //            });
        //}

        public ValidationResult<AbsenceType> UpdateAbsenceType(int organisationId, AbsenceType absenceType)
        {
            var validationResult = AbsenceTypeAlreadyExists(organisationId, absenceType.AbsenceTypeId, absenceType.Name);
            if (!validationResult.Succeeded)
                return validationResult;

            try
            {
                validationResult.Entity = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, absenceType);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public ValidationResult<AreaOfInterest> UpdateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest)
        {
            var validationResult = AreaOfInterestAlreadyExists(organisationId, areaOfInterest.AreaOfInterestId,
                areaOfInterest.Name);
            if (!validationResult.Succeeded)
                return validationResult;

            try
            {
                validationResult.Entity = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, areaOfInterest);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate)
        {
            var centre = _nidanDataService.RetrieveCentre(organisationId, centreId, p => true);
            return centre;
        }

        public Batch RetrieveBatch(int organisationId, int batchId, Expression<Func<Batch, bool>> predicate)
        {
            var batch = _nidanDataService.RetrieveBatch(organisationId, batchId, p => true);
            return batch;
        }

        public Centre RetrieveCentre(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCentre(organisationId, id, p => true);
        }

        public PagedResult<Counselling> RetrieveCounsellings(int organisationId, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellings(organisationId, predicate, orderBy, paging);
        }

        public Batch RetrieveBatch(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBatch(organisationId, id, p => true);
        }

        public Counselling RetrieveCounselling(int organisationId, int counsellingId,
            Expression<Func<Counselling, bool>> predicate)
        {
            var counselling = _nidanDataService.RetrieveCounselling(organisationId, counsellingId, p => true);
            return counselling;
        }

        public Counselling RetrieveCounselling(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCounselling(organisationId, id, p => true);
        }

        public PagedResult<Admission> RetrieveAdmissions(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissions(organisationId, p => true, orderBy, paging);
        }

        public Admission RetrieveAdmission(int organisationId, int admissionId,
            Expression<Func<Admission, bool>> predicate)
        {
            var admission = _nidanDataService.RetrieveAdmission(organisationId, admissionId, p => true);
            return admission;
        }

        public Admission RetrieveAdmission(int organisationId, int id)
        {
            return _nidanDataService.RetrieveAdmission(organisationId, id, p => true);
        }

        public Personnel UpdatePersonnel(int organisationId, Personnel personnel)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, personnel);
        }

        public Enquiry UpdateEnquiry(int organisationId, Enquiry enquiry)
        {
            //update follow Up
            var enquiryFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            var counsellingFromEnquiry = enquiry.Counsellings?.FirstOrDefault(e => e.EnquiryId == enquiry.EnquiryId);
            var counselling = _nidanDataService.RetrieveCounselling(organisationId, counsellingFromEnquiry?.CounsellingId ?? -1, e => true);
            if (enquiryFollowUp != null)
            {
                enquiryFollowUp.FollowUpDateTime = enquiry.FollowUpDate ?? enquiryFollowUp.FollowUpDateTime;
                enquiry.FollowUpDate = enquiryFollowUp.FollowUpDateTime;
                enquiryFollowUp.Close = enquiry.Close;
                enquiryFollowUp.Name = enquiry.CandidateName;
                enquiryFollowUp.Mobile = enquiry.Mobile;
                enquiryFollowUp.AlternateMobile = enquiry.AlternateMobile;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryFollowUp);
            }
            if (counselling != null)
            {
                counselling.FollowUpDate = enquiry.FollowUpDate ?? enquiryFollowUp.FollowUpDateTime.AddDays(2);
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
        }

        public Mobilization UpdateMobilization(int organisationId, Mobilization mobilization)
        {
            //update follow Up
            var mobilizationFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.MobilizationId == mobilization.MobilizationId).Items.FirstOrDefault();
            mobilizationFollowUp.FollowUpDateTime = mobilization.FollowUpDate ?? mobilizationFollowUp.FollowUpDateTime;
            //mobilizationFollowUp.Closed = mobilization.Close == "Yes";
            mobilizationFollowUp.Name = mobilization.Name ?? mobilizationFollowUp.Name;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilizationFollowUp);

            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilization);
        }

        public FollowUp UpdateFollowUp(int organisationId, FollowUp followUp)
        {
            //Update Mobilization Date
            if (followUp.MobilizationId.HasValue && followUp.MobilizationId.Value != 0)
            {
                var mobilization = _nidanDataService.RetrieveMobilization(organisationId, followUp.MobilizationId.Value, e => true);
                mobilization.FollowUpDate = followUp.FollowUpDateTime;
                mobilization.Close = followUp.Close;
                mobilization.ClosingRemark = followUp.ClosingRemark;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
            }

            //Update Enquiry Date
            if (followUp.EnquiryId.HasValue && followUp.EnquiryId.Value != 0)
            {
                var enquiry = _nidanDataService.RetrieveEnquiry(organisationId, followUp.EnquiryId.Value, e => true);
                enquiry.FollowUpDate = followUp.FollowUpDateTime;
                enquiry.Close = followUp.Close;
                enquiry.ClosingRemark = followUp.ClosingRemark;

                var counsellingFromEnquiry = _nidanDataService.RetrieveEnquiry(organisationId, followUp.EnquiryId.Value, e => true);
                var counsellingData =
                    counsellingFromEnquiry.Counsellings.FirstOrDefault(
                        e => e.EnquiryId == counsellingFromEnquiry.EnquiryId);

                if (counsellingData != null)
                {
                    var counselling = _nidanDataService.RetrieveCounselling(organisationId,
                        counsellingData.CounsellingId, c => true);
                    counselling.Close = followUp.Close;
                    counselling.ClosingRemark = followUp.ClosingRemark;
                    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);

                }
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            }

            //Update Counselling Date
            if (followUp.CounsellingId.HasValue && followUp.CounsellingId.Value != 0)
            {
                var counselling = _nidanDataService.RetrieveCounselling(organisationId, followUp.CounsellingId.Value, e => true);
                counselling.FollowUpDate = followUp.FollowUpDateTime;
                counselling.Close = followUp.Close;
                counselling.ClosingRemark = followUp.ClosingRemark;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }

            //Update RegistrationPaymentReceipt Date
            if (followUp.RegistrationPaymentReceiptId.HasValue && followUp.RegistrationPaymentReceiptId.Value != 0)
            {
                var registrationPaymentReceipt = _nidanDataService.RetrieveRegistrationPaymentReceipt(organisationId, followUp.RegistrationPaymentReceiptId.Value, e => true);
                registrationPaymentReceipt.FollowUpDate = followUp.FollowUpDateTime;
                //registrationPaymentReceipt.Close = followUp.Close;
                //registrationPaymentReceipt.ClosingRemark = followUp.ClosingRemark;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationPaymentReceipt);
            }

            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
        }

        public Centre UpdateCentre(int organisationId, Centre centre)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centre);
        }

        public Counselling UpdateCounselling(int organisationId, Counselling counselling)
        {
            var enquiryFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == counselling.EnquiryId).Items.FirstOrDefault();
            var enquiry = _nidanDataService.RetrieveEnquiry(organisationId, counselling.EnquiryId, e => true);

            if (enquiryFollowUp != null)
            {
                enquiryFollowUp.FollowUpDateTime = counselling.FollowUpDate.Value;
                enquiryFollowUp.FollowUpType = "Counselling";
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryFollowUp);
            }

            enquiry.FollowUpDate = counselling.FollowUpDate.Value;
            enquiry.EnquiryStatus = "Counselling";
            counselling.Close = enquiry.Close;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
        }

        public RegistrationPaymentReceipt UpdateRegistrationPaymentReceipt(int organisationId,
            RegistrationPaymentReceipt registrationPaymentReceipt)
        {
            var data = _nidanDataService.RetrieveRegistrationPaymentReceipt(organisationId, registrationPaymentReceipt.RegistrationPaymentReceiptId,e=>true);
            var course = _nidanDataService.RetrieveCourse(organisationId, registrationPaymentReceipt.CourseId, e => true);
            registrationPaymentReceipt.Particulars = string.Format(registrationPaymentReceipt.Fees + " Rupees Paid Against " + course.Name);
            
            var registrationFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
            if (registrationFollowUp != null)
            {
                registrationFollowUp.Remark = data?.Remarks;
                registrationFollowUp.FollowUpDateTime = registrationPaymentReceipt.FollowUpDate ??
                                                        DateTime.Now.AddDays(2);
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationFollowUp);
            }
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationPaymentReceipt);
        }


        public Admission UpdateAdmission(int organisationId, Admission admission)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, admission);
        }

        public Course UpdateCourse(int organisationId, Course course)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, course);
        }

        public CourseInstallment UpdateCourseInstallment(int organisationId, CourseInstallment courseInstallment)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, courseInstallment);
        }
        
        public Question UpdateQuestion(int organisationId, Question question)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, question);
        }

        public Event UpdateEvent(int organisationId, Event eventplan)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, eventplan);
        }

        public Batch UpdateBatch(int organisationId, Batch batch)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, batch);
        }

        #endregion

        #region //Delete

        //Delete
        public void DeletePersonnel(int organisationId, int personnelId)
        {
            _nidanDataService.Delete<Personnel>(organisationId, e => e.PersonnelId == personnelId);
        }

        public void DeleteFollowUp(int organisationId, int followUpId)
        {
            _nidanDataService.Delete<FollowUp>(organisationId, e => e.FollowUpId == followUpId);
        }

        public void MarkAsReadFollowUp(int organisationId, int id)
        {
            var data = RetrieveFollowUp(organisationId, id);
            data.ReadDateTime = _today;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, data);
        }



        #endregion

        //Document
        public List<DocumentType> RetrieveDocumentTypes(int organisationId)
        {
            return _nidanDataService.RetrieveDocumentTypes(organisationId).ToList();
        }

        public Document CreateDocument(int organisationId, int centreId, int documentTypeId, string filePath,
            string studentCode, string studentName, string description, string fileName, Guid guid)
        {
            var document = new Document()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                DocumentTypeId = documentTypeId,
                StudentCode = studentCode,
                CreatedDateTime = DateTime.UtcNow.Date,
                Description = description,
                FileName = fileName,
                Location = filePath,
                StudentName = studentName,
                Guid = guid
            };

            return _nidanDataService.Create<Document>(organisationId, document);
        }

        public PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveDocuments(organisationId, predicate, orderBy, paging);
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            return _nidanDataService.RetrieveDocument(organisationId, documentGuid);
        }
    }
}

