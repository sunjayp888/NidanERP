using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nidan.Business.Enum;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Business.Models;
using Nidan.Data.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using PaymentMode = Nidan.Entity.PaymentMode;


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

        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month,
            DateTime.UtcNow.Date.Day, 0, 0, 0);

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

        public Batch CreateBatch(int organisationId, Batch batch, BatchDay batchDays, List<int> trainerIds)
        {
            var data = _nidanDataService.CreateBatch(organisationId, batch);
            CreateBatchDay(organisationId, data.BatchId, data.CentreId, batchDays);
            CreateBatchTrainer(organisationId, data.CentreId, data.BatchId, trainerIds);
            return data;
        }

        private void CreateBatchTrainer(int organisationId, int centreId, int batchId, List<int> trainerIds)
        {

            //Create Department Employment
            var batchTrainer = trainerIds.Select(item => new BatchTrainer()
            {
                OrganisationId = organisationId,
                BatchId = batchId,
                TrainerId = item,
                CentreId = centreId
            }).ToList();
            _nidanDataService.Create<BatchTrainer>(organisationId, batchTrainer);

        }

        public BatchDay CreateBatchDay(int organisationId, BatchDay batchDay)
        {
            var data = _nidanDataService.CreateBatchDay(organisationId, batchDay);
            return data;
        }

        private BatchDay CreateBatchDay(int organisationId, int batchId, int centreId, BatchDay batchDay)
        {
            var batchDayData = new BatchDay()
            {
                BatchId = batchId,
                IsMonday = batchDay.IsMonday,
                IsTuesday = batchDay.IsTuesday,
                IsWednesday = batchDay.IsWednesday,
                IsThursday = batchDay.IsThursday,
                IsFriday = batchDay.IsFriday,
                IsSaturday = batchDay.IsSaturday,
                IsSunday = batchDay.IsSunday,
                OrganisationId = organisationId,
                CentreId = centreId
            };
            _nidanDataService.Create(organisationId, batchDayData);
            return batchDay;
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
                Title = mobilization.Title,
                FirstName = mobilization.FirstName,
                MiddleName = mobilization.MiddleName,
                LastName = mobilization.LastName,
                IntrestedCourseId = mobilization.InterestedCourseId,
                Mobile = mobilization.Mobile,
                AlternateMobile = mobilization.AlternateMobile,
                CreatedDateTime = DateTime.UtcNow.Date,
                FollowUpType = "Mobilization",
                Close = "No",
                FollowUpUrl = string.Format("/Mobilization/Edit/{0}", data.MobilizationId),
                ReadDateTime = _today.AddYears(-100)
            };
            var followUpData = _nidanDataService.Create<FollowUp>(organisationId, followUp);
            var followUpHistory = new FollowUpHistory
            {
                FollowUpId = followUpData.FollowUpId,
                FollowUpType = followUpData.FollowUpType,
                Remarks = followUpData.Remark,
                Close = followUpData.Close,
                ClosingRemarks = followUpData.ClosingRemark,
                FollowUpDate = followUpData.FollowUpDateTime.Date,
                CreatedDate = DateTime.UtcNow.Date,
                CentreId = followUpData.CentreId,
                OrganisationId = organisationId
            };
            _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            return data;
        }

        public Enquiry CreateEnquiry(int organisationId, int personnelId, Enquiry enquiry, List<int> courseIds)
        {
            var data = _nidanDataService.Create<Enquiry>(organisationId, enquiry);
            //Update student code
            data.StudentCode = GenerateStudentCode(organisationId, data.EnquiryId, enquiry.CentreId);
            _nidanDataService.UpdateEntityEntry(data);
            //Create Counselling
            // CreateCounselling(organisationId, personnelId, data, courseIds.FirstOrDefault());
            //Create FollowUp
            CreateFollowUp(organisationId, data);
            //Create EnquiryCourse
            CreateEnquiryCourse(organisationId, enquiry.CentreId, data.EnquiryId, courseIds);
            return data;
        }

        private void CreateEnquiryCourse(int organisationId, int centreId, int enquiryId, List<int> couserIds)
        {
            var enquiryCourses = RetrieveEnquiryCourses(organisationId, centreId, enquiryId).ToList();
            var enquiryCourseList = new List<EnquiryCourse>();
            foreach (var item in couserIds)
            {
                if (!enquiryCourses.Any(e => e.CourseId == item && e.EnquiryId == enquiryId))
                {
                    enquiryCourseList.Add(new EnquiryCourse()
                    {
                        OrganisationId = organisationId,
                        CentreId = centreId,
                        EnquiryId = enquiryId,
                        CourseId = item,
                    });
                }
            }
            if (enquiryCourseList.Any())
                _nidanDataService.Create<EnquiryCourse>(organisationId, enquiryCourseList);
        }

        private void CreateCounselling(int organisationId, int personnelId, Enquiry enquiry, int courseId)
        {
            var conselling = new Counselling()
            {
                Title = enquiry.Title,
                FirstName = enquiry.FirstName,
                MiddleName = enquiry.MiddleName,
                LastName = enquiry.LastName,
                CentreId = enquiry.CentreId,
                CourseOfferedId = courseId,
                EnquiryId = enquiry.EnquiryId,
                FollowUpDate = _today.AddDays(2),
                OrganisationId = organisationId,
                PersonnelId = personnelId,
                ConversionProspect = enquiry.ConversionProspect,
                SectorId = enquiry.SectorId,
                Close = enquiry.Close
            };
            _nidanDataService.Create<Counselling>(organisationId, conselling);
        }

        private void CreateFollowUp(int organisationId, Enquiry enquiry)
        {
            var followUp = new FollowUp
            {
                CentreId = enquiry.CentreId,
                FollowUpDateTime = enquiry.FollowUpDate.Value.Date,
                EnquiryId = enquiry.EnquiryId,
                Remark = enquiry.Remarks,
                Title = enquiry.Title,
                FirstName = enquiry.FirstName,
                MiddleName = enquiry.MiddleName,
                LastName = enquiry.LastName,
                IntrestedCourseId = enquiry.IntrestedCourseId,
                Mobile = enquiry.Mobile,
                CreatedDateTime = DateTime.UtcNow.Date,
                FollowUpType = "Enquiry",
                FollowUpUrl = string.Format("/Enquiry/Edit/{0}", enquiry.EnquiryId),
                AlternateMobile = enquiry.AlternateMobile,
                Close = "No",
                ReadDateTime = _today.AddYears(-100)
            };
            var followUpData = _nidanDataService.Create<FollowUp>(organisationId, followUp);
            var followUpHistory = new FollowUpHistory
            {
                FollowUpId = followUpData.FollowUpId,
                FollowUpType = followUpData.FollowUpType,
                Remarks = followUpData.Remark,
                Close = followUpData.Close,
                ClosingRemarks = followUpData.ClosingRemark,
                FollowUpDate = followUpData.FollowUpDateTime.Date,
                CreatedDate = DateTime.UtcNow.Date,
                CentreId = followUpData.CentreId,
                OrganisationId = organisationId
            };
            _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
        }

        private string GenerateStudentCode(int organisationId, int enquiryId, int centreId)
        {
            var centre = RetrieveCentre(organisationId, centreId, e => true);
            return enquiryId.ToString(); //string.Format("{0}{1}", centre.Name.Substring(0, 3), enquiryId);
        }

        public void UploadMobilization(int organisationId, int centreId, int eventId, int personnelId,
            DateTime generateDateTime, List<Mobilization> mobilizations)
        {
            var interestedCourses = RetrieveCourses(organisationId, c => true);
            var qualifications = RetrieveQualifications(organisationId, q => true);
            var mobilizationType =
                RetrieveMobilizationTypes(organisationId, e => e.Name == "Event").FirstOrDefault();
            var followUpList = new List<FollowUp>();
            var followUpHistoryList = new List<FollowUpHistory>();
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
                    Title = item.Title,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
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
                    Title = item.Title,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    IntrestedCourseId = interestedCourseId,
                    Mobile = item.Mobile,
                    CreatedDateTime = DateTime.UtcNow.Date,
                    ReadDateTime = _today.AddYears(-100),
                    Close = "No",
                    FollowUpType = "Mobilization",
                    FollowUpUrl = string.Format("/Mobilization/Edit/{0}", data.MobilizationId)
                });
            }
             _nidanDataService.Create<FollowUp>(organisationId, followUpList);
            foreach (var item in followUpList)
            {
                var followUpHistory = new FollowUpHistory
                {
                    FollowUpId = item.FollowUpId,
                    FollowUpType = item.FollowUpType,
                    Remarks = item.Remark,
                    Close = item.Close,
                    ClosingRemarks = item.ClosingRemark,
                    FollowUpDate = item.FollowUpDateTime,
                    CreatedDate = DateTime.UtcNow.Date,
                    CentreId = centreId,
                    OrganisationId = organisationId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }
        }

        public void UploadSession(int organisationId, List<Session> session)
        {
            var subjects = RetrieveSubjects(organisationId, e => true);
            var courseTypes = RetrieveCourseTypes(organisationId, e => true);
            foreach (var item in session)
            {
                var subjectId =
                    subjects.FirstOrDefault(e => e.Name.Trim().ToLower() == item.SubjectName.Trim().ToLower())?
                        .SubjectId ?? subjects.First(e => e.Name.ToLower() == "others").SubjectId;

                var courseTypeId =
                    courseTypes.FirstOrDefault(e => e.Name.Trim().ToLower() == item.CourseTypeName.Trim().ToLower())?
                        .CourseTypeId ?? courseTypes.First(e => e.Name.ToLower() == "others").CourseTypeId;

                var data = new Session
                {
                    Name = item.Name,
                    Duration = item.Duration,
                    CourseTypeId = courseTypeId,
                    Description = item.Description,
                    SubjectId = subjectId,
                    OrganisationId = organisationId
                };
                _nidanDataService.CreateSession(organisationId, data);
            }
        }

        public Counselling CreateCounselling(int organisationId, Counselling counselling)
        {
            var enquiryData = RetrieveEnquiry(organisationId, counselling.EnquiryId);
            counselling.Title = enquiryData.Title;
            counselling.FirstName = enquiryData.FirstName;
            counselling.MiddleName = enquiryData.MiddleName;
            counselling.LastName = enquiryData.LastName;
            var data = _nidanDataService.CreateCounselling(organisationId, counselling);
            var followUp = RetrieveFollowUps(organisationId, e => e.EnquiryId == data.EnquiryId).Items.FirstOrDefault();
            if (followUp != null)
            {
                followUp.Remark = data.Remarks;
                followUp.Title = enquiryData.Title;
                followUp.FirstName = enquiryData.FirstName;
                followUp.MiddleName = enquiryData.MiddleName;
                followUp.LastName = enquiryData.LastName;
                followUp.IntrestedCourseId = data.CourseOfferedId;
                followUp.Mobile = enquiryData.Mobile;
                followUp.FollowUpType = "Counselling";
                followUp.FollowUpUrl = string.Format("/Counselling/Edit/{0}", data.CounsellingId);
                followUp.CounsellingId = data.CounsellingId;
                var followUpData = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
                var followUpHistory = new FollowUpHistory
                {
                    FollowUpId = followUpData.FollowUpId,
                    FollowUpType = followUpData.FollowUpType,
                    Remarks = followUpData.Remark,
                    Close = followUpData.Close,
                    ClosingRemarks = followUpData.ClosingRemark,
                    FollowUpDate = counselling.FollowUpDate ?? followUpData.FollowUpDateTime.Date ,
                    CreatedDate = DateTime.UtcNow.Date,
                    CentreId = followUpData.CentreId,
                    OrganisationId = organisationId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }
            enquiryData.EnquiryStatus = "Counselling";
            enquiryData.IsCounsellingDone = true;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryData);
            return data;
        }

        //public RegistrationPaymentReceipt CreateRegistrationPaymentReceipt(int organisationId, RegistrationPaymentReceipt registrationPaymentReceipt)
        //{
        //    var enquirydata = RetrieveEnquiry(organisationId, registrationPaymentReceipt.EnquiryId);
        //    enquirydata.SectorId = registrationPaymentReceipt.Enquiry.SectorId;
        //    enquirydata.IntrestedCourseId = registrationPaymentReceipt.Enquiry.IntrestedCourseId;
        //    enquirydata.BatchTimePreferId = registrationPaymentReceipt.Enquiry.BatchTimePreferId;
        //    var counsellingdata = _nidanDataService.RetrieveCounsellings(organisationId, e => e.EnquiryId == registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
        //    if (counsellingdata != null)
        //    {
        //        var course = _nidanDataService.RetrieveCourse(organisationId, counsellingdata.CourseOfferedId, e => true);
        //        registrationPaymentReceipt.Particulars = string.Format(registrationPaymentReceipt.Fees + " Rupees Paid Against " + course.Name);
        //        registrationPaymentReceipt.CourseId = counsellingdata.CourseOfferedId;
        //        registrationPaymentReceipt.CounsellingId = counsellingdata.CounsellingId;
        //    }
        //    registrationPaymentReceipt.FollowUpDate = registrationPaymentReceipt.FollowUpDate ?? DateTime.Now.AddDays(2);
        //    registrationPaymentReceipt.FinancialYear = "2016-2017";

        //    var data = _nidanDataService.CreateRegistrationPaymentReceipt(organisationId, registrationPaymentReceipt);

        //    enquirydata.IsRegistrationDone = data != null;
        //    enquirydata.EnquiryStatus = "Registration";
        //    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquirydata);
        //    if (counsellingdata != null) counsellingdata.IsRegistrationDone = data != null;
        //    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counsellingdata);

        //    var registrationFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
        //    if (registrationFollowUp != null)
        //    {
        //        registrationFollowUp.RegistrationtId = data?.RegistrationId;
        //        registrationFollowUp.Remark = data?.Remarks;
        //        registrationFollowUp.FollowUpDateTime = registrationPaymentReceipt.FollowUpDate ?? DateTime.Now.AddDays(2);
        //        registrationFollowUp.FollowUpUrl = string.Format("/Registration/Edit/{0}", data?.EnquiryId);
        //        registrationFollowUp.FollowUpType = "Registration";
        //    }

        //    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationFollowUp);
        //    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquirydata);
        //    return data;
        //}

        public Enquiry CreateEnquiryFromMobilization(int organisationId, int centreId, int mobilizationId)
        {
            //var followUp = RetrieveFollowUps(organisationId, e => e.MobilizationId == mobilizationId).Items.FirstOrDefault();
            var mobilization = RetrieveMobilization(organisationId, mobilizationId);
            //Delete follow Up
            //_nidanDataService.Delete<FollowUp>(organisationId, e => e.FollowUpId == followUp.FollowUpId);
            return new Enquiry
            {
                OrganisationId = organisationId,
                Title = mobilization == null ? string.Empty : mobilization.Title,
                FirstName = mobilization == null ? string.Empty : mobilization.FirstName,
                MiddleName = mobilization == null ? string.Empty : mobilization.MiddleName,
                LastName = mobilization == null ? string.Empty : mobilization.LastName,
                Mobile = mobilization.Mobile,
                AlternateMobile = mobilization.AlternateMobile,
                EducationalQualificationId = mobilization.QualificationId,
                Address1 = mobilization?.StudentLocation ?? string.Empty,
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

        public Trainer CreatetTrainer(int organisationId, Trainer trainer)
        {
            var data = _nidanDataService.Create<Trainer>(organisationId, trainer);
            //var personnel = new Personnel()
            //{
            //    OrganisationId = organisationId,
            //    DOB = DateTime.Today,
            //    Title = "Mr",
            //    Forenames = data.Name,
            //    Surname = "Surname",
            //    Email = data.EmailId,
            //    Address1 = "Address1",
            //    Postcode = "POST CODE",
            //    Telephone = "12345678",
            //    NINumber = "NZ1234567",
            //    CentreId = data.CentreId
            //};
            //_nidanDataService.CreatePersonnel(organisationId, personnel);

            //trainer.PersonnelId = personnel.PersonnelId;
            //_nidanDataService.UpdateOrganisationEntityEntry(organisationId, trainer);

            return data;
        }

        public FollowUp CreateFollowUp(int organisationId, FollowUp followUp)
        {
            return _nidanDataService.CreateFollowUp(organisationId, followUp);
        }

        public Subject CreateSubject(int organisationId, Subject subject, List<int> courseIds, List<int> trainerIds)
        {

            var data = _nidanDataService.Create<Subject>(organisationId, subject);
            CreateSubjectCourse(organisationId,data.SubjectId, courseIds);
            CreateSubjectTrainer(organisationId, data.SubjectId, trainerIds);
            return data;
        }

        private void CreateSubjectCourse(int organisationId, int subjectId, List<int> couserIds)
        {
            var subjectCourses = RetrieveSubjectCourses(organisationId, subjectId).ToList();
            var subjectCourseList = new List<SubjectCourse>();
            foreach (var item in couserIds)
            {
                if (!subjectCourses.Any(e => e.CourseId == item && e.SubjectId == subjectId))
                {
                    subjectCourseList.Add(new SubjectCourse()
                    {
                        OrganisationId = organisationId,
                        SubjectId = subjectId,
                        CourseId = item,
                    });
                }
            }
            if (subjectCourseList.Any())
                _nidanDataService.Create<SubjectCourse>(organisationId, subjectCourseList);
        }

        private void CreateSubjectTrainer(int organisationId, int subjectId, List<int> trainerIds)
        {
            var subjectTrainers = RetrieveSubjectTrainers(organisationId, subjectId).ToList();
            var subjectTrainerList = new List<SubjectTrainer>();
            foreach (var item in trainerIds)
            {
                if (!subjectTrainers.Any(e => e.TrainerId == item && e.SubjectId == subjectId))
                {
                    subjectTrainerList.Add(new SubjectTrainer()
                    {
                        OrganisationId = organisationId,
                        SubjectId = subjectId,
                        TrainerId = item,
                    });
                }
            }
            if (subjectTrainerList.Any())
                _nidanDataService.Create<SubjectTrainer>(organisationId, subjectTrainerList);
        }

        public Session CreateSession(int organisationId, Session session)
        {
            return _nidanDataService.CreateSession(organisationId, session);
        }

        public Room CreateRoom(int organisationId, Room room)
        {
            return _nidanDataService.CreateRoom(organisationId, room);
        }

        public EnquiryCourse CreateEnquiryCourse(int organisationId, EnquiryCourse enquiryCourse)
        {
            return _nidanDataService.Create<EnquiryCourse>(organisationId, enquiryCourse);
        }

        public SubjectCourse CreateSubjectCourse(int organisationId, SubjectCourse subjectCourse)
        {
            return _nidanDataService.Create<SubjectCourse>(organisationId, subjectCourse);
        }

        public SubjectTrainer CreateSubjectTrainer(int organisationId, SubjectTrainer subjectTrainer)
        {
            return _nidanDataService.Create<SubjectTrainer>(organisationId, subjectTrainer);
        }

        public BatchTrainer CreateBatchTrainer(int organisationId, BatchTrainer batchTrainer)
        {
            return _nidanDataService.Create<BatchTrainer>(organisationId, batchTrainer);
        }

        public CentreCourse CreateCentreCourse(int organisationId, int centreId, int courseId)
        {
            var centreCourse = new CentreCourse()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                CourseId = courseId
            };
            return _nidanDataService.Create<CentreCourse>(organisationId, centreCourse);
        }

        public CentreCourseInstallment CreateCentreCourseInstallment(int organisationId, int centreId,
            int courseInstallmentId)
        {
            var centreCourseInstallment = new CentreCourseInstallment()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                CourseInstallmentId = courseInstallmentId
            };
            return _nidanDataService.Create<CentreCourseInstallment>(organisationId, centreCourseInstallment);
        }

        public CentreScheme CreateCentreScheme(int organisationId, int centreId, int schemeId)
        {
            var centreScheme = new CentreScheme()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                SchemeId = schemeId
            };
            return _nidanDataService.Create<CentreScheme>(organisationId, centreScheme);
        }

        public CentreSector CreateCentreSector(int organisationId, int centreId, int sectorId)
        {
            var centreSector = new CentreSector()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                SectorId = sectorId
            };
            return _nidanDataService.Create<CentreSector>(organisationId, centreSector);
        }

        public Admission CreateAdmission(int organisationId, int centreId, int personnelId, Admission admission,
            CandidateFee candidateFee)
        {
            var registrationData = RetrieveRegistration(organisationId, admission.RegistrationId);
            var enquiryData = RetrieveEnquiry(organisationId, registrationData.EnquiryId);
            var candidatInstallment = RetrieveCandidateInstallment(organisationId,
                registrationData.CandidateInstallmentId, e => true);
            var courseInstallment = RetrieveCourseInstallment(organisationId, registrationData.CourseInstallmentId);
            candidatInstallment.NumberOfInstallment = courseInstallment.NumberOfInstallment;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidatInstallment);
            var candidatefeeCreate = new CandidateFee()
            {
                PaymentDate = DateTime.UtcNow,
                CandidateInstallmentId = candidatInstallment.CandidateInstallmentId,
                PaidAmount = candidateFee.PaidAmount,
                PaymentModeId = candidateFee.PaymentModeId,
                FeeTypeId = (int)FeeType.Admission,
                ChequeNumber = candidateFee.ChequeNumber,
                ChequeDate = candidateFee.ChequeDate,
                BankName = candidateFee.BankName,
                Penalty = null,
                StudentCode = registrationData.StudentCode,
                FiscalYear = DateTime.Now.FiscalYear(),
                CentreId = centreId,
                OrganisationId = organisationId,
                PersonnelId = personnelId,
                IsPaymentDone = true
            };
            _nidanDataService.Create<CandidateFee>(organisationId, candidatefeeCreate);
            CreateCandidateFee(organisationId, centreId, personnelId, admission);

            admission.OrganisationId = organisationId;
            admission.CentreId = centreId;
            admission.AdmissionDate = DateTime.UtcNow;
            var admissionData = _nidanDataService.CreateAdmission(organisationId, admission);
            // Update Registration IsAdmissionDone
            var registration = RetrieveRegistration(organisationId, centreId, admission.RegistrationId);
            registration.IsAdmissionDone = true;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registration);
            // EnquiryStatus Update
            enquiryData.Close = "Yes";
            enquiryData.ClosingRemark = "Admission Done";
            enquiryData.IsAdmissionDone = true;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryData);
            //FollowUp Update
            var followup = RetrieveFollowUps(organisationId, e => e.EnquiryId == enquiryData.EnquiryId)
                .Items.FirstOrDefault();
            if (followup != null)
            {
                followup.FollowUpType = "Admission";
                followup.Close = "Yes";
                followup.ClosingRemark = "Admission Done";
                var followUpData = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followup);
                var followUpHistory = new FollowUpHistory
                {
                    FollowUpId = followUpData.FollowUpId,
                    FollowUpType = followUpData.FollowUpType,
                    Remarks = followUpData.Remark,
                    Close = followUpData.Close,
                    ClosingRemarks = followUpData.ClosingRemark,
                    FollowUpDate = followUpData.FollowUpDateTime.Date,
                    CreatedDate = DateTime.UtcNow.Date,
                    CentreId = followUpData.CentreId,
                    OrganisationId = organisationId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }
            return admissionData;
        }

        private void CreateCandidateFee(int organisationId, int centreId, int personnelId, Admission admission)
        {
            var candidateInstallment = RetrieveCandidateInstallment(organisationId,
                admission.Registration.CandidateInstallmentId, c => true);
            var feePaymentMethod = admission.Registration.CandidateInstallment.PaymentMethod;
            var candidateBatch = RetrieveBatch(organisationId, admission.BatchId??0);
            // Update data in CandidateInstallment
            candidateInstallment.PaymentMethod = feePaymentMethod;
            candidateInstallment.NumberOfInstallment = feePaymentMethod ==
                                                       FeePaymentMethod.MonthlyInstallment.ToString()
                ? candidateBatch.NumberOfInstallment
                : 1;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateInstallment);

            var candidateFees = new List<CandidateFee>();

            for (int i = 1; i <= candidateInstallment.NumberOfInstallment; i++)
            {
                candidateFees.Add(new CandidateFee
                {
                    CandidateInstallmentId = candidateInstallment.CandidateInstallmentId,
                    PaymentModeId = (int)Enum.PaymentMode.Cash,
                    FeeTypeId = (int)FeeType.Installment,
                    FollowUpDate = candidateBatch.BatchStartDate.AddMonths(candidateBatch.NumberOfInstallment),
                    FiscalYear = DateTime.UtcNow.FiscalYear(),
                    InstallmentAmount =
                        (candidateInstallment.CourseFee - candidateInstallment.DownPayment) /
                        candidateBatch.NumberOfInstallment,
                    CentreId = centreId,
                    OrganisationId = organisationId,
                    PersonnelId = personnelId,
                    IsPaymentDone = false
                });
            }
            _nidanDataService.Create<CandidateFee>(organisationId, candidateFees);
        }

        public CandidateFee CreateCandidateFee(int organisationId, CandidateFee candidateFee)
        {
            return _nidanDataService.CreateCandidateFee(organisationId, candidateFee);
        }

        public FollowUpHistory CreateFollowUpHistory(int organisationId, FollowUpHistory followUpHistory)
        {
            return _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
        }

        public Registration CreateCandidateRegistration(int organisationId, int centreId, int personnelId, string studentCode,
            Registration registration)
        {
            registration.CourseInstallment.CourseInstallmentId = registration.CourseInstallmentId;
            var candidateInstallmentData = CandidateInstallment(organisationId, centreId, studentCode,
                registration?.CandidateInstallment, registration?.CourseInstallment);
            registration.CandidateFee.CandidateInstallmentId = candidateInstallmentData.CandidateInstallmentId;
            registration.CandidateInstallmentId = candidateInstallmentData.CandidateInstallmentId;
            var candidateFeeData = CandidateFee(organisationId, centreId, personnelId, studentCode,
                candidateInstallmentData.CandidateInstallmentId, registration?.CandidateFee);

            return CandidateRegistration(organisationId, centreId, studentCode, registration,
                candidateFeeData.CandidateFeeId);
        }

        private CandidateInstallment CandidateInstallment(int organisationId, int centreId, string studentCode,
            CandidateInstallment candidateInstallment, CourseInstallment courseInstallment)
        {
            if (candidateInstallment.IsTotalAmountDiscount)
            {
                var candidateInstallmentData = new CandidateInstallment
                {
                    StudentCode = studentCode,
                    CentreId = centreId,
                    CourseFee = courseInstallment.Fee - candidateInstallment.DiscountAmount,
                    OrganisationId = organisationId,
                    DiscountAmount = candidateInstallment.DiscountAmount,
                    LumpsumAmount = candidateInstallment.LumpsumAmount,
                    IsTotalAmountDiscount = candidateInstallment.IsTotalAmountDiscount,
                    DownPayment = courseInstallment.DownPayment,
                    NumberOfInstallment = candidateInstallment.NumberOfInstallment,
                    CourseInstallmentId = courseInstallment.CourseInstallmentId,
                    PaymentMethod = candidateInstallment.PaymentMethod
                };
                return _nidanDataService.Create<CandidateInstallment>(organisationId, candidateInstallmentData);
            }
            else
            {
                var candidateInstallmentData = new CandidateInstallment()
                {
                    StudentCode = studentCode,
                    CentreId = centreId,
                    CourseFee = courseInstallment.Fee,
                    OrganisationId = organisationId,
                    DiscountAmount = candidateInstallment.DiscountAmount,
                    LumpsumAmount = courseInstallment.LumpsumAmount,
                    IsTotalAmountDiscount = candidateInstallment.IsTotalAmountDiscount,
                    DownPayment = courseInstallment.DownPayment,
                    NumberOfInstallment = courseInstallment.NumberOfInstallment,
                    CourseInstallmentId = courseInstallment.CourseInstallmentId
                };
                return _nidanDataService.Create<CandidateInstallment>(organisationId, candidateInstallmentData);
            }
        }

        private Registration CandidateRegistration(int organisationId, int centreId, string studentCode,
            Registration registration, int candidateFeeId)
        {
            var registrationData = new Registration()
            {
                CandidateFeeId = candidateFeeId,
                CentreId = centreId,
                CourseId = registration.CourseId,
                CourseInstallmentId = registration.CourseInstallmentId,
                CandidateInstallmentId = registration.CandidateInstallmentId,
                EnquiryId = registration.EnquiryId,
                FollowupDate = registration.FollowupDate,
                StudentCode = studentCode,
                Remarks = registration.Remarks,
                OrganisationId = organisationId,
                RegistrationDate = registration.RegistrationDate
            };
            var data = _nidanDataService.Create<Registration>(organisationId, registrationData);
            var enquiry = RetrieveEnquiry(organisationId, registration.EnquiryId);
            enquiry.IsRegistrationDone = true;
            enquiry.EnquiryStatus = "Registration";
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            var counselling =
                RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            if (counselling != null)
            {
                counselling.IsRegistrationDone = true;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }
            var followUp = RetrieveFollowUps(organisationId, e => e.EnquiryId == data.EnquiryId).Items.FirstOrDefault();
            if (followUp != null)
            {
                followUp.RegistrationId = data.RegistrationId;
                followUp.FollowUpType = "Registration";
                followUp.FollowUpUrl = string.Format("/Registration/Edit/{0}", data?.RegistrationId);
                var followUpData = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
                var followUpHistory = new FollowUpHistory
                {
                    FollowUpId = followUpData.FollowUpId,
                    FollowUpType = followUpData.FollowUpType,
                    Remarks = followUpData.Remark,
                    Close = followUpData.Close,
                    ClosingRemarks = followUpData.ClosingRemark,
                    FollowUpDate = registration.FollowupDate ?? followUpData.FollowUpDateTime.Date,
                    CreatedDate = DateTime.UtcNow.Date,
                    CentreId = followUpData.CentreId,
                    OrganisationId = organisationId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }
            return data;
        }

        private CandidateFee CandidateFee(int organisationId, int centreId, int personnelId, string studentCode,
            int? candidateInstallmentId, CandidateFee candidateFee)
        {
            var candidateFeeData = new CandidateFee()
            {
                CentreId = centreId,
                OrganisationId = organisationId,
                BankName = candidateFee.BankName,
                BalanceInstallmentAmount = candidateFee.BalanceInstallmentAmount,
                CandidateInstallmentId = candidateInstallmentId,
                ChequeDate = candidateFee.ChequeDate,
                ChequeNumber = candidateFee.ChequeNumber,
                FeeTypeId = (int)FeeType.Registration,
                PaidAmount = candidateFee.PaidAmount,
                IsPaymentDone = true,
                PaymentDate = DateTime.Now,
                StudentCode = studentCode,
                PaymentModeId = candidateFee.PaymentModeId,
                FiscalYear = DateTime.UtcNow.FiscalYear(),
                PersonnelId = personnelId
            };
            return _nidanDataService.Create<CandidateFee>(organisationId, candidateFeeData);
        }

        //public CandidateInstallment CreateCandidateInstallment(int organisationId, CandidateInstallment candidateInstallment)
        //{
        //    return _nidanDataService.Create<CandidateInstallment>(organisationId, candidateInstallment);
        //}

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

        public PagedResult<Mobilization> RetrieveMobilizations(int organisationId,
            Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null,
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
                        at =>
                            at.FirstName.ToLower() == name.Trim().ToLower() &&
                            at.LastName.ToLower() == name.Trim().ToLower() && at.EnquiryId != (enquiryId ?? -1))
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

        public List<CourseType> RetrieveCourseTypes(int organisationId, Expression<Func<CourseType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<CourseType>(organisationId, predicate);
        }

        public List<RoomType> RetrieveRoomTypes(int organisationId, Expression<Func<RoomType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<RoomType>(organisationId, predicate);
        }

        public List<Trainer> RetrieveTrainers(int organisationId, Expression<Func<Trainer, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Trainer>(organisationId, predicate);
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
            return _nidanDataService.Retrieve<Enquiry>(organisationId, predicate);
        }

        public List<State> RetrieveStates(int organisationId, Expression<Func<State, bool>> predicate)
        {
            return _nidanDataService.Retrieve<State>(organisationId, e => true);
        }

        public List<District> RetrieveDistricts(int organisationId, Expression<Func<District, bool>> predicate)
        {
            return _nidanDataService.Retrieve<District>(organisationId, predicate);
        }

        public List<Taluka> RetrieveTalukas(int organisationId, Expression<Func<Taluka, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Taluka>(organisationId, predicate);
        }


        public List<EventFunctionType> RetrieveEventFunctionTypes(int organisationId,
            Expression<Func<EventFunctionType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<EventFunctionType>(organisationId, e => true);
        }

        public List<PaymentMode> RetrievePaymentModes(int organisationId, Expression<Func<PaymentMode, bool>> predicate)
        {
            return _nidanDataService.Retrieve<PaymentMode>(organisationId, e => true);
        }

        public List<CourseInstallment> RetrieveCourseInstallments(int organisationId,
            Expression<Func<CourseInstallment, bool>> predicate)
        {
            return _nidanDataService.Retrieve<CourseInstallment>(organisationId, predicate);
        }

        public List<Room> RetrieveRooms(int organisationId, Expression<Func<Room, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Room>(organisationId, predicate);
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

        public PagedResult<Enquiry> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiryBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public List<MobilizationType> RetrieveMobilizationTypes(int organisationId,
            Expression<Func<MobilizationType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<MobilizationType>(organisationId, predicate);
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCentres(organisationId, p => true, orderBy, paging);
        }

        public PagedResult<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatches(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<Mobilization, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizationBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            return _nidanDataService.RetrieveCentres(organisationId, e => true).Items.ToList();
        }

        public PagedResult<Counselling> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellingBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public Brainstorming RetrieveBrainstorming(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBrainstorming(organisationId, id, p => true);
        }

        public Brainstorming RetrieveBrainstorming(int organisationId, int brainstormingId,
            Expression<Func<Brainstorming, bool>> predicate)
        {
            var brainstorming = _nidanDataService.RetrieveBrainstorming(organisationId, brainstormingId, p => true);
            return brainstorming;
        }

        public PagedResult<Brainstorming> RetrieveBrainstormings(int organisationId,
            Expression<Func<Brainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
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

        public PagedResult<Planning> RetrievePlannings(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
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

        public PagedResult<Budget> RetrieveBudgets(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
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

        public PagedResult<Eventday> RetrieveEventdays(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            //  return _nidanDataService.RetrieveEventdays(organisationId, p => true, orderBy, paging);
            return null;
        }

        public Postevent RetrievePostevent(int organisationId, int id)
        {
            return _nidanDataService.RetrievePostevent(organisationId, id, p => true);
        }

        public Postevent RetrievePostevent(int organisationId, int posteventId,
            Expression<Func<Postevent, bool>> predicate)
        {
            var postevent = _nidanDataService.RetrievePostevent(organisationId, posteventId, p => true);
            return postevent;
        }

        public PagedResult<Postevent> RetrievePostevents(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            // return _nidanDataService.RetrievePostevents(organisationId, p => true, orderBy, paging);
            return null;
        }

        public PagedResult<Registration> RetrieveRegistrations(int organisationId,
            Expression<Func<Registration, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveRegistrations(organisationId, predicate, orderBy, paging);
        }

        public Trainer RetrieveTrainer(int organisationId, int id)
        {
            return _nidanDataService.RetrieveTrainer(organisationId, id, p => true);
        }

        public PagedResult<Trainer> RetrieveTrainers(int organisationId, Expression<Func<Trainer, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveTrainers(organisationId, predicate, orderBy, paging);
        }

        public Trainer RetrieveTrainer(int organisationId, int trainerId, Expression<Func<Trainer, bool>> predicate)
        {
            var trainer = _nidanDataService.RetrieveTrainer(organisationId, trainerId, p => true);
            return trainer;
        }

        public PagedResult<Trainer> RetrieveTrainerBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<Trainer, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveTrainerBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
        }

        public PagedResult<Holiday> RetrieveHolidays(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveHolidays(organisationId, p => true, orderBy, paging);
        }

        public Holiday RetrieveHoliday(int organisationId, int holidayId, Expression<Func<Holiday, bool>> predicate)
        {
            var holiday = _nidanDataService.RetrieveHoliday(organisationId, holidayId, p => true);
            return holiday;
        }

        public Holiday RetrieveHoliday(int organisationId, int id)
        {
            return _nidanDataService.RetrieveHoliday(organisationId, id, p => true);
        }

        public Course RetrieveCourse(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCourse(organisationId, id, p => true);
        }

        public PagedResult<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCourses(organisationId, predicate, orderBy, paging);
        }

        public Course RetrieveCourse(int organisationId, int courseId, Expression<Func<Course, bool>> predicate)
        {
            return _nidanDataService.RetrieveCourse(organisationId, courseId, p => true);
        }

        public PagedResult<Course> RetrieveCourseBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<Course, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCourseBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
        }

        public PagedResult<CourseInstallment> RetrieveCourseInstallments(int organisationId,
            Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCourseInstallments(organisationId, predicate, orderBy, paging);
        }

        public CourseInstallment RetrieveCourseInstallment(int organisationId, int courseInstallmentId,
            Expression<Func<CourseInstallment, bool>> predicate)
        {
            var courseInstallment = _nidanDataService.RetrieveCourseInstallment(organisationId, courseInstallmentId,
                p => true);
            return courseInstallment;
        }

        public CourseInstallment RetrieveCourseInstallment(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCourseInstallment(organisationId, id, p => true);
        }

        public PagedResult<CourseInstallment> RetrieveCourseInstallmentBySearchKeyword(int organisationId,
            string searchKeyword, Expression<Func<CourseInstallment, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCourseInstallmentBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public PagedResult<Subject> RetrieveSubjects(int organisationId, Expression<Func<Subject, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveSubjects(organisationId, predicate, orderBy, paging);
        }

        public Subject RetrieveSubject(int organisationId, int subjectId, Expression<Func<Subject, bool>> predicate)
        {
            var subject = _nidanDataService.RetrieveSubject(organisationId, subjectId, p => true);
            return subject;
        }

        public Subject RetrieveSubject(int organisationId, int id)
        {
            return _nidanDataService.RetrieveSubject(organisationId, id, p => true);
        }

        public PagedResult<Session> RetrieveSessions(int organisationId, Expression<Func<Session, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveSessions(organisationId, predicate, orderBy, paging);
        }

        public Session RetrieveSession(int organisationId, int sessionId, Expression<Func<Session, bool>> predicate)
        {
            var session = _nidanDataService.RetrieveSession(organisationId, sessionId, p => true);
            return session;
        }

        public Session RetrieveSession(int organisationId, int id)
        {
            return _nidanDataService.RetrieveSession(organisationId, id, p => true);
        }

        public List<Subject> RetrieveSubjects(int organisationId, Expression<Func<Subject, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Subject>(organisationId, predicate);
        }

        public PagedResult<Room> RetrieveRooms(int organisationId, Expression<Func<Room, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveRooms(organisationId, predicate, orderBy, paging);
        }

        public Room RetrieveRoom(int organisationId, int roomId, Expression<Func<Room, bool>> predicate)
        {
            var room = _nidanDataService.RetrieveRoom(organisationId, roomId, p => true);
            return room;
        }

        public Room RetrieveRoom(int organisationId, int id)
        {
            return _nidanDataService.RetrieveRoom(organisationId, id, p => true);
        }


        public BatchDay RetrieveBatchDay(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBatchDay(organisationId, id, p => true);
        }

        public PagedResult<BatchDay> RetrieveBatchDays(int organisationId, Expression<Func<BatchDay, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchDays(organisationId, predicate, orderBy, paging);
        }

        public BatchDay RetrieveBatchDay(int organisationId, int batchDayId, Expression<Func<BatchDay, bool>> predicate)
        {
            var batchDay = _nidanDataService.RetrieveBatchDay(organisationId, batchDayId, p => true);
            return batchDay;
        }

        public IEnumerable<EnquiryCourse> RetrieveEnquiryCourses(int organisationId, int centreId, int enquiryId)
        {
            return _nidanDataService.RetrieveEnquiryCourses(organisationId, centreId, enquiryId);
        }

        public IEnumerable<Course> RetrieveUnassignedCentreCourses(int organisationId, int centreId)
        {
            return
                _nidanDataService.RetrieveCourses(organisationId, a => !a.CentreCourses.Any(d => d.CentreId == centreId),
                    null, null).Items.ToList();
        }

        public PagedResult<CentreCourse> RetrieveCentreCourses(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreCourses(organisationId, centreId, orderBy, paging);
        }

        public IEnumerable<SubjectCourse> RetrieveSubjectCourses(int organisationId, int subjectId)
        {
            return _nidanDataService.RetrieveSubjectCourses(organisationId, subjectId);
        }

        public IEnumerable<SubjectTrainer> RetrieveSubjectTrainers(int organisationId, int subjectId)
        {
            return _nidanDataService.RetrieveSubjectTrainers(organisationId, subjectId);
        }


        public IEnumerable<BatchTrainer> RetrieveBatchTrainers(int organisationId, int batchId)
        {
            return _nidanDataService.RetrieveBatchTrainers(organisationId, batchId);
        }

        public IEnumerable<CourseInstallment> RetrieveUnassignedCentreCourseInstallments(int organisationId,
            int centreId)
        {

            return
                _nidanDataService.RetrieveCourseInstallments(organisationId,
                        a =>
                            !a.CentreCourseInstallments.Any(d => d.CentreId == centreId) &&
                            a.Course.CentreCourses.Any(e => e.CentreId == centreId)
                    )
                    .Items.ToList();
        }

        public PagedResult<CentreCourseInstallment> RetrieveCentreCourseInstallments(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreCourseInstallments(organisationId, centreId, orderBy, paging);
        }

        public IEnumerable<Scheme> RetrieveUnassignedCentreSchemes(int organisationId, int schemeId)
        {
            return
                _nidanDataService.RetrieveSchemes(organisationId, a => !a.CentreSchemes.Any(d => d.SchemeId == schemeId),
                    null, null).Items.ToList();
        }

        public PagedResult<CentreScheme> RetrieveCentreSchemes(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreSchemes(organisationId, centreId, orderBy, paging);
        }

        public IEnumerable<Sector> RetrieveUnassignedCentreSectors(int organisationId, int sectorId)
        {
            return
                _nidanDataService.RetrieveSectors(organisationId, a => !a.CentreSectors.Any(d => d.SectorId == sectorId),
                    null, null).Items.ToList();
        }

        public PagedResult<CentreSector> RetrieveCentreSectors(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreSectors(organisationId, centreId, orderBy, paging);
        }

        public PagedResult<Admission> RetrieveAdmissions(int organisationId, Expression<Func<Admission, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissions(organisationId, predicate, orderBy, paging);
        }

        public List<CourseInstallment> RetrieveCourseInstallments(int organisationId, int centreId)
        {
            return _nidanDataService.Retrieve<CourseInstallment>(organisationId, c => c.CentreId == centreId).ToList();
        }

        public List<Graph> RetrievePieGraphStatistics(int organisationId)
        {
            var centre = RetrieveCentres(organisationId, e => true).ToList();
            var graphData = new List<Graph>();
            foreach (var item in centre)
            {
                graphData.Add(new Graph
                {
                    CentreId = item.CentreId,
                    CentreName = item.Name,
                    MobilizationCount = item.Mobilizations.Count(e => e.Close == "No"),
                    AdmissionCount = item.Admissions.Count,
                    EnquiryCount = item.Enquiries.Count(e=>e.IsRegistrationDone==false),
                    RegistrationCount = item.Registrations.Count(e=>e.IsAdmissionDone==false),
                    CounsellingCount = item.Counsellings.Count(e => e.IsRegistrationDone == false)
                });
            }
            return graphData;
        }

        public List<Graph> RetrieveBarGraphStatistics(int organisationId)
        {
            var startOfWeekDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endOfWeekDate = startOfWeekDate.AddDays(6);
            var enquiries =
                RetrieveEnquiries(organisationId,
                    e => e.EnquiryDate >= startOfWeekDate && e.EnquiryDate <= endOfWeekDate).ToList();
            var mobilizations =
                RetrieveMobilizations(organisationId,
                    e => e.CreatedDate >= startOfWeekDate && e.CreatedDate <= endOfWeekDate).Items.ToList();
            var registrations =
                RetrieveRegistrations(organisationId,
                    e => e.RegistrationDate >= startOfWeekDate && e.RegistrationDate <= endOfWeekDate).Items.ToList();
            var admissions =
                RetrieveAdmissions(organisationId,
                    e => e.AdmissionDate >= startOfWeekDate && e.AdmissionDate <= endOfWeekDate).Items.ToList();
            var graphData = new List<Graph>();

            foreach (var date in endOfWeekDate.RangeFrom(startOfWeekDate))
            {
                graphData.Add(new Graph
                {
                    MobilizationCount = mobilizations.Count(e => e.CreatedDate.Date == date.Date && e.Close == "No"),
                    AdmissionCount = admissions.Count(e => e.AdmissionDate.Date == date.Date),
                    EnquiryCount = enquiries.Count(e => e.EnquiryDate.Date == date.Date && e.IsRegistrationDone == false),
                    RegistrationCount = registrations.Count(e => e.RegistrationDate.Date == date && e.IsAdmissionDone == false),
                    Date = date
                });
            }
            return graphData;
        }

        public Registration RetrieveRegistration(int organisationId, int centreId, int registraionId)
        {
            return _nidanDataService.RetrieveRegistration(organisationId, centreId, registraionId, p => true);
        }

        public PagedResult<FollowUpHistory> RetrieveFollowUpHistories(int organisationId, Expression<Func<FollowUpHistory, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpHistories(organisationId, predicate, orderBy, paging);
        }

        public FollowUpHistory RetrieveFollowUpHistory(int organisationId, int followUpHistoryId, Expression<Func<FollowUpHistory, bool>> predicate)
        {
            return _nidanDataService.RetrieveFollowUpHistory(organisationId, followUpHistoryId, predicate);
        }

        public PagedResult<FollowUp> RetrieveFollowUpBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<FollowUp, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
        }

        public PagedResult<Registration> RetrieveRegistrationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Registration, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveRegistrationBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
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

        public PagedResult<Counselling> RetrieveCounsellings(int organisationId,
            Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellings(organisationId, predicate, orderBy, paging);
        }

        public Batch RetrieveBatch(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBatch(organisationId, id, b => true);
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

        public Admission RetrieveAdmission(int organisationId, int admissionId,
            Expression<Func<Admission, bool>> predicate)
        {
            return _nidanDataService.RetrieveAdmission(organisationId, admissionId, p => true);
        }

        public Admission RetrieveAdmission(int organisationId, int id)
        {
            return _nidanDataService.RetrieveAdmission(organisationId, id, p => true);
        }

        public List<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Batch>(organisationId, predicate);
        }


        public PagedResult<CandidateFee> RetrieveCandidateFees(int organisationId,
            Expression<Func<CandidateFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateFees(organisationId, predicate, orderBy, paging);
        }

        public CandidateFee RetrieveCandidateFee(int organisationId, int candidateFeeId,
            Expression<Func<CandidateFee, bool>> predicate)
        {
            return _nidanDataService.RetrieveCandidateFee(organisationId, candidateFeeId, p => true);
        }

        public CandidateFee RetrieveCandidateFee(int organisationId, int id)
        {
            return _nidanDataService.RetrieveCandidateFee(organisationId, id, p => true);
        }

        public PagedResult<CandidateInstallment> RetrieveCandidateInstallments(int organisationId,
            Expression<Func<CandidateInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateInstallments(organisationId, predicate, orderBy, paging);
        }

        public CandidateInstallment RetrieveCandidateInstallment(int organisationId, int candidateInstallmentId,
            Expression<Func<CandidateInstallment, bool>> predicate)
        {
            return _nidanDataService.RetrieveCandidateInstallment(organisationId, candidateInstallmentId, p => true);
        }

        public PagedResult<CandidateFee> RetrieveCandidateFeeBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<CandidateFee, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateFeeBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public List<Course> RetrieveCentreCourses(int organisationId, int centreId,
            Expression<Func<CentreCourse, bool>> predicate)
        {
            // var t = _nidanDataService.RetrieveCentreCourses(organisationId, centreId);
            var courses =
                _nidanDataService.RetrieveCentreCourses(organisationId, centreId, predicate).Select(e => e.Course);
            return courses.ToList();
        }

        public List<Scheme> RetrieveCentreSchemes(int organisationId, int centreId,
            Expression<Func<CentreScheme, bool>> predicate)
        {
            var schemes =
                _nidanDataService.RetrieveCentreSchemes(organisationId, centreId, predicate).Select(e => e.Scheme);
            return schemes.ToList();
        }

        public List<Sector> RetrieveCentreSectors(int organisationId, int centreId,
            Expression<Func<CentreSector, bool>> predicate)
        {
            var sectors =
                _nidanDataService.RetrieveCentreSectors(organisationId, centreId, predicate).Select(e => e.Sector);
            return sectors.ToList();
        }

        public Registration RetrieveRegistration(int organisationId, int id)
        {
            return _nidanDataService.RetrieveRegistration(organisationId, id, r => true);
        }

        //Update


        public Personnel UpdatePersonnel(int organisationId, Personnel personnel)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, personnel);
        }

        public Enquiry UpdateEnquiry(int organisationId, Enquiry enquiry, List<int> courseIds)
        {
            //update follow Up
            var enquiryFollowUp =
                _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == enquiry.EnquiryId)
                    .Items.FirstOrDefault();
            var enquiryCounselling =
                _nidanDataService.RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId)
                    .Items.FirstOrDefault();
            var counsellingFromEnquiry = enquiry.Counsellings?.FirstOrDefault(e => e.EnquiryId == enquiry.EnquiryId);
            var counselling = _nidanDataService.RetrieveCounselling(organisationId,
                counsellingFromEnquiry?.CounsellingId ?? -1, e => true);
            if (enquiryFollowUp != null)
            {
                enquiryFollowUp.FollowUpDateTime = enquiry.FollowUpDate ?? enquiryFollowUp.FollowUpDateTime;
                enquiry.FollowUpDate = enquiryFollowUp.FollowUpDateTime;
                enquiryFollowUp.Close = enquiry.Close;
                enquiryFollowUp.Title = enquiry.Title;
                enquiryFollowUp.FirstName = enquiry.FirstName;
                enquiryFollowUp.MiddleName = enquiry.MiddleName;
                enquiryFollowUp.LastName = enquiry.LastName;
                enquiryFollowUp.Mobile = enquiry.Mobile;
                enquiryFollowUp.AlternateMobile = enquiry.AlternateMobile;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryFollowUp);
            }
            if (counselling != null)
            {
                counselling.FollowUpDate = enquiry.FollowUpDate ?? enquiryFollowUp.FollowUpDateTime.AddDays(2);
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }

            if (enquiryCounselling != null)
            {
                enquiryCounselling.Title = enquiry.Title;
                enquiryCounselling.FirstName = enquiry.FirstName;
                enquiryCounselling.MiddleName = enquiry.MiddleName;
                enquiryCounselling.LastName = enquiry.LastName;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryCounselling);
            }

            var enquiryCourses = RetrieveEnquiryCourses(organisationId, enquiry.CentreId, enquiry.EnquiryId);
            // Create EnquiryCourse If not added on create
            if (!enquiry.EnquiryCourses.Any() && courseIds.Any())
                CreateEnquiryCourse(organisationId, enquiry.CentreId, enquiry.EnquiryId, courseIds);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
        }



        public Mobilization UpdateMobilization(int organisationId, Mobilization mobilization)
        {
            //update follow Up
            var mobilizationFollowUp =
                _nidanDataService.RetrieveFollowUps(organisationId, e => e.MobilizationId == mobilization.MobilizationId)
                    .Items.FirstOrDefault();
            mobilizationFollowUp.FollowUpDateTime = mobilization.FollowUpDate ?? mobilizationFollowUp.FollowUpDateTime;
            //mobilizationFollowUp.Closed = mobilization.Close == "Yes";
            mobilizationFollowUp.Title = mobilization.Title ?? mobilizationFollowUp.Title;
            mobilizationFollowUp.FirstName = mobilization.FirstName ?? mobilizationFollowUp.FirstName;
            mobilizationFollowUp.MiddleName = mobilization.MiddleName ?? mobilizationFollowUp.MiddleName;
            mobilizationFollowUp.LastName = mobilization.LastName ?? mobilizationFollowUp.LastName;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilizationFollowUp);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilization);
        }

        public FollowUp UpdateFollowUp(int organisationId, FollowUp followUp)
        {
            //Update Mobilization Date
            if (followUp.MobilizationId.HasValue && followUp.MobilizationId.Value != 0)
            {
                var mobilization = _nidanDataService.RetrieveMobilization(organisationId, followUp.MobilizationId.Value,
                    e => true);
                mobilization.FollowUpDate = followUp.FollowUpDateTime;
                mobilization.Close = followUp.Close;
                mobilization.ClosingRemark = followUp.ClosingRemark;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, mobilization);

            }

            //Update Enquiry Date
            if (followUp.EnquiryId.HasValue && followUp.EnquiryId.Value != 0)
            {
                var enquiry = _nidanDataService.RetrieveEnquiry(organisationId, followUp.EnquiryId.Value, e => true);
                enquiry.FollowUpDate = followUp.FollowUpDateTime;
                enquiry.Close = followUp.Close;
                enquiry.ClosingRemark = followUp.ClosingRemark;

                var counsellingFromEnquiry = _nidanDataService.RetrieveEnquiry(organisationId, followUp.EnquiryId.Value,
                    e => true);
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
                    _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
                }
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            }

            //Update Counselling Date
            if (followUp.CounsellingId.HasValue && followUp.CounsellingId.Value != 0)
            {
                var counselling = _nidanDataService.RetrieveCounselling(organisationId, followUp.CounsellingId.Value,
                    e => true);
                counselling.FollowUpDate = followUp.FollowUpDateTime;
                counselling.Close = followUp.Close;
                counselling.ClosingRemark = followUp.ClosingRemark;

                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }


            var followUpHistory = new FollowUpHistory
            {
                FollowUpId = followUp.FollowUpId,
                FollowUpType = followUp.FollowUpType,
                Remarks = followUp.Remark,
                Close = followUp.Close,
                ClosingRemarks = followUp.ClosingRemark,
                FollowUpDate = followUp.FollowUpDateTime.Date,
                CreatedDate = DateTime.UtcNow.Date,
                CentreId = followUp.CentreId,
                OrganisationId = organisationId
            };
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
            _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            return data;
        }

        public Centre UpdateCentre(int organisationId, Centre centre)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centre);
        }

        public Counselling UpdateCounselling(int organisationId, Counselling counselling)
        {
            var enquiryFollowUp =
                _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == counselling.EnquiryId)
                    .Items.FirstOrDefault();
            var enquiry = _nidanDataService.RetrieveEnquiry(organisationId, counselling.EnquiryId, e => true);

            if (enquiryFollowUp != null)
            {
                enquiryFollowUp.FollowUpDateTime = counselling.FollowUpDate.Value;
                enquiryFollowUp.FollowUpType = "Counselling";
                enquiryFollowUp.CounsellingId = counselling.CounsellingId;
                enquiryFollowUp.IntrestedCourseId = counselling.CourseOfferedId;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiryFollowUp);
            }

            enquiry.FollowUpDate = counselling.FollowUpDate.Value;
            enquiry.EnquiryStatus = "Counselling";
            counselling.Close = enquiry.Close;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
        }

        //public RegistrationPaymentReceipt UpdateRegistrationPaymentReceipt(int organisationId,
        //    RegistrationPaymentReceipt registrationPaymentReceipt)
        //{
        //    var data = _nidanDataService.RetrieveRegistrationPaymentReceipt(organisationId, registrationPaymentReceipt.RegistrationPaymentReceiptId, e => true);
        //    var course = _nidanDataService.RetrieveCourse(organisationId, registrationPaymentReceipt.CourseId, e => true);
        //    registrationPaymentReceipt.Particulars = string.Format(registrationPaymentReceipt.Fees + " Rupees Paid Against " + course.Name);
        //    registrationPaymentReceipt.CounsellingId = data.CounsellingId;
        //    var registrationFollowUp = _nidanDataService.RetrieveFollowUps(organisationId, e => e.EnquiryId == registrationPaymentReceipt.EnquiryId).Items.FirstOrDefault();
        //    if (registrationFollowUp != null)
        //    {
        //        registrationFollowUp.Remark = data?.Remarks;
        //        registrationFollowUp.FollowUpDateTime = registrationPaymentReceipt.FollowUpDate ??
        //                                                DateTime.Now.AddDays(2);
        //        _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationFollowUp);
        //    }
        //    return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registrationPaymentReceipt);
        //}

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

        public Trainer UpdateTrainer(int organisationId, Trainer trainer)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, trainer);
        }

        public Subject UpdateSubject(int organisationId, Subject subject, List<int> courseIds, List<int> trainerIds)
        {
            var subjectCourseId = RetrieveSubjectCourses(organisationId, subject.SubjectId);
            if (subjectCourseId.Any() != courseIds.Any())
            {

            }

            // Create SubjectCourse If not added on create
            if (!subject.SubjectCourses.Any() && courseIds.Any())
                CreateSubjectCourse(organisationId, subject.SubjectId, courseIds);

            // Create SubjectTrainer If not added on create
            if (!subject.SubjectTrainers.Any() && trainerIds.Any())
                CreateSubjectTrainer(organisationId, subject.SubjectId, trainerIds);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, subject);
        }

        public Session UpdateSession(int organisationId, Session session)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, session);
        }

        public Room UpdateRoom(int organisationId, Room room)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, room);
        }

        public BatchDay UpdateBatchDay(int organisationId, BatchDay batchDay)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, batchDay);
        }

        public CandidateFee UpdateCandidateFee(int organisationId, CandidateFee candidateFee)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry<CandidateFee>(organisationId, candidateFee);
        }

        public Registration UpdateRegistartion(int organisationId, Registration registration)
        {
            // Update Paid Amount in CandidateFee
            var candidateFeeData = RetrieveCandidateFee(organisationId, registration.CandidateFeeId);
            candidateFeeData.PaidAmount = registration.CandidateFee.PaidAmount;
            candidateFeeData.PaymentModeId = registration.CandidateFee.PaymentModeId;
            candidateFeeData.BankName = registration.CandidateFee.BankName;
            candidateFeeData.ChequeNumber = registration.CandidateFee.ChequeNumber;
            candidateFeeData.ChequeDate = registration.CandidateFee.ChequeDate;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateFeeData);

            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registration);
        }


        public Admission UpdateAdmission(int organisationId, Admission admission)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, admission);
        }

        public Batch UpdateBatch(int organisationId, Batch batch)
        {
            return null;
        }

        public Batch UpdateBatch(int organisationId, Batch batch, BatchDay batchDays, List<int> trainerIds)
        {
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, batch);
            if (!batch.BatchTrainers.Any() && trainerIds.Any())
                CreateBatchTrainer(organisationId, batch.CentreId, batch.BatchId, trainerIds);
            UpdateBatchDay(organisationId, batchDays);
            return data;
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

        public void DeleteEnquiryCourse(int organisationId, int enquiryId, int courseId)
        {
            _nidanDataService.Delete<EnquiryCourse>(organisationId,
                p => p.EnquiryId == enquiryId && p.CourseId == courseId);
        }

        public void DeleteSubjectCourse(int organisationId, int subjectId, int courseId)
        {
            _nidanDataService.Delete<SubjectCourse>(organisationId,
                p => p.SubjectId == subjectId && p.CourseId == courseId);
        }

        public void DeleteSubjectTrainer(int organisationId, int subjectId, int trainerId)
        {
            _nidanDataService.Delete<SubjectTrainer>(organisationId,
                p => p.SubjectId == subjectId && p.TrainerId == trainerId);
        }

        public void DeleteBatchTrainer(int organisationId, int batchId, int trainerId)
        {
            _nidanDataService.Delete<BatchTrainer>(organisationId, p => p.BatchId == batchId && p.TrainerId == trainerId);
        }


        public void MarkAsReadFollowUp(int organisationId, int id)
        {
            var data = RetrieveFollowUp(organisationId, id);
            data.ReadDateTime = _today;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, data);
        }

        public void DeleteCentreCourse(int organisationId, int centreId, int courseId)
        {
            _nidanDataService.Delete<CentreCourse>(organisationId, p => p.CentreId == centreId && p.CourseId == courseId);
        }

        public void DeleteCentreScheme(int organisationId, int centreId, int schemeId)
        {
            _nidanDataService.Delete<CentreScheme>(organisationId, p => p.CentreId == centreId && p.SchemeId == schemeId);
        }

        public void DeleteCentreSector(int organisationId, int centreId, int sectorId)
        {
            _nidanDataService.Delete<CentreSector>(organisationId, p => p.CentreId == centreId && p.SectorId == sectorId);
        }

        public void DeleteCentreCourseInstallment(int organisationId, int centreId, int courseInstallmentId)
        {
            _nidanDataService.Delete<CentreCourseInstallment>(organisationId, p => p.CentreId == centreId && p.CourseInstallmentId == courseInstallmentId);
        }

        #endregion

        //Document

        #region Document

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

        public PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveDocuments(organisationId, predicate, orderBy, paging);
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            return _nidanDataService.RetrieveDocument(organisationId, documentGuid);
        }

        #endregion

        //Template
        public byte[] CreateRegistrationRecieptBytes(int organisationId, int centreId, int id)
        {
            var candidateFeeData = _nidanDataService.RetrieveCandidateFee(organisationId, id,
                r => r.CentreId == centreId);
            var enquiry = RetrieveEnquiries(organisationId, e => e.StudentCode == candidateFeeData.StudentCode).FirstOrDefault();
            int value = candidateFeeData.FeeTypeId;
            FeeType feeType = (FeeType) value;
            var candidateFeeReceipt = new CandidateFeeReceipt()
            {
                OrganisationName = candidateFeeData.Organisation.Name,
                EmailId = enquiry.EmailId,
                PaymentDate = candidateFeeData.PaymentDate.Value.ToShortDateString(),
                CandidateAddress =
                    string.Concat(enquiry.Address1, enquiry.Address2,enquiry.Address3, enquiry.Address4),
                CandidateName = enquiry.FirstName + " " + enquiry.LastName,
                CentreName = candidateFeeData.Centre.Name,
                CourseDuration = candidateFeeData.CandidateInstallment.CourseInstallment.Course.Duration.ToString(),
                CourseName = candidateFeeData.CandidateInstallment.CourseInstallment.Course.Name,
                FeeTypeName = feeType.ToString(),
                InvoiceNumber = candidateFeeData.CandidateFeeId.ToString(),
                RecievedAmount = candidateFeeData.PaidAmount.ToString(),
                MobileNumber = enquiry.Mobile.ToString(),
                TotalCourseFee = candidateFeeData.CandidateInstallment.CourseFee.ToString()

            };
            return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(candidateFeeReceipt), "Registration");
        }

    }
}

