﻿using System;
using System.Collections.Generic;
using System.Data;
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
        private enum ShowColour { Company = 1, Division, Department };
        private const string OrganisationCacheKey = "Organisations";
        private const string OrganisationEmploymentsTreeKey = "OrganisationEmploymentsTree";
        private const string AbsenceStatusTemplateKey = "HRAbsenceStatus";
        private object lockObject = new object();
        readonly string PersonnelPhotoKey = "PersonnelPhoto";
        readonly string PersonnelProfileCategory = "ProfileImage";
        private readonly DateTime _today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        public NidanBusinessService(INidanDataService nidanDataService, ICacheProvider cacheProvider, ITemplateService templateService, IEmailService emailService)
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
            return _nidanDataService.Create<Question>(organisationId, question);
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
                CreatedDateTime = DateTime.Now,
                ReadDateTime = _today.AddYears(-100)
            };
            _nidanDataService.Create<FollowUp>(organisationId, followUp);
            return data;
        }

        public Enquiry CreateEnquiry(int organisationId, Enquiry enquiry)
        {
            var data = _nidanDataService.Create<Enquiry>(organisationId, enquiry);
            var followUp = new FollowUp
            {
                CentreId = data.CentreId,
                FollowUpDateTime = data.FollowUpDate.Value,
                EnquiryId = data.EnquiryId,
                Remark = data.Remarks,
                Name = data.CandidateName,
                IntrestedCourseId = data.IntrestedCourseId,
                Mobile = data.ContactNo,
                CreatedDateTime = DateTime.Now,
                ReadDateTime = _today.AddYears(-100)
            };

            var counselling = new Counselling
            {
                EnquiryId = data.EnquiryId,
                CentreId = data.CentreId,
                CourseOfferedId = data.IntrestedCourseId,
                Name = data.CandidateName
            };
            _nidanDataService.Create<Counselling>(organisationId, counselling);
            _nidanDataService.Create<FollowUp>(organisationId, followUp);
            return data;
        }

        public void UploadMobilization(int organisationId, int eventId, int personnelId, DateTime generateDateTime, List<Mobilization> mobilizations)
        {
            var interestedCourses = RetrieveCourses(organisationId, c => true);
            var qualifications = RetrieveQualifications(organisationId, q => true);
            var mobilizationType = RetrieveMobilizationTypes(organisationId, e => e.Name.ToLower() == "event").FirstOrDefault();
            var mobilizationList = new List<Mobilization>();
            var followUpList = new List<FollowUp>();
            foreach (var item in mobilizations)
            {
                var interestedCourseId = interestedCourses.FirstOrDefault(e => e.Name.Trim().ToLower() == item.InterestedCourse.Trim().ToLower())?.CourseId ??
                                         interestedCourses.First(e => e.Name.ToLower() == "others").CourseId;
                var qualificationId = qualifications.FirstOrDefault(q => q.Name.Trim().ToLower() == item.HighestQualification.Trim().ToLower())?.QualificationId ??
                                       qualifications.First(e => e.Name.ToLower() == "others").QualificationId;

                mobilizationList.Add(new Mobilization()
                {
                    InterestedCourseId = interestedCourseId,
                    CentreId = 1,
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
                    FollowUpDate = DateTime.Now.AddDays(2),
                    OtherInterestedCourse = item.OtherInterestedCourse
                });
                followUpList.Add(new FollowUp
                {
                    CentreId = item.CentreId,
                    FollowUpDateTime = DateTime.Now.AddDays(2),
                    MobilizationId = item.MobilizationId,
                    Remark = item.Remark,
                    Name = item.Name,
                    IntrestedCourseId = interestedCourseId,
                    Mobile = item.Mobile,
                    CreatedDateTime = DateTime.Now,
                    ReadDateTime = _today.AddYears(-100),
                });

            }
            _nidanDataService.Create<Mobilization>(organisationId, mobilizationList);
            _nidanDataService.Create<FollowUp>(organisationId, followUpList);
        }

        public Admission CreateAdmission(int organisationId, Admission admission)
        {
            return _nidanDataService.Create<Admission>(organisationId, admission);
        }

        public CommercialAdmission CreateCommercialAdmission(int organisationId, CommercialAdmission commercialAdmission)
        {
            return _nidanDataService.Create<CommercialAdmission>(organisationId, commercialAdmission);
        }

        public GovernmentAdmission CreateGovernmentAdmission(int organisationId, GovernmentAdmission governmentAdmission)
        {
            return _nidanDataService.Create<GovernmentAdmission>(organisationId, governmentAdmission);
        }

        #endregion

        #region // Retrieve

        public Personnel RetrievePersonnel(int organisationId, int personnelId)
        {
            var personnel = _nidanDataService.RetrievePersonnel(organisationId, personnelId, p => true);
            return personnel;
        }

        public PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy, Paging paging)
        {
            return _nidanDataService.RetrievePersonnel(organisationId, p => p.CentreId == centreId, orderBy, paging);
        }



        public Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate)
        {
            return _nidanDataService.RetrieveEvent(organisationId, eventId, predicate);
        }

        public PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEvents(organisationId, predicate, orderBy, paging);
        }
        public Enquiry RetrieveEnquiry(int organisationId, int id)
        {
            return _nidanDataService.RetrieveEnquiry(organisationId, id, p => true);
        }

        public PagedResult<Enquiry> RetrieveEnquiries(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiries(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Mobilization> RetrieveMobilizations(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizations(organisationId, p => true, orderBy, paging);
        }

        public Mobilization RetrieveMobilization(int organisationId, int mobilizationId, Expression<Func<Mobilization, bool>> predicate)
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


        private ValidationResult<AbsenceType> AbsenceTypeAlreadyExists(int organisationId, int? absenceTypeId, string name)
        {
            var alreadyExists =
               _nidanDataService.RetrieveAbsenceTypes(organisationId, at => at.Name.ToLower() == name.Trim().ToLower() && at.AbsenceTypeId != (absenceTypeId ?? -1))
                    .Items.Any();
            return new ValidationResult<AbsenceType>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Absence type already exists." } : null
            };
        }

        private ValidationResult<AreaOfInterest> AreaOfInterestAlreadyExists(int organisationId, int? areaOfInterestId, string name)
        {
            var alreadyExists =
               _nidanDataService.RetrieveAreaOfInterests(organisationId, at => at.Name.ToLower() == name.Trim().ToLower() && at.AreaOfInterestId != (areaOfInterestId ?? -1))
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
               _nidanDataService.RetrieveEnquiries(organisationId, at => at.CandidateName.ToLower() == name.Trim().ToLower() && at.EnquiryId != (enquiryId ?? -1))
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

        public PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, List<OrderBy> orderBy, Paging paging)
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

        public PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
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

        public Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId, int? personnelId = null)
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

        public PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate, List<OrderBy> orderBy, Paging paging)
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

        public PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUps(organisationId, predicate, orderBy, paging);
        }

        public FollowUp RetrieveFollowUp(int organisationId, int followUpId)
        {
            return _nidanDataService.RetrieveFollowUp(organisationId, followUpId, p => true);
        }

        public List<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Course>(organisationId, e => true);
        }

        public List<Qualification> RetrieveQualifications(int organisationId, Expression<Func<Qualification, bool>> predicate)
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

        public List<SchemeType> RetrieveSchemeTypes(int organisationId, Expression<Func<SchemeType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<SchemeType>(organisationId, e => true);
        }

        public List<Sector> RetrieveSectors(int organisationId, Expression<Func<Sector, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Sector>(organisationId, e => true);
        }

        public List<BatchTimePrefer> RetrieveBatchTimePrefers(int organisationId, Expression<Func<BatchTimePrefer, bool>> predicate)
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

        public List<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Batch>(organisationId, e => true);
        }

        public List<SubSector> RetrieveSubSectors(int organisationId, Expression<Func<SubSector, bool>> predicate)
        {
            return _nidanDataService.Retrieve<SubSector>(organisationId, e => true);
        }

        public List<Disability> RetrieveDisabilities(int organisationId, Expression<Func<Disability, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Disability>(organisationId, e => true);
        }

        public List<AlternateIdType> RetrieveAlternateIdTypes(int organisationId, Expression<Func<AlternateIdType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AlternateIdType>(organisationId, e => true);
        }

        public List<CasteCategory> RetrieveCasteCategories(int organisationId, Expression<Func<CasteCategory, bool>> predicate)
        {
            return _nidanDataService.Retrieve<CasteCategory>(organisationId, e => true);
        }

        public List<HowDidYouKnowAbout> RetrieveHowDidYouKnowAbouts(int organisationId, Expression<Func<HowDidYouKnowAbout, bool>> predicate)
        {
            return _nidanDataService.Retrieve<HowDidYouKnowAbout>(organisationId, e => true);
        }

        public List<Occupation> RetrieveOccupations(int organisationId, Expression<Func<Occupation, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Occupation>(organisationId, e => true);
        }

        public List<MobilizationType> RetrieveMobilizationTypes(int organisationId, Expression<Func<MobilizationType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<MobilizationType>(organisationId, e => true);
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentres(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Batch> RetrieveBatches(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatches(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizationBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        }

        public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiryBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        }

        public PagedResult<AdmissionSearchField> RetrieveAdmissionBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissionBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        }

        public List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Centre>(organisationId, e => true);
        }

        public PagedResult<CommercialAdmission> RetrieveCommercialAdmissions(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCommercialAdmissions(organisationId, p => true, orderBy, paging);
        }

        public CommercialAdmission RetrieveCommercialAdmission(int organisationId, int commercialAdmissionId, Expression<Func<CommercialAdmission, bool>> predicate)
        {
            var commercialAdmission = _nidanDataService.RetrieveCommercialAdmission(organisationId, commercialAdmissionId, p => true);
            return commercialAdmission;
        }

        public CommercialAdmission RetrieveCommercialAdmission(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCommercialAdmission(organisationId, id, p => true);
        }

        public PagedResult<GovernmentAdmission> RetrieveGovernmentAdmissions(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveGovernmentAdmissions(organisationId, p => true, orderBy, paging);
        }

        public GovernmentAdmission RetrieveGovernmentAdmission(int organisationId, int governmentAdmissionId, Expression<Func<GovernmentAdmission, bool>> predicate)
        {
            var governmentAdmission = _nidanDataService.RetrieveGovernmentAdmission(organisationId, governmentAdmissionId, p => true);
            return governmentAdmission;
        }

        public GovernmentAdmission RetrieveGovernmentAdmission(int organisationId, int id)
        {
            return _nidanDataService.RetrieveGovernmentAdmission(organisationId, id, p => true);
        }

        public PagedResult<CommercialAdmission> RetrieveCommercialAdmissionBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCommercialAdmissionBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
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
            var validationResult = AreaOfInterestAlreadyExists(organisationId, areaOfInterest.AreaOfInterestId, areaOfInterest.Name);
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

        public PagedResult<Counselling> RetrieveCounsellings(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellings(organisationId, p => true, orderBy, paging);
        }
            public Batch RetrieveBatch(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBatch(organisationId, id, p => true);
        }

        public Counselling RetrieveCounselling(int organisationId, int counsellingId, Expression<Func<Counselling, bool>> predicate)
        {
            var counselling = _nidanDataService.RetrieveCounselling(organisationId, counsellingId, p => true);
            return counselling;
        }

        public Counselling RetrieveCounselling(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCounselling(organisationId, id, p => true);
        }

        public PagedResult<Admission> RetrieveAdmissions(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissions(organisationId, p => true, orderBy, paging);
        }

        public Admission RetrieveAdmission(int organisationId, int admissionId, Expression<Func<Admission, bool>> predicate)
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
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
        }

        public Mobilization UpdateMobilization(int organisationId, Mobilization mobilization)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilization);
        }

        public FollowUp UpdateFollowUp(int organisationId, FollowUp followUp)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
        }

        public Centre UpdateCentre(int organisationId, Centre centre)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centre);
        }

        public Counselling UpdateCounselling(int organisationId, Counselling counselling)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
        }

        public Admission UpdateAdmission(int organisationId, Admission admission)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, admission);
        }

        public CommercialAdmission UpdateCommercialAdmission(int organisationId, CommercialAdmission commercialAdmission)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, commercialAdmission);
        }

        public GovernmentAdmission UpdateGovernmentAdmission(int organisationId, GovernmentAdmission governmentAdmission)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, governmentAdmission);
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

        public void MarkAsReadFollowUp(int organisationId, int id)
        {
            var data = RetrieveFollowUp(organisationId, id);
            data.ReadDateTime = _today;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, data);
        }

        public PagedResult<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiries(organisationId, p => true, orderBy, paging);
        }
        #endregion
    }
}
