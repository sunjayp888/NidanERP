using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using SharedTypes.DataContracts;
using AssignType = Nidan.Entity.AssignType;
using PaymentMode = Nidan.Entity.PaymentMode;


namespace Nidan.Business
{
    public partial class NidanBusinessService : INidanBusinessService, ITenantOrganisationService
    {
        private INidanDataService _nidanDataService;
        private ICacheProvider _cacheProvider;
        private ITemplateService _templateService;
        private IEmailService _emailService;
        private ISMSService _smsService;

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
            ITemplateService templateService, IEmailService emailService, ISMSService smsService)
        {
            _nidanDataService = nidanDataService;
            _cacheProvider = cacheProvider;
            _templateService = templateService;
            _emailService = emailService;
            _smsService = smsService;
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
            CreateRommAvailable(organisationId, data.CentreId, batch, batchDays);
            //CreateTrainerAvailable(organisationId, data.CentreId, batch, batchDays);

            return data;
        }

        private void CreateRommAvailable(int organisationId, int centreId, Batch batch, BatchDay batchDays)
        {
            var batchTrainerData = RetrieveBatchTrainers(organisationId, batch.BatchId).ToList();
            var roomAvailables = new List<RoomAvailable>();
            var trainerAvailables = new List<TrainerAvailable>();
            if (batchDays.IsMonday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Monday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Monday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }

            }
            if (batchDays.IsTuesday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Tuesday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Tuesday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            if (batchDays.IsWednesday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Wednesday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Wednesday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            if (batchDays.IsThursday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Thursday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Thursday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            if (batchDays.IsFriday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Friday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Friday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            if (batchDays.IsSaturday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Saturday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Saturday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            if (batchDays.IsSunday)
            {
                roomAvailables.Add(new RoomAvailable
                {
                    RoomId = batch.RoomId,
                    BatchId = batch.BatchId,
                    Day = "Sunday",
                    StartTimeHours = batch.BatchStartTimeHours,
                    StartTimeMinutes = batch.BatchStartTimeMinutes,
                    StartTimeSpan = batch.BatchStartTimeSpan,
                    EndTimeHours = batch.BatchEndTimeHours,
                    EndTimeMinutes = batch.BatchEndTimeMinutes,
                    EndTimeSpan = batch.BatchEndTimeSpan,
                    CentreId = centreId,
                    OrganisationId = organisationId
                });
                foreach (var batchTrainer in batchTrainerData)
                {
                    trainerAvailables.Add(new TrainerAvailable
                    {
                        TrainerId = batchTrainer.TrainerId,
                        BatchId = batch.BatchId,
                        Day = "Sunday",
                        StartTimeHours = batch.BatchStartTimeHours,
                        StartTimeMinutes = batch.BatchStartTimeMinutes,
                        StartTimeSpan = batch.BatchStartTimeSpan,
                        EndTimeHours = batch.BatchEndTimeHours,
                        EndTimeMinutes = batch.BatchEndTimeMinutes,
                        EndTimeSpan = batch.BatchEndTimeSpan,
                        CentreId = centreId,
                        OrganisationId = organisationId
                    });
                }
            }
            _nidanDataService.Create<RoomAvailable>(organisationId, roomAvailables);
            _nidanDataService.Create<TrainerAvailable>(organisationId, trainerAvailables);
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
                FollowUpType = "Mobilization",
                Close = "No",
                CreatedBy = mobilization.CreatedBy,
                FollowUpUrl = string.Format("/Mobilization/Edit/{0}", data.MobilizationId),
                ReadDateTime = _today.AddYears(-100),
            };
            var followUpData = _nidanDataService.Create<FollowUp>(organisationId, followUp);
            var followUpHistory = new FollowUpHistory
            {
                FollowUpId = followUpData.FollowUpId,
                FollowUpType = followUpData.FollowUpType,
                Remarks = followUpData.Remark,
                Close = followUpData.Close,
                ClosingRemarks = followUpData.ClosingRemark,
                CreatedDate = DateTime.UtcNow.Date,
                FollowUpDate = followUpData.FollowUpDateTime.Date,
                CentreId = followUpData.CentreId,
                OrganisationId = organisationId,
                FollowUpBy = followUpData.CreatedBy
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
            CreateFollowUp(organisationId, data, personnelId);
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

        private void CreateFollowUp(int organisationId, Enquiry enquiry, int personnelId)
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
                FollowUpType = "Enquiry",
                FollowUpUrl = string.Format("/Enquiry/Edit/{0}", enquiry.EnquiryId),
                AlternateMobile = enquiry.AlternateMobile,
                Close = "No",
                ReadDateTime = _today.AddYears(-100),
                CreatedBy = personnelId
            };
            var followUpData = _nidanDataService.Create<FollowUp>(organisationId, followUp);
            var followUpHistory = new FollowUpHistory
            {
                FollowUpId = followUpData.FollowUpId,
                FollowUpType = followUpData.FollowUpType,
                Remarks = followUpData.Remark,
                Close = followUpData.Close,
                ClosingRemarks = followUpData.ClosingRemark,
                CreatedDate = DateTime.UtcNow.Date,
                FollowUpDate = followUpData.FollowUpDateTime.Date,
                CentreId = followUpData.CentreId,
                OrganisationId = organisationId,
                FollowUpBy = followUpData.CreatedBy
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
                    CreatedBy = personnelId,
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
                    ReadDateTime = _today.AddYears(-100),
                    Close = "No",
                    FollowUpType = "Mobilization",
                    FollowUpUrl = string.Format("/Mobilization/Edit/{0}", data.MobilizationId),
                    CreatedBy = personnelId
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
                    CreatedDate = DateTime.UtcNow.Date,
                    FollowUpDate = item.FollowUpDateTime,
                    CentreId = centreId,
                    OrganisationId = organisationId,
                    FollowUpBy = personnelId
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
                followUp.CreatedBy = counselling.PersonnelId;
                var followUpData = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, followUp);
                var followUpHistory = new FollowUpHistory
                {
                    FollowUpId = followUpData.FollowUpId,
                    FollowUpType = followUpData.FollowUpType,
                    Remarks = followUpData.Remark,
                    Close = followUpData.Close,
                    ClosingRemarks = followUpData.ClosingRemark,
                    FollowUpDate = counselling.FollowUpDate ?? followUpData.FollowUpDateTime.Date,
                    CreatedDate = DateTime.UtcNow.Date,
                    CentreId = followUpData.CentreId,
                    OrganisationId = organisationId,
                    FollowUpBy = followUpData.CreatedBy
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
            eventplan.EventApproveStateId = (int) Enum.EventApproveState.Created;
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

        public PostEvent CreatePostEvent(int organisationId, PostEvent postEvent)
        {
            return _nidanDataService.CreatePostEvent(organisationId, postEvent);
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
            CreateSubjectCourse(organisationId, data.SubjectId, courseIds);
            CreateSubjectTrainer(organisationId, data.SubjectId, trainerIds);
            return data;
        }

        private void CreateSubjectCourse(int organisationId, int subjectId, List<int> couserIds)
        {
            var subjectCourses = RetrieveSubjectCourses(organisationId, e => e.SubjectId == subjectId).ToList();
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
            // Retrieve CentreRecieptsetting where centreId = 
            var centreEnrollmentRecieptsettingData = _nidanDataService.RetrieveCentreEnrollmentReceiptSetting(organisationId, e => e.CentreId == centreId);
            var receiptNumber = string.Format("{0}/{1}/{2}", centreEnrollmentRecieptsettingData.TaxYear, centreEnrollmentRecieptsettingData.Centre.CentreCode, centreEnrollmentRecieptsettingData.EnrollmentNumber);
            var registrationData = RetrieveRegistration(organisationId, admission.RegistrationId);
            var enquiryData = RetrieveEnquiry(organisationId, registrationData.EnquiryId);
            var candidateInstallment = RetrieveCandidateInstallment(organisationId,
                registrationData.CandidateInstallmentId, e => true);
            admission.Registration.StudentCode = registrationData.StudentCode;
            admission.CreatedBy = personnelId;
            var batchData = new Batch();
            if (admission.BatchId != null)
            {
                batchData = RetrieveBatch(organisationId, admission.BatchId.Value);
                //create fee detail
                if (admission.Registration.CandidateInstallment.PaymentMethod == "LumpsumAmount")
                {
                    CreateCandidateFeeLumpSum(organisationId, centreId, personnelId, candidateInstallment, admission,
                        registrationData, candidateFee);
                }
                else
                {
                    CreateCandidateFeeInstallment(organisationId, centreId, personnelId, candidateInstallment, admission,
                        registrationData, candidateFee);
                }
            }
            if (admission.BatchId == null)
            {
                CreateCandidateFeeLumpSum(organisationId, centreId, personnelId, candidateInstallment, admission, registrationData, candidateFee);
            }
            admission.OrganisationId = organisationId;
            admission.CentreId = centreId;
            admission.EnrollmentNumber = receiptNumber;
            admission.CreatedBy = personnelId;
            centreEnrollmentRecieptsettingData.EnrollmentNumber = centreEnrollmentRecieptsettingData.EnrollmentNumber + 1;
            centreEnrollmentRecieptsettingData.TaxYear = DateTime.UtcNow.FiscalYear();
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreEnrollmentRecieptsettingData);
            var admissionData = _nidanDataService.CreateAdmission(organisationId, admission);
            admissionData.Batch = admission.BatchId != null ? batchData : null;
            admissionData.Registration = registrationData;

            // Update Registration IsAdmissionDone
            var registration = RetrieveRegistration(organisationId, centreId, admission.RegistrationId);
            if (registration != null)
            {
                registration.IsAdmissionDone = true;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registration);
            }

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
                followup.AdmissionId = admissionData.AdmissionId;
                followup.Centre = null;
                followup.Course = null;
                followup.Enquiry = null;
                followup.Organisation = null;
                followup.Registration = null;
                followup.CreatedBy = personnelId;
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
                    OrganisationId = organisationId,
                    FollowUpBy = personnelId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }

            //Email
            //SendCandidateEnrollmentEmail(organisationId, centreId, admissionData);
            //send SMS
            //SendAdmissionSms(admissionData);
            return admissionData;
        }

        private void CreateCandidateFeeLumpSum(int organisationId, int centreId, int personnelId,
            CandidateInstallment candidateInstallment, Admission admission, Registration registration,
            CandidateFee candidateFee)
        {
            // Retrieve CentreRecieptsetting where centreId = 
            var centreRecieptsettingData = _nidanDataService.RetrieveCentreReceiptSetting(organisationId, e => e.CentreId == centreId);
            var receiptNumber = string.Format("{0}/{1}/{2}", centreRecieptsettingData.TaxYear, centreRecieptsettingData.Centre.CentreCode, centreRecieptsettingData.ReceiptNumber);
            var candidateFeeData = new CandidateFee
            {
                CandidateInstallmentId = candidateInstallment.CandidateInstallmentId,
                PaymentModeId = candidateFee.PaymentModeId,
                ChequeDate = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeDate,
                ChequeNumber = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeNumber,
                BankName = candidateFee.PaymentModeId == 1 ? null : candidateFee.BankName,
                FeeTypeId = (int)FeeType.Admission,
                FiscalYear = DateTime.UtcNow.FiscalYear(),
                CentreId = centreId,
                OrganisationId = organisationId,
                PersonnelId = personnelId,
                IsPaymentDone = true,
                StudentCode = admission.Registration.StudentCode,
                PaidAmount = candidateInstallment.LumpsumAmount - registration.CandidateFee.PaidAmount,
                PaymentDate = DateTime.UtcNow,
                ReceiptNumber = receiptNumber
            };
            // Update data in CandidateInstallment
            candidateInstallment.PaymentMethod = admission.Registration.CandidateInstallment.PaymentMethod;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateInstallment);
            // Increment RecieptNo by and Update.
            centreRecieptsettingData.ReceiptNumber = centreRecieptsettingData.ReceiptNumber + 1;
            centreRecieptsettingData.TaxYear = DateTime.UtcNow.FiscalYear();
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreRecieptsettingData);
            _nidanDataService.Create<CandidateFee>(organisationId, candidateFeeData);
        }

        private void CreateCandidateFeeInstallment(int organisationId, int centreId, int personnelId,
            CandidateInstallment candidateInstallment, Admission admission, Registration registration,
            CandidateFee candidateFee)
        {
            // Retrieve CentreRecieptsetting where centreId = 
            var centreRecieptsettingData = _nidanDataService.RetrieveCentreReceiptSetting(organisationId, e => e.CentreId == centreId);
            var receiptNumber = string.Format("{0}/{1}/{2}", centreRecieptsettingData.TaxYear, centreRecieptsettingData.Centre.CentreCode, centreRecieptsettingData.ReceiptNumber);
            var installmentDate = new DateTime(DateTime.UtcNow.Year, DateTime.Now.Month, 5, 0, 0, 0);
            var batch = RetrieveBatch(organisationId, admission.BatchId ?? 0);
            var candidateFees = new List<CandidateFee>();
            var candidateFeeData = new CandidateFee
            {
                CandidateInstallmentId = candidateInstallment.CandidateInstallmentId,
                PaymentModeId = candidateFee.PaymentModeId,
                ChequeDate = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeDate,
                ChequeNumber = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeNumber,
                BankName = candidateFee.PaymentModeId == 1 ? null : candidateFee.BankName,
                FeeTypeId = (int)FeeType.Admission,
                FiscalYear = DateTime.UtcNow.FiscalYear(),
                CentreId = centreId,
                OrganisationId = organisationId,
                PersonnelId = personnelId,
                IsPaymentDone = true,
                StudentCode = admission.Registration.StudentCode,
                PaidAmount = candidateFee.IsPaidAmountOverride ? candidateFee.PaidAmount : candidateInstallment.DownPayment <= registration.CandidateFee.PaidAmount
                    ? 0
                    : (candidateInstallment.DownPayment - registration.CandidateFee.PaidAmount),
                PaymentDate = DateTime.UtcNow,
                ReceiptNumber = receiptNumber
            };
            // Increment RecieptNo by and Update.
            centreRecieptsettingData.ReceiptNumber = centreRecieptsettingData.ReceiptNumber + 1;
            centreRecieptsettingData.TaxYear = DateTime.UtcNow.FiscalYear();
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreRecieptsettingData);
            candidateFees.Add(candidateFeeData);
            if (batch != null)
            {
                for (int i = 1; i <= batch?.NumberOfInstallment; i++)
                {
                    candidateFees.Add(new CandidateFee
                    {
                        CandidateInstallmentId = candidateInstallment.CandidateInstallmentId,
                        PaymentModeId = candidateFee.PaymentModeId,
                        ChequeDate = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeDate,
                        ChequeNumber = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeNumber,
                        BankName = candidateFee.PaymentModeId == 1 ? null : candidateFee.BankName,
                        FeeTypeId = (int)FeeType.Installment,
                        FollowUpDate = batch?.BatchStartDate.AddMonths(batch.NumberOfInstallment),
                        FiscalYear = DateTime.UtcNow.FiscalYear(),
                        InstallmentAmount = candidateFee.IsPaidAmountOverride ?
                            (candidateInstallment.CourseFee - (candidateFee.PaidAmount + registration.CandidateFee.PaidAmount)) / batch?.NumberOfInstallment :
                            (candidateInstallment.CourseFee - candidateInstallment.DownPayment) / batch?.NumberOfInstallment,
                        CentreId = centreId,
                        OrganisationId = organisationId,
                        PersonnelId = personnelId,
                        IsPaymentDone = false,
                        StudentCode = admission.Registration.StudentCode,
                        InstallmentNumber = i,
                        InstallmentDate = installmentDate.AddMonths(i)
                    });
                }
                candidateInstallment.NumberOfInstallment = batch?.NumberOfInstallment;
            }
            _nidanDataService.Create<CandidateFee>(organisationId, candidateFees);
            candidateInstallment.PaymentMethod = admission.Registration.CandidateInstallment.PaymentMethod;
            candidateInstallment.LumpsumAmount = null;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateInstallment);
        }

        public CandidateFee CreateCandidateFee(int organisationId, CandidateFee candidateFee)
        {
            return _nidanDataService.CreateCandidateFee(organisationId, candidateFee);
        }

        public FollowUpHistory CreateFollowUpHistory(int organisationId, FollowUpHistory followUpHistory)
        {
            return _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
        }

        public Module CreateModule(int organisationId, Module module)
        {
            return _nidanDataService.CreateModule(organisationId, module);
        }

        public RoomAvailable CreateRoomAvailable(int organisationId, RoomAvailable roomAvailable)
        {
            return _nidanDataService.Create<RoomAvailable>(organisationId, roomAvailable);
        }

        public TrainerAvailable CreateTrainerAvailable(int organisationId, TrainerAvailable trainerAvailable)
        {
            return _nidanDataService.Create<TrainerAvailable>(organisationId, trainerAvailable);
        }

        public ExpenseHeader CreateExpenseHeader(int organisationId, ExpenseHeader expenseHeader)
        {
            return _nidanDataService.Create<ExpenseHeader>(organisationId, expenseHeader);
        }

        public Registration CreateCandidateRegistration(int organisationId, int centreId, int personnelId,
            string studentCode, Registration registration)
        {
            registration.CourseInstallment.CourseInstallmentId = registration.CourseInstallmentId;
            var candidateInstallmentData = CandidateInstallment(organisationId, centreId, studentCode, registration?.CandidateInstallment, registration.CourseInstallment);
            registration.CandidateFee.CandidateInstallmentId = candidateInstallmentData.CandidateInstallmentId;
            registration.CandidateInstallmentId = candidateInstallmentData.CandidateInstallmentId;
            registration.CreatedBy = personnelId;

            var candidateFeeData = CreateCandidateFee(organisationId, centreId, personnelId, studentCode, candidateInstallmentData.CandidateInstallmentId, registration?.CandidateFee);

            //

            var data = CandidateRegistration(organisationId, centreId, studentCode, registration, candidateFeeData.CandidateFeeId, personnelId);

            var registrationData = RetrieveRegistration(organisationId, data.RegistrationId);
            //Send Email
            //SendCandidateRegistrationEmail(organisationId, centreId, registrationData);
            //Send SMS
            //SendRegistrationSms(registrationData);
            return data;
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
                    LumpsumAmount = courseInstallment.LumpsumAmount,
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
            Registration registration, int candidateFeeId, int personnelId)
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
            enquiry.Scheme = null;
            enquiry.Sector = null;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, enquiry);
            var counselling =
                RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            if (counselling != null)
            {
                counselling.Centre = null;
                counselling.Course = null;
                counselling.Enquiry = null;
                counselling.Organisation = null;
                counselling.IsRegistrationDone = true;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, counselling);
            }
            var followUp = RetrieveFollowUps(organisationId, e => e.EnquiryId == data.EnquiryId).Items.FirstOrDefault();
            if (followUp != null)
            {
                followUp.Centre = null;
                followUp.Course = null;
                followUp.Enquiry = null;
                followUp.Organisation = null;
                followUp.IntrestedCourseId = data.CourseId;
                followUp.RegistrationId = data.RegistrationId;
                followUp.FollowUpType = "Registration";
                followUp.FollowUpUrl = string.Format("/Registration/Edit/{0}", data?.RegistrationId);
                followUp.CreatedBy = personnelId;
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
                    OrganisationId = organisationId,
                    FollowUpBy = personnelId
                };
                _nidanDataService.Create<FollowUpHistory>(organisationId, followUpHistory);
            }
            return data;
        }

        private CandidateFee CreateCandidateFee(int organisationId, int centreId, int personnelId, string studentCode, int? candidateInstallmentId, CandidateFee candidateFee)
        {

            // Retrieve CentreRecieptsetting where centreId = 
            var centreRecieptsettingData = _nidanDataService.RetrieveCentreReceiptSetting(organisationId, e => e.CentreId == centreId);
            var receiptNumber = string.Format("{0}/{1}/{2}", centreRecieptsettingData.TaxYear, centreRecieptsettingData.Centre.CentreCode, centreRecieptsettingData.ReceiptNumber);

            var candidateFeeData = new CandidateFee()
            {
                CentreId = centreId,
                OrganisationId = organisationId,
                BalanceInstallmentAmount = candidateFee.BalanceInstallmentAmount,
                CandidateInstallmentId = candidateInstallmentId,
                ChequeDate = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeDate,
                ChequeNumber = candidateFee.PaymentModeId == 1 ? null : candidateFee.ChequeNumber,
                BankName = candidateFee.PaymentModeId == 1 ? null : candidateFee.BankName,
                FeeTypeId = (int)FeeType.Registration,
                PaidAmount = candidateFee.PaidAmount,
                IsPaymentDone = true,
                PaymentDate = DateTime.UtcNow,
                StudentCode = studentCode,
                PaymentModeId = candidateFee.PaymentModeId,
                FiscalYear = DateTime.UtcNow.FiscalYear(),
                PersonnelId = personnelId,
                IsPaidAmountOverride = false,
                ReceiptNumber = receiptNumber
            };

            // Increment RecieptNo by and Update.
            centreRecieptsettingData.ReceiptNumber = centreRecieptsettingData.ReceiptNumber + 1;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreRecieptsettingData);
            return _nidanDataService.Create<CandidateFee>(organisationId, candidateFeeData);
        }


        public OtherFee CreateOtherFee(int organisationId, int centreId, OtherFee otherFee)
        {
            var centre = RetrieveCentre(organisationId, centreId);
            var voucherData = new Voucher();
            var vouchers =
                RetrieveVouchers(organisationId, centreId, e => e.CashMemo == otherFee.CashMemo).Items.ToList();
            if (!vouchers.Any(e => e.CashMemo == otherFee.CashMemo))
            {
                voucherData.CashMemo = otherFee.CashMemo;
                voucherData.CentreId = centreId;
                voucherData.OrganisationId = organisationId;
                voucherData.CreatedDate = DateTime.UtcNow;
                voucherData = _nidanDataService.Create<Voucher>(organisationId, voucherData);
                voucherData.VoucherNumber = String.Format("{0}/{1}/{2}", centre.Name, DateTime.UtcNow.ToString("MMMM"),
                    voucherData.VoucherId);
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, voucherData);
            }
            otherFee.VoucherId = voucherData.VoucherId == 0
                ? vouchers.FirstOrDefault().VoucherId
                : voucherData.VoucherId;
            var data = _nidanDataService.Create<OtherFee>(organisationId, otherFee);
            return data;
        }

        public Expense CreateExpense(int organisationId, int centreId, Expense expense, List<int> projectIds)
        {
            var centre = RetrieveCentre(organisationId, centreId);
            var centreVoucherNumber = RetrieveCentreVoucherNumber(organisationId, centreId, e => true);
            expense.VoucherNumber = String.Format("{0}/{1}/{2}", centre.Name, DateTime.UtcNow.ToString("MMMM"),
                centreVoucherNumber.Number);
            var data = _nidanDataService.Create<Expense>(organisationId, expense);
            CreateExpenseProject(organisationId, expense.CentreId, data.ExpenseId, projectIds);
            centreVoucherNumber.Number = centreVoucherNumber.Number + 1;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreVoucherNumber);
            return data;
        }

        public CentrePettyCash CreateCentrePettyCash(int organisationId, int centreId, int personnelId,
            CentrePettyCash centrePettyCash)
        {
            centrePettyCash.OrganisationId = organisationId;
            centrePettyCash.CentreId = centreId;
            centrePettyCash.CreatedBy = personnelId;
            return _nidanDataService.Create<CentrePettyCash>(organisationId, centrePettyCash);
        }

        public Voucher CreateVoucher(int organisationId, int centreId, int personnelId, Voucher voucher)
        {
            return _nidanDataService.Create<Voucher>(organisationId, voucher);
        }

        public ExpenseProject CreateExpenseProject(int organisationId, ExpenseProject expenseProject)
        {
            var data = _nidanDataService.Create<ExpenseProject>(organisationId, expenseProject);
            return data;
        }

        private void CreateExpenseProject(int organisationId, int centreId, int expenseId, List<int> projectIds)
        {
            var expenseProjects = RetrieveExpenseProjects(organisationId, centreId, expenseId).ToList();
            var expenseProjectList = new List<ExpenseProject>();
            foreach (var item in projectIds)
            {
                if (!expenseProjects.Any(e => e.ProjectId == item && e.ExpenseId == expenseId))
                {
                    expenseProjectList.Add(new ExpenseProject()
                    {
                        OrganisationId = organisationId,
                        CentreId = centreId,
                        ExpenseId = expenseId,
                        ProjectId = item
                    });
                }
            }
            if (expenseProjectList.Any())
                _nidanDataService.Create<ExpenseProject>(organisationId, expenseProjectList);
        }

        public Attendance CreateAttendance(int organisationId, int centreId, int personnelId, Attendance attendance)
        {
            return _nidanDataService.Create<Attendance>(organisationId, attendance);
        }

        public BatchAttendance CreateBatchAttendance(int organisationId, int centreId, int personnelId,
            BatchAttendance batchAttendance)
        {
            return _nidanDataService.Create<BatchAttendance>(organisationId, batchAttendance);
        }

        public bool CreateEventBrainstorming(int organisationId, int centreId, int eventId, List<EventBrainstorming> eventBrainstorming)
        {
            try
            {
                //Get EventBrainStroming
                var eventBrainStorming = _nidanDataService.Retrieve<EventBrainstorming>(organisationId, e => e.CentreId == centreId && e.EventId == eventId);
                if (eventBrainStorming.Any())
                {
                    //Delete EventBrainStroming    
                    _nidanDataService.DeleteList<EventBrainstorming>(organisationId, e => e.EventId == eventId);
                }
                //Create EventBrainStroming
                return _nidanDataService.CreateEventBrainstorming(organisationId, eventBrainstorming);

            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        //public CandidateInstallment CreateCandidateInstallment(int organisationId, CandidateInstallment candidateInstallment)
        //{
        //    return _nidanDataService.Create<CandidateInstallment>(organisationId, candidateInstallment);
        //}

        public bool MarkAttendance(int organisationId, int centreId, int personnelId, List<StudentAttendance> attendances, int batchId, int subjectId, int sessionId)
        {
            try
            {
                var attendanceData = RetrieveAttendances(organisationId, e => true);
                var batchAttendanceData = RetrieveBatchAttendances(organisationId, e => true);
                foreach (var attendance in attendances)
                {
                    var attendanceRecord = attendanceData.Items.Where(e => e.AttendanceDate == attendance.AttendanceDate && e.StudentCode == attendance.StudentCode);
                    if (attendanceRecord.Any())
                    {
                        var result = attendanceData.Items.FirstOrDefault(e => e.AttendanceDate == attendance.AttendanceDate && e.StudentCode == attendance.StudentCode);
                        if (result != null && attendance.IsPresent)
                        {
                            var batchResult = batchAttendanceData.Items.FirstOrDefault(e => e.CreatedDate == attendance.AttendanceDate && e.StudentCode == attendance.StudentCode && e.AttendanceId == result.AttendanceId);
                            result.InHour = attendance.InHour;
                            result.InMinute = attendance.InMinute;
                            result.InTimeSpan = attendance.InTimeSpan;
                            result.OutHour = attendance.OutHour;
                            result.OutMinute = attendance.OutMinute;
                            result.OutTimeSpan = attendance.OutTimeSpan;
                            result.IsPresent = true;
                            UpdateAttendance(organisationId, result);
                            if (batchResult != null)
                            {
                                batchResult.SubjectId = subjectId;
                                batchResult.SessionId = sessionId;
                                batchResult.Topic = attendance.Topic;
                                UpdateBatchAttendance(organisationId, batchResult);
                            }
                        }
                    }
                    else
                    {
                        var attendanceCreate = new Attendance()
                        {
                            PersonnelId = personnelId,
                            StudentCode = attendance.StudentCode,
                            InHour = attendance.InHour,
                            InMinute = attendance.InMinute,
                            InTimeSpan = attendance.InTimeSpan,
                            OutHour = attendance.OutHour,
                            OutMinute = attendance.OutMinute,
                            OutTimeSpan = attendance.OutTimeSpan,
                            IsPresent = attendance.IsPresent,
                            AttendanceDate = attendance.AttendanceDate,
                            CentreId = centreId,
                            OrganisationId = organisationId,
                            BioMetricLogTime = attendance.BiometricLogTime,
                            Direction = attendance.Direction
                        };
                        var data = CreateAttendance(organisationId, centreId, personnelId, attendanceCreate);
                        var batchAttendanceCreate = new BatchAttendance()
                        {
                            BatchId = batchId,
                            AttendanceId = data.AttendanceId,
                            SubjectId = subjectId,
                            SessionId = sessionId,
                            Topic = attendance.Topic,
                            //BatchTrainerId =  ,
                            StudentCode = attendance.StudentCode,
                            CentreId = centreId,
                            OrganisationId = organisationId,
                            CreatedBy = personnelId,
                            CreatedDate = attendance.AttendanceDate,
                        };
                        CreateBatchAttendance(organisationId, centreId, personnelId, batchAttendanceCreate);
                    }
                    //1. for each, Retrieve attendance from attendanceTable where date = attendance.AttendanceDate and studentcode = studentcode
                    //if record.Any() then update IsPresent true
                    //else create Attendance Records
                    //now create BatchAttendance record. 
                    //And please do not make any new function you have CreateBatchAttendance and CreateAttendance
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public EventBudget CreateEventBudget(int organisationId, EventBudget eventBudget)
        {
            return _nidanDataService.CreateEventBudget(organisationId, eventBudget);
        }

        public EventPlanning CreateEventPlanning(int organisationId, EventPlanning eventPlanning)
        {
            return _nidanDataService.CreateEventPlanning(organisationId, eventPlanning);
        }

        public CentreReceiptSetting CreateCentreReceiptSetting(int organisationId, CentreReceiptSetting centreReceiptSetting)
        {
            var data = _nidanDataService.Create<CentreReceiptSetting>(organisationId, centreReceiptSetting);
            return data;
        }

        public StockPurchase CreateStockPurchase(int organisationId, StockPurchase stockPurchase)
        {
            var data = _nidanDataService.Create<StockPurchase>(organisationId, stockPurchase);
            return data;
        }

        public StockIssue CreateStockIssue(int organisationId, StockIssue stockIssue)
        {
            var data = _nidanDataService.Create<StockIssue>(organisationId, stockIssue);
            return data;
        }

        public BatchPlanner CreateBatchPlanner(int organisationId, BatchPlanner batchPlanner, BatchPlannerDay batchPlannerDay)
        {
            var data = _nidanDataService.CreateBatchPlanner(organisationId, batchPlanner);
            CreateBatchPlannerDay(organisationId, batchPlanner.BatchPlannerId, batchPlanner.CentreId, batchPlannerDay);
            return data;
        }

        private BatchPlannerDay CreateBatchPlannerDay(int organisationId, int batchPlannerId, int centreId, BatchPlannerDay batchPlannerDay)
        {
            var batchPlannerDayData = new BatchPlannerDay()
            {
                BatchPlannerId = batchPlannerId,
                IsMonday = batchPlannerDay.IsMonday,
                IsTuesday = batchPlannerDay.IsTuesday,
                IsWednesday = batchPlannerDay.IsWednesday,
                IsThursday = batchPlannerDay.IsThursday,
                IsFriday = batchPlannerDay.IsFriday,
                IsSaturday = batchPlannerDay.IsSaturday,
                IsSunday = batchPlannerDay.IsSunday,
                OrganisationId = organisationId,
                CentreId = centreId
            };
            _nidanDataService.Create(organisationId, batchPlannerDayData);
            return batchPlannerDayData;
        }

        public BatchPlannerDay CreateBatchPlannerDay(int organisationId, BatchPlannerDay batchPlannerDay)
        {
            var data = _nidanDataService.Create<BatchPlannerDay>(organisationId, batchPlannerDay);
            return data;
        }

        public FixAsset CreateFixAsset(int organisationId, FixAsset fixAsset)
        {
            var data = _nidanDataService.CreateFixAsset(organisationId, fixAsset);
            var centre = RetrieveCentre(organisationId, fixAsset.CentreId);
            var itemName = RetrieveItems(organisationId, e => e.ItemId == fixAsset.ItemId).FirstOrDefault();
            for (int i = 1; i <= data.Quantity; i++)
            {
                var centreItemSetting = _nidanDataService.RetrieveCentreItemSetting(organisationId, fixAsset.CentreId, fixAsset.ItemId);
                var fixAssetMapping = new FixAssetMapping()
                {
                    FixAssetId = data.FixAssetId,
                    CostPerAsset = data.Cost / data.Quantity,
                    AssetCode = String.Format("Nest/{0}/{1}/{2}", centre.Name, itemName.Name, centreItemSetting.ItemNumber),
                    AssetOutOwner = centre.Name,
                    AssetOutStatusId = 1,
                    StatusDate = data.DateofPurchase,
                    CentreId = data.CentreId,
                    CreatedBy = data.CreatedBy,
                    ItemSettingId = centreItemSetting.ItemNumber,
                    OrganisationId = data.OrganisationId
                };
                centreItemSetting.ItemNumber = centreItemSetting.ItemNumber + 1;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreItemSetting);
                _nidanDataService.Create<FixAssetMapping>(organisationId, fixAssetMapping);
            }
            return data;
        }

        public FixAssetMapping CreateFixAssetMapping(int organisationId, FixAssetMapping fixAssetMapping)
        {
            var data = _nidanDataService.Create<FixAssetMapping>(organisationId, fixAssetMapping);
            return data;
        }

        public BankDeposite CreateBankDeposite(int organisationId, BankDeposite bankDeposite)
        {
            var data = _nidanDataService.Create<BankDeposite>(organisationId, bankDeposite);
            return data;
        }

        public ActivityAssigneeGroup CreateActivityAssigneeGroup(int organisationId, ActivityAssigneeGroup activityAssigneeGroup)
        {
            var data = _nidanDataService.CreateActivityAssigneeGroup(organisationId, activityAssigneeGroup);
            return data;
        }

        public Activity CreateActivity(int organisationId, int personnelId, int centreId, Activity activity)
        {
            activity.CreatedBy = personnelId;
            activity.CentreId = centreId;
            activity.TaskStateId = (int)Enum.TaskState.Created;
            var data = _nidanDataService.CreateActivity(organisationId, activity);
            return data;
        }

        public ActivityTask CreateActivityTask(int organisationId, int personnelId, int centreId, ActivityTask activityTask)
        {
            activityTask.CreatedBy = personnelId;
            //activityTask.CentreId = centreId;
            activityTask.Activity = null;
            var data = _nidanDataService.CreateActivityTask(organisationId, activityTask);
            return data;
        }

        public ActivityTaskState CreateActivityTaskState(int organisationId, ActivityTaskState activityTaskState)
        {
            var data = _nidanDataService.CreateActivityTaskState(organisationId, activityTaskState);
            if (data.TaskStateId==(int)Enum.TaskState.InProgress)
            {
                var activityTask = RetrieveActivityTask(organisationId, data.ActivityTaskId, e => true);
                var activityData = activityTask.Activity;
                activityData.TaskStateId = (int) Enum.TaskState.InProgress;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, activityData);
            }
            return data;
        }

        public ActivityAssignPersonnel CreateActivityAssignPersonnel(int organisationId, int centreId, int activityAssigneeGroupId,
            int personnelId)
        {
            var activityAssignPersonnel = new ActivityAssignPersonnel()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                ActivityAssigneeGroupId = activityAssigneeGroupId,
                PersonnelId = personnelId
            };
            return _nidanDataService.Create<ActivityAssignPersonnel>(organisationId, activityAssignPersonnel);
        }

        public ModuleExamSet CreateModuleExamSet(int organisationId, ModuleExamSet moduleExamSet)
        {
            return _nidanDataService.Create<ModuleExamSet>(organisationId, moduleExamSet);
        }

        public ModuleExamQuestionSet CreateModuleExamQuestionSet(int organisationId, ModuleExamQuestionSet moduleExamQuestionSet)
        {
            return _nidanDataService.Create<ModuleExamQuestionSet>(organisationId, moduleExamQuestionSet);
        }

        public Assessment CreateAssessment(int organisationId, Assessment assessment)
        {
            return _nidanDataService.Create<Assessment>(organisationId, assessment);
        }

        public CandidateAssessment CreateCandidateAssessment(int organisationId, CandidateAssessment candidateAssessment)
        {
            return _nidanDataService.Create<CandidateAssessment>(organisationId, candidateAssessment);
        }

        public CandidateAssessmentQuestionAnswer CreateCandidateAssessmentQuestionAnswer(int organisationId, CandidateAssessmentQuestionAnswer candidateAssessmentQuestionAnswer)
        {
            return _nidanDataService.Create<CandidateAssessmentQuestionAnswer>(organisationId, candidateAssessmentQuestionAnswer);
        }

        public Partner CreatePartner(int organisationId, Partner partner)
        {
            return _nidanDataService.Create<Partner>(organisationId, partner);
        }

        public CentreItemSetting RetrieveCentreItemSetting(int organisationId, int centreId, int itemId)
        {
            var data = _nidanDataService.RetrieveCentreItemSetting(organisationId, centreId, itemId);
            return data;
        }

        public PagedResult<FixAssetMapping> RetrieveFixAssetMappings(int organisationId, Expression<Func<FixAssetMapping, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            var data = _nidanDataService.RetrieveFixAssetMappings(organisationId, predicate);
            return data;
        }

        public FixAssetMapping RetrieveFixAssetMapping(int organisationId, int fixAssetMappingId)
        {
            var data = _nidanDataService.RetrieveFixAssetMapping(organisationId, fixAssetMappingId, e => true);
            return data;
        }

        public PagedResult<FixAssetMappingCountByCentre> RetrieveFixAssetMappingCountByCentre(int organisationId, Expression<Func<FixAssetMappingCountByCentre, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            var data = _nidanDataService.RetrieveFixAssetMappingCountByCentre(organisationId, predicate);
            return data;
        }

        public PagedResult<FixAssetDetailGrid> RetrieveFixAssetDetailGrid(int organisationId, Expression<Func<FixAssetDetailGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            var data = _nidanDataService.RetrieveFixAssetDetailGrid(organisationId, predicate);
            return data;
        }

        public IEnumerable<BankDepositeSummaryReport> RetriveBankDepositeCountReportByMonthWise(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var _today = DateTime.UtcNow;
            var date = _today.Date;
            var bankDepositeSummaryReports = new List<BankDepositeSummaryReport>();
            var data = _nidanDataService.RetriveBankDepositeCentreReportMonthWise(organisationId, e => e.Month == _today.Month && e.Year == _today.Year).Items.ToList();
            var centres = _nidanDataService.RetrieveCentres(organisationId, e => true).Items.ToList();
            foreach (var centre in centres)
            {
                var result = data.FirstOrDefault(e => e.CentreId == centre.CentreId);
                bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
                {
                    CentreId = result?.CentreId ?? centre.CentreId,
                    TotalBankDepositeAmount = result?.TotalBankDepositeAmount ?? 0,
                    CentreName = result?.CentreName ?? centre.Name,
                    Year = _today.Year
                });
            }
            bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
            {
                MonthName = "Total",
                TotalBankDepositeAmount = bankDepositeSummaryReports.Sum(e => e.TotalBankDepositeAmount),
            });
            return bankDepositeSummaryReports;
        }

        public IEnumerable<AvailablePettyCashReport> RetriveAvailablePettyCashReport(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var centres = RetrieveCentres(organisationId, e => true);
            var data = _nidanDataService.RetrieveAvailablePettyCashGrid(organisationId, e => true, orderBy, paging).Items.ToList();
            var availablePettyCashReport = new List<AvailablePettyCashReport>();
            foreach (var centre in centres)
            {
                var result = data.FirstOrDefault(e => e.CentreId == centre.CentreId);
                availablePettyCashReport.Add(new AvailablePettyCashReport()
                {
                    CentreId = result?.CentreId ?? centre.CentreId,
                    AvailablePettyCash = result?.AvailablePettyCash ?? 0,
                    CentreName = centre.Name
                });
            }
            return availablePettyCashReport;
        }

        public PagedResult<AvailablePettyCashGrid> RetrieveAvailablePettyCashGrid(int organisationId, Expression<Func<AvailablePettyCashGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveAvailablePettyCashGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<BankDepositeSearchField> RetriveCentreBankDepositeByDate(int organisationId, int centreId, DateTime date)
        {
            var bankDepositeData = _nidanDataService.RetrieveBankDeposites(organisationId, e => e.CentreId == centreId && e.DepositedDate.Day == date.Day && e.DepositedDate.Month == date.Month && e.DepositedDate.Year == date.Year);
            return bankDepositeData;
        }

        public IEnumerable<BankDepositeSummaryReport> RetriveBankDepositeReportByMonthAndYear(int organisationId, int centreId, int year, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            var startFiscalDate = new DateTime(year, 04, 01);
            var endFiscalDate = new DateTime(startFiscalDate.AddYears(1).Year, 03, 31);
            var bankDepositeSummaryReports = new List<BankDepositeSummaryReport>();
            var data = _nidanDataService.RetriveBankDepositeReportByMonthAndYear(organisationId, centreId, e => e.CentreId == centreId).Items.ToList();
            var months = DateTimeExtensions.EachMonth(startFiscalDate, endFiscalDate);
            foreach (var item in months)
            {
                var result = data.FirstOrDefault(e => e.Month == item.Month && e.Year == item.Year);
                bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
                {
                    CentreId = result?.CentreId ?? 0,
                    TotalBankDepositeAmount = result?.TotalBankDepositeAmount ?? 0,
                    CentreName = result?.CentreName ?? String.Empty,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month),
                    Year = item.Year,
                    Month = item.Month
                });
            }
            bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
            {
                MonthName = "Total",
                TotalBankDepositeAmount = bankDepositeSummaryReports.Sum(e => e.TotalBankDepositeAmount),
            });
            return bankDepositeSummaryReports;
        }

        public IEnumerable<BankDepositeSummaryReport> RetriveBankDepositeReportByDate(int organisationId, int centreId, int year, int month, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var bankDepositeSummaryReports = new List<BankDepositeSummaryReport>();
            var days = DateTimeExtensions.EachDay(firstDayOfMonth, lastDayOfMonth);
            var data = _nidanDataService.RetriveBankDepositeReportByDate(organisationId, centreId, e => e.DepositedDate >= firstDayOfMonth && e.DepositedDate <= lastDayOfMonth && e.CentreId == centreId, orderBy, paging).Items.ToList();
            foreach (var item in days)
            {
                var result = data.FirstOrDefault(e => e.DepositedDate.Month == item.Month && e.DepositedDate.Day == item.Day && e.DepositedDate.Year == item.Year);
                bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
                {
                    CentreId = result?.CentreId ?? 0,
                    TotalBankDepositeAmount = result?.TotalBankDeposite ?? 0,
                    CentreName = result?.CentreName ?? String.Empty,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month),
                    Year = item.Year,
                    Month = item.Month,
                    Date = item.ToShortDateString()
                });
            }
            bankDepositeSummaryReports.Add(new BankDepositeSummaryReport()
            {
                Date = "Total",
                TotalBankDepositeAmount = bankDepositeSummaryReports.Sum(e => e.TotalBankDepositeAmount),
            });
            return bankDepositeSummaryReports;
        }

        public PagedResult<BankDepositeSearchField> RetrieveBankDeposites(int organisationId, Expression<Func<BankDepositeSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBankDeposites(organisationId, predicate, orderBy, paging);
        }

        public BankDeposite RetrieveBankDeposite(int organisationId, int bankDepositeId, Expression<Func<BankDeposite, bool>> predicate)
        {
            return _nidanDataService.RetrieveBankDeposite(organisationId, bankDepositeId, predicate);
        }

        public PagedResult<ExpenseHeadLimit> RetrieveExpenseHeadLimits(int organisationId, int centreId, Expression<Func<ExpenseHeadLimit, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveExpenseHeadLimits(organisationId, centreId, predicate, orderBy, paging);
        }

        public PagedResult<ActivityAssigneeGroup> RetrieveActivityAssigneeGroups(int organisationId, Expression<Func<ActivityAssigneeGroup, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityAssigneeGroups(organisationId, predicate, orderBy, paging);
        }

        public ActivityAssigneeGroup RetrieveActivityAssigneeGroup(int organisationId, int activityAssigneeGroupId,
            Expression<Func<ActivityAssigneeGroup, bool>> predicate)
        {
            return _nidanDataService.RetrieveActivityAssigneeGroup(organisationId, activityAssigneeGroupId, predicate);
        }

        public PagedResult<Activity> RetrieveActivities(int organisationId, Expression<Func<Activity, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivities(organisationId, predicate, orderBy, paging);
        }

        public Activity RetrieveActivity(int organisationId, int activityId, Expression<Func<Activity, bool>> predicate)
        {
            return _nidanDataService.RetrieveActivity(organisationId, activityId, predicate);
        }

        public PagedResult<ActivityTask> RetrieveActivityTasks(int organisationId, Expression<Func<ActivityTask, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityTasks(organisationId, predicate, orderBy, paging);
        }

        public ActivityTask RetrieveActivityTask(int organisationId, int activityTaskId, Expression<Func<ActivityTask, bool>> predicate)
        {
            return _nidanDataService.RetrieveActivityTask(organisationId, activityTaskId, predicate);
        }

        public PagedResult<ActivityTaskState> RetrieveActivityTaskStates(int organisationId, Expression<Func<ActivityTaskState, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityTaskStates(organisationId, predicate, orderBy, paging);
        }

        public ActivityTaskState RetrieveActivityTaskState(int organisationId, int activityTaskStateId, Expression<Func<ActivityTaskState, bool>> predicate)
        {
            return _nidanDataService.RetrieveActivityTaskState(organisationId, activityTaskStateId, predicate);
        }

        public PagedResult<ActivityType> RetrieveActivityTypes(int organisationId, Expression<Func<ActivityType, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityTypes(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<Entity.TaskState> RetrieveTaskStates(int organisationId, Expression<Func<Entity.TaskState, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveTaskStates(organisationId, predicate, orderBy, paging);
        }

        public IEnumerable<Personnel> RetrieveUnassignedPersonnels(int organisationId, int centreId, int activityAssigneeGroupId)
        {
            var data = _nidanDataService.RetrievePersonnels(organisationId, a => !a.ActivityAssignPersonnels.Any(d => d.CentreId == centreId && d.ActivityAssignPersonnelId == activityAssigneeGroupId) && a.CentreId == centreId, null, null).Items.ToList();
            return data;
        }

        public PagedResult<ActivityAssignPersonnel> RetrieveActivityAssignPersonnels(int organisationId, int centreId, int activityAssigneeGroupId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityAssignPersonnels(organisationId, centreId, activityAssigneeGroupId, orderBy, paging);
        }

        public PagedResult<ActivityDataGrid> RetrieveActivityBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<ActivityDataGrid, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        public PagedResult<ActivityDataGrid> RetrieveActivityDataGrids(int organisationId, Expression<Func<ActivityDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityDataGrids(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<BankDepositeSearchField> RetrieveBankDepositeBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<BankDepositeSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBankDepositeBySearchKeyword(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public PagedResult<ModuleExamSet> RetrieveModuleExamSets(int organisationId, Expression<Func<ModuleExamSet, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveModuleExamSets(organisationId, predicate, orderBy, paging);
        }

        public ModuleExamSet RetrieveModuleExamSet(int organisationId, int moduleExamSetId, Expression<Func<ModuleExamSet, bool>> predicate)
        {
            return _nidanDataService.RetrieveModuleExamSet(organisationId, moduleExamSetId, predicate);
        }

        public PagedResult<ModuleExamQuestionAnswerGrid> RetrieveModuleExamQuestionSets(int organisationId, Expression<Func<ModuleExamQuestionAnswerGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveModuleExamQuestionSets(organisationId, predicate, orderBy, paging);
        }

        public ModuleExamQuestionSet RetrieveModuleExamQuestionSet(int organisationId, int moduleExamQuestionSetId,
            Expression<Func<ModuleExamQuestionSet, bool>> predicate)
        {
            return _nidanDataService.RetrieveModuleExamQuestionSet(organisationId, moduleExamQuestionSetId, predicate);
        }

        public PagedResult<FollowUpSearchField> RetrieveFollowUpsData(int organisationId, Expression<Func<FollowUpSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpsData(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<CounsellingDataGrid> RetrieveCounsellingDataGrid(int organisationId, Expression<Func<CounsellingDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellingDataGrid(organisationId, predicate, orderBy, paging);
        }

        #endregion

        #region // Retrieve

        public Personnel RetrievePersonnel(int organisationId, int personnelId)
        {
            var personnel = _nidanDataService.RetrievePersonnel(organisationId, personnelId, e => true);
            return personnel;
        }

        #endregion

        #region // Retrieve
        public PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy,
            Paging paging)
        {
            return _nidanDataService.RetrievePersonnels(organisationId, p => p.CentreId == centreId, orderBy, paging);
        }


        public PagedResult<ActivityTaskDataGrid> RetrieveActivityTaskBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<ActivityTaskDataGrid, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityTaskBySearchKeyword(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        public PagedResult<ActivityTaskDataGrid> RetrieveActivityTaskDataGrids(int organisationId, Expression<Func<ActivityTaskDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveActivityTaskDataGrids(organisationId, predicate, orderBy, paging);
        }

        public Assessment RetrieveAssessment(int organisationId, int assessmentId, Expression<Func<Assessment, bool>> predicate)
        {
            return _nidanDataService.RetrieveAssessment(organisationId, assessmentId, predicate);
        }

        public PagedResult<AssessmentGrid> RetrieveAssessmentGrid(int organisationId, Expression<Func<AssessmentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAssessmentGrid(organisationId, predicate, orderBy, paging);
        }

        public List<AssessmentType> RetrieveAssessmentTypes(int organisationId, Expression<Func<AssessmentType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AssessmentType>(organisationId, predicate);
        }

        public PagedResult<CandidateAssessmentGrid> RetrieveCandidateAssessmentGrid(int organisationId, Expression<Func<CandidateAssessmentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateAssessmentGrid(organisationId, predicate);
        }

        public List<ModuleExamSet> RetrieveModuleExamSets(int organisationId, Expression<Func<ModuleExamSet, bool>> predicate)
        {
            return _nidanDataService.Retrieve<ModuleExamSet>(organisationId, predicate);
        }

        public CandidateAssessment RetrieveCandidateAssessment(int organisationId, int candidateAssessmentId, Expression<Func<CandidateAssessment, bool>> predicate)
        {
            return _nidanDataService.RetrieveCandidateAssessment(organisationId, candidateAssessmentId, predicate);
        }

        public Partner RetrievePartner(int organisationId, int partnerId, Expression<Func<Partner, bool>> predicate)
        {
            return _nidanDataService.RetrievePartner(organisationId, partnerId, predicate);
        }

        public PagedResult<Partner> RetrievePartners(int organisationId, Expression<Func<Partner, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrievePartners(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<BatchTrainer> RetrieveBatchTrainers(int organisationId, Expression<Func<BatchTrainer, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchTrainers(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<EventManagementGrid> RetrieveEventManagementGrid(int organisationId, Expression<Func<EventManagementGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveEventManagementGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<CandidateAttemptedQuestionAnswerGrid> RetrieveCandidateAttemptedQuestionAnswerGrid(int organisationId, Expression<Func<CandidateAttemptedQuestionAnswerGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateAttemptedQuestionAnswerGrid(organisationId, predicate, orderBy, paging);
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

        public PagedResult<EnquiryDataGrid> RetrieveEnquiries(int organisationId, Expression<Func<EnquiryDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
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

        //private ValidationResult<Enquiry> EnquiryAlreadyExists(int organisationId, int? enquiryId, string name)
        //{
        //    var alreadyExists =
        //        _nidanDataService.RetrieveEnquiries(organisationId,
        //                at =>
        //                    at.FirstName.ToLower() == name.Trim().ToLower() &&
        //                    at.LastName.ToLower() == name.Trim().ToLower() && at.EnquiryId != (enquiryId ?? -1))
        //            .Items.Any();
        //    return new ValidationResult<Enquiry>
        //    {
        //        Succeeded = !alreadyExists,
        //        Errors = alreadyExists ? new List<string> { "Enquiry already exists." } : null
        //    };
        //}

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

        public PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUps(organisationId, predicate, orderBy, paging);
        }

        public FollowUp RetrieveFollowUp(int organisationId, int followUpId)
        {
            return _nidanDataService.RetrieveFollowUp(organisationId, followUpId, e => true);
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

        public PagedResult<Personnel> RetrievePersonnels(int organisationId, Expression<Func<Personnel, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrievePersonnels(organisationId, predicate, orderBy, paging);
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

        //public IEnumerable<Personnel> RetrieveReportsToPersonnel(int organisationId, int personnelId)
        //{
        //    return _nidanDataService.RetrievePersonnel(organisationId, p => p.PersonnelId != personnelId).Items;
        //}

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

        public Personnel RetrievePersonnel(int organisationId, int personnelId, Expression<Func<Personnel, bool>> predicate)
        {
            var personnel = _nidanDataService.RetrievePersonnel(organisationId, personnelId, p => true);
            return personnel;
        }

        //public IEnumerable<Personnel> RetrievePersonnel(int organisationId, IEnumerable<int> companyIds, IEnumerable<int> departmentIds,
        //    IEnumerable<int> divisionIds)
        //{
        //    using (ReadUncommitedTransactionScope)
        //    using (var context = _databaseFactory.Create(organisationId))
        //    {
        //        var personnel = context
        //            .Personnels
        //            //.Include(p => p.Employments.Select(e => e.Division))
        //            .AsNoTracking();

        //        //if (companyIds != null && companyIds.Any())
        //        //    personnel = personnel.Where(p => companyIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().Division.CompanyId));

        //        //if (departmentIds != null && departmentIds.Any())
        //        //    personnel = personnel.Where(p => departmentIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DepartmentId));

        //        //if (divisionIds != null && divisionIds.Any())
        //        //    personnel = personnel.Where(p => divisionIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DivisionId));

        //        return personnel.ToList();
        //    }
        //}

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

        public List<OtherFee> RetrieveOtherFees(int organisationId, Expression<Func<OtherFee, bool>> predicate)
        {
            return _nidanDataService.Retrieve<OtherFee>(organisationId, predicate);
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
            return _nidanDataService.Retrieve<PaymentMode>(organisationId, predicate);
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

        public List<AssignType> RetrieveAssignTypes(int organisationId, Expression<Func<AssignType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AssignType>(organisationId, predicate);
        }

        public List<AspNetRole> RetrieveAspNetRoles(int organisationId, Expression<Func<AspNetRole, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AspNetRole>(organisationId, predicate);
        }

        public List<AssetOutState> RetrieveAssetOutStates(int organisationId, Expression<Func<AssetOutState, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AssetOutState>(organisationId, predicate);
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

        public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<EnquirySearchField, bool>> predicate,
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
            return _nidanDataService.RetrieveCentres(organisationId, predicate).Items.ToList();
        }

        public List<Centre> RetrieveCentres(int organisationId)
        {
            return _nidanDataService.RetrieveCentres(organisationId).Items.ToList();
        }

        public PagedResult<CounsellingSearchField> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CounsellingSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
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

        public PostEvent RetrievePostEvent(int organisationId, int postEventId,
            Expression<Func<PostEvent, bool>> predicate)
        {
            var postevent = _nidanDataService.RetrievePostEvent(organisationId, postEventId, p => true);
            return postevent;
        }

        public PagedResult<PostEvent> RetrievePostEvents(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrievePostEvents(organisationId, p => true, orderBy, paging);
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

        public PagedResult<Holiday> RetrieveHolidays(int organisationId, Expression<Func<Holiday, bool>> predicate,
            List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveHolidays(organisationId, predicate, orderBy, paging);
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

        public List<QuestionType> RetrieveQuestionTypes(int organisationId, Expression<Func<QuestionType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<QuestionType>(organisationId, predicate);
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

        public IEnumerable<SubjectCourse> RetrieveSubjectCourses(int organisationId,
            Expression<Func<SubjectCourse, bool>> predicate)
        {
            return _nidanDataService.RetrieveSubjectCourses(organisationId, predicate);
        }

        public IEnumerable<SubjectTrainer> RetrieveSubjectTrainers(int organisationId, int subjectId)
        {
            return _nidanDataService.RetrieveSubjectTrainers(organisationId, subjectId);
        }


        public IEnumerable<BatchTrainer> RetrieveBatchTrainers(int organisationId, int batchId)
        {
            return _nidanDataService.RetrieveBatchTrainers(organisationId, batchId);
        }

        public IEnumerable<CourseInstallment> RetrieveUnassignedCentreCourseInstallments(int organisationId, int centreId)
        {
            var data = _nidanDataService.RetrieveCourseInstallments(organisationId,
                    a => !a.CentreCourseInstallments.Any(d => d.CentreId == centreId)
                )
                .Items.ToList();
            return data;
        }

        public PagedResult<CentreCourseInstallment> RetrieveCentreCourseInstallments(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreCourseInstallments(organisationId, centreId, orderBy, paging);
        }

        public IEnumerable<Scheme> RetrieveUnassignedCentreSchemes(int organisationId, int centreId)
        {
            return
                _nidanDataService.RetrieveSchemes(organisationId, a => !a.CentreSchemes.Any(d => d.CentreId == centreId),
                    null, null).Items.ToList();
        }

        public PagedResult<CentreScheme> RetrieveCentreSchemes(int organisationId, int centreId,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreSchemes(organisationId, centreId, orderBy, paging);
        }

        public IEnumerable<Sector> RetrieveUnassignedCentreSectors(int organisationId, int centreId)
        {
            return
                _nidanDataService.RetrieveSectors(organisationId, a => !a.CentreSectors.Any(d => d.CentreId == centreId),
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

        public List<Graph> RetrievePieGraphStatistics(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            var month = DateTime.UtcNow.Month;
            var year = DateTime.UtcNow.Year;
            var centre = RetrieveCentres(organisationId, predicate).ToList();
            var graphData = new List<Graph>();
            foreach (var item in centre)
            {
                graphData.Add(new Graph
                {
                    CentreId = item.CentreId,
                    CentreName = item.Name,
                    MobilizationCount =
                        item.Mobilizations.Count(
                            e => e.Close == "No" && e.CreatedDate.Month == month && e.CreatedDate.Year == year),
                    AdmissionCount =
                        item.Admissions.Count(e => e.AdmissionDate.Month == month && e.AdmissionDate.Year == year),
                    EnquiryCount =
                        item.Enquiries.Count(
                            e =>
                                e.IsRegistrationDone == false && e.EnquiryDate.Month == month &&
                                e.EnquiryDate.Year == year),
                    RegistrationCount =
                        item.Registrations.Count(
                            e =>
                                e.IsAdmissionDone == false && e.RegistrationDate.Month == month &&
                                e.RegistrationDate.Year == year),
                    CounsellingCount =
                        item.Counsellings.Count(
                            e =>
                                e.IsRegistrationDone == false && e.CreatedDate.Month == month &&
                                e.CreatedDate.Year == year)
                });
            }
            return graphData;
        }

        public List<Graph> RetrieveBarGraphStatistics(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            var centre = RetrieveCentres(organisationId, predicate).ToList();
            var startOfWeekDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
            var endOfWeekDate = startOfWeekDate.AddDays(6);
            var graphData = new List<Graph>();
            //foreach (var item in centre)
            //{

            //}
            var enquiries =
                RetrieveEnquiries(organisationId,
                        e => e.EnquiryDate >= startOfWeekDate && e.EnquiryDate <= endOfWeekDate)
                    .ToList();
            var mobilizations =
                RetrieveMobilizations(organisationId,
                        e => e.CreatedDate >= startOfWeekDate && e.CreatedDate <= endOfWeekDate)
                    .Items.ToList();
            var registrations =
                RetrieveRegistrations(organisationId,
                        e => e.RegistrationDate >= startOfWeekDate && e.RegistrationDate <= endOfWeekDate)
                    .Items.ToList();
            var admissions =
                RetrieveAdmissions(organisationId,
                        e => e.AdmissionDate >= startOfWeekDate && e.AdmissionDate <= endOfWeekDate)
                    .Items.ToList();

            foreach (var date in endOfWeekDate.RangeFrom(startOfWeekDate))
            {
                graphData.Add(new Graph
                {
                    MobilizationCount = mobilizations.Count(e => e.CreatedDate.Date == date.Date && e.Close == "No"),
                    AdmissionCount = admissions.Count(e => e.AdmissionDate.Date == date.Date),
                    EnquiryCount =
                        enquiries.Count(e => e.EnquiryDate.Date == date.Date && e.IsRegistrationDone == false),
                    RegistrationCount =
                        registrations.Count(e => e.RegistrationDate.Date == date && e.IsAdmissionDone == false),
                    Date = date
                });
            }

            return graphData;
        }

        public List<AssetClass> RetrieveAssetClasses(int organisationId, Expression<Func<AssetClass, bool>> predicate)
        {
            return _nidanDataService.Retrieve<AssetClass>(organisationId, predicate);
        }

        public List<Item> RetrieveItems(int organisationId, Expression<Func<Item, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Item>(organisationId, predicate);
        }

        public Registration RetrieveRegistration(int organisationId, int centreId, int registraionId)
        {
            return _nidanDataService.RetrieveRegistration(organisationId, centreId, registraionId, p => true);
        }

        public PagedResult<FollowUpHistoryData> RetrieveFollowUpHistories(int organisationId,
            Expression<Func<FollowUpHistoryData, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpHistories(organisationId, predicate, orderBy, paging);
        }

        public FollowUpHistory RetrieveFollowUpHistory(int organisationId, int followUpHistoryId,
            Expression<Func<FollowUpHistory, bool>> predicate)
        {
            return _nidanDataService.RetrieveFollowUpHistory(organisationId, followUpHistoryId, predicate);
        }

        public PagedResult<FollowUpSearchField> RetrieveFollowUpBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<FollowUpSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
        }

        public PagedResult<RegistrationGrid> RetrieveRegistrationBySearchKeyword(int organisationId, Expression<Func<RegistrationGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveRegistrationBySearchKeyword(organisationId, predicate,
                orderBy,
                paging);
        }

        public PagedResult<AdmissionGrid> RetrieveAdmissionBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<AdmissionGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissionBySearchKeyword(organisationId, searchKeyword, predicate, orderBy,
                paging);
        }

        public Module RetrieveModule(int organisationId, int id)
        {
            return _nidanDataService.RetrieveModule(organisationId, id, p => true);
        }

        public PagedResult<Module> RetrieveModules(int organisationId, Expression<Func<Module, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveModules(organisationId, predicate, orderBy, paging);
        }

        public Entity.Module RetrieveModule(int organisationId, int moduleId, Expression<Func<Module, bool>> predicate)
        {
            return _nidanDataService.RetrieveModule(organisationId, moduleId, predicate);
        }

        public PagedResult<CandidateInstallmentSearchField> RetrieveCandidateInstallmentBySearchKeyword(
            int organisationId, string searchKeyword, Expression<Func<CandidateInstallmentSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateInstallmentBySearchKeyword(organisationId, searchKeyword,
                predicate, orderBy,
                paging);
        }

        public PagedResult<AdmissionGrid> RetrieveAdmissionGrid(int organisationId,
            Expression<Func<AdmissionGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveAdmissionGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<CandidateInstallmentGrid> RetrieveCandidateInstallmentGrid(int organisationId,
            Expression<Func<CandidateInstallmentGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateInstallmentGrid(organisationId, predicate, orderBy,
                paging);
        }

        public BatchMonth GetBatchDetail(int organisationId, int centreId, int numberOfCourseHours, DateTime startDate,
            int dailyBatchHours, int numberOfWeekDays, int courseFee, int downPayment)
        {
            var hoursPerWeekToWork = dailyBatchHours * numberOfWeekDays;
            var totalNumberOfDays = (numberOfCourseHours / hoursPerWeekToWork) * 7;
            var endDate = startDate.AddDays(totalNumberOfDays);
            //calculate public holiday from startdate and endDate for eg 7
            var date = endDate;
            var publicHoliday =
                RetrieveHolidays(organisationId,
                    e => e.HolidayDate >= startDate && e.HolidayDate <= date && e.CentreId == centreId).Items.Count();
            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            endDate = endDate.AddDays(publicHoliday);
            var assessmentDate = endDate.AddDays(3);
            var numberOfInstallment = months - 2 != 0 ? months - 2 : 1;
            var installmentAmount = (courseFee - downPayment) / (numberOfInstallment != 0 ? numberOfInstallment : 1);
            return new BatchMonth
            {
                StartDate = startDate,
                EndDate = endDate,
                Month = months,
                Holiday = publicHoliday,
                AssessmentDate = assessmentDate,
                NumberOfInstallment = numberOfInstallment,
                InstallmentAmount = installmentAmount
            };
        }

        public BatchMonth GetBatchPlannerDetail(int organisationId, int centreId, int roomId, DateTime startDate, int numberOfCourseHours, int dailyBatchHours, int courseId, int numberOfWeekDays)
        {
            var courseFeeData = RetrieveCourseInstallments(organisationId, e => e.CourseId == courseId);
            var roomData = RetrieveRoom(organisationId, roomId);
            var maxCandidate = Math.Round((decimal)roomData.SquareFeet / 10);
            var maxCourseFee = courseFeeData.Max(e => e.Fee);
            var downPayment = courseFeeData.FirstOrDefault(e => e.Fee == maxCourseFee)?.DownPayment;
            var hoursPerWeekToWork = dailyBatchHours * numberOfWeekDays;
            var totalNumberOfDays = (numberOfCourseHours / hoursPerWeekToWork) * 7;
            var endDate = startDate.AddDays(totalNumberOfDays);
            //calculate public holiday from startdate and endDate for eg 7
            var date = endDate;
            var publicHoliday = RetrieveHolidays(organisationId, e => e.HolidayDate >= startDate && e.HolidayDate <= date && e.CentreId == centreId).Items.Count();
            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            var numberOfBatch = Math.Round((decimal)12 / months);
            endDate = endDate.AddDays(publicHoliday);
            var assessmentDate = endDate.AddDays(3);
            var numberOfInstallment = months - 2 != 0 ? months - 2 : 1;
            var installmentAmount = (maxCourseFee - downPayment) / (numberOfInstallment != 0 ? numberOfInstallment : 1);
            return new BatchMonth
            {
                StartDate = startDate,
                EndDate = endDate,
                Month = months,
                Holiday = publicHoliday,
                AssessmentDate = assessmentDate,
                NumberOfInstallment = numberOfInstallment,
                InstallmentAmount = installmentAmount ?? 0,
                MaximumCandidate = (int)maxCandidate,
                NumberOfBatch = (int)numberOfBatch
            };
        }

        public PagedResult<ExpenseHeader> RetrieveExpenseHeaders(int organisationId,
            Expression<Func<ExpenseHeader, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveExpenseHeaders(organisationId, predicate, orderBy, paging);
        }

        public ExpenseHeader RetrieveExpenseHeader(int organisationId, int expenseHeaderId,
            Expression<Func<ExpenseHeader, bool>> predicate)
        {
            return _nidanDataService.RetrieveExpenseHeader(organisationId, expenseHeaderId, predicate);
        }

        public PagedResult<OtherFee> RetrieveOtherFees(int organisationId, int centreId,
            Expression<Func<OtherFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveOtherFees(organisationId, centreId, predicate, orderBy, paging);
        }

        public OtherFee RetrieveOtherFee(int organisationId, int centreId, int otherFeeId,
            Expression<Func<OtherFee, bool>> predicate)
        {
            return _nidanDataService.RetrieveOtherFee(organisationId, centreId, otherFeeId, predicate);
        }

        public PagedResult<Expense> RetrieveExpenses(int organisationId, int centreId,
            Expression<Func<Expense, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveExpenses(organisationId, centreId, predicate, orderBy, paging);
        }

        public Expense RetrieveExpense(int organisationId, int centreId, int expenseId,
            Expression<Func<Expense, bool>> predicate)
        {
            return _nidanDataService.RetrieveExpense(organisationId, centreId, expenseId, predicate);
        }

        public IEnumerable<ExpenseProject> RetrieveExpenseProjects(int organisationId, int centreId, int expenseId)
        {
            return _nidanDataService.RetrieveExpenseProjects(organisationId, centreId, expenseId);
        }

        public List<Project> RetrieveProjects(int organisationId, int projectId,
            Expression<Func<Project, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Project>(organisationId, p => p.ProjectId == projectId).ToList();
        }

        public PagedResult<Project> RetrieveProjects(int organisationId, Expression<Func<Project, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveProjects(organisationId, predicate, orderBy, paging);
        }

        public Project RetrieveProject(int organisationId, int projectId, Expression<Func<Project, bool>> predicate)
        {
            return _nidanDataService.RetrieveProject(organisationId, projectId, predicate);
        }

        public PagedResult<CentrePettyCash> RetrieveCentrePettyCashs(int organisationId, int centreId,
            Expression<Func<CentrePettyCash, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCentrePettyCashs(organisationId, centreId, predicate, orderBy, paging);
        }

        public CentrePettyCash RetrieveCentrePettyCash(int organisationId, int centreId, int centrePettyCashId,
            Expression<Func<CentrePettyCash, bool>> predicate)
        {
            return _nidanDataService.RetrieveCentrePettyCash(organisationId, centreId, centrePettyCashId, predicate);
        }

        public PagedResult<CandidateFeeGrid> RetrieveCandidateFeeGrid(int organisationId,
            Expression<Func<CandidateFeeGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateFeeGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId,
            Expression<Func<MobilizationDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizationDataGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<EnquiryDataGrid> RetrieveEnquiryDataGrid(int organisationId,
            Expression<Func<EnquiryDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEnquiryDataGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<FollowUpDataGrid> RetrieveFollowUpDataGrid(int organisationId,
            Expression<Func<FollowUpDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFollowUpDataGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<Voucher> RetrieveVouchers(int organisationId, int centreId,
            Expression<Func<Voucher, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveVouchers(organisationId, centreId, predicate, orderBy, paging);
        }

        public Voucher RetrieveVoucher(int organisationId, int centreId, int voucherId,
            Expression<Func<Voucher, bool>> predicate)
        {
            return _nidanDataService.RetrieveVoucher(organisationId, centreId, voucherId, predicate);
        }

        public PagedResult<VoucherGrid> RetrieveVoucherGrids(int organisationId, int centreId,
            Expression<Func<VoucherGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveVoucherGrids(organisationId, centreId, predicate, orderBy, paging);
        }

        public PagedResult<RegistrationGrid> RetrieveRegistrationGrid(int organisationId,
            Expression<Func<RegistrationGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveRegistrationGrid(organisationId, predicate, orderBy, paging);
        }

        public CentreVoucherNumber RetrieveCentreVoucherNumber(int organisationId, int centreId,
            Expression<Func<CentreVoucherNumber, bool>> predicate)
        {
            return _nidanDataService.RetrieveCentreVoucherNumber(organisationId, centreId, predicate);
        }

        public PagedResult<Attendance> RetrieveAttendances(int organisationId,
            Expression<Func<Attendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAttendances(organisationId, predicate, orderBy, paging);
        }

        public Attendance RetrieveAttendance(int organisationId, int attendanceId,
            Expression<Func<Attendance, bool>> predicate)
        {
            var attendance = _nidanDataService.RetrieveAttendance(organisationId, attendanceId, p => true);
            return attendance;
        }

        public Attendance RetrieveAttendance(int organisationId, int id)
        {
            return _nidanDataService.RetrieveAttendance(organisationId, id, p => true);
        }

        public PagedResult<BatchAttendance> RetrieveBatchAttendances(int organisationId,
            Expression<Func<BatchAttendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchAttendances(organisationId, predicate, orderBy, paging);
        }

        public BatchAttendance RetrieveBatchAttendance(int organisationId, int batchAttendanceId,
            Expression<Func<BatchAttendance, bool>> predicate)
        {
            var batchAttendance = _nidanDataService.RetrieveBatchAttendance(organisationId, batchAttendanceId, p => true);
            return batchAttendance;
        }

        public BatchAttendance RetrieveBatchAttendance(int organisationId, int id)
        {
            return _nidanDataService.RetrieveBatchAttendance(organisationId, id, p => true);
        }

        public PagedResult<AttendanceGrid> RetrieveAttendanceGrid(int organisationId,
            Expression<Func<AttendanceGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveAttendanceGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<CounsellingDataGrid> RetrieveCounsellingGrid(int organisationId,
            Expression<Func<CounsellingDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveCounsellingGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<ExpenseDataGrid> RetrieveExpenseDataGrid(int organisationId,
            Expression<Func<ExpenseDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveExpenseDataGrid(organisationId, predicate, orderBy, paging);
        }

        public IEnumerable<ExpensePettyCashData> RetrieveExpensePettyCashDataByCentre(int organisationId, int centreId,
            DateTime startDate, DateTime endDate)
        {
            var totalCashAvailTilldate = _nidanDataService.RetrieveCentrePettyCashs(organisationId, centreId, c => true).Items.Sum(e => e.Amount);
            return null;
        }

        public PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId, string searchKeyword, Expression<Func<MobilizationDataGrid, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveMobilizationDataGrid(organisationId, searchKeyword, predicate,
                orderBy, paging);
        }

        public PagedResult<PettyCashExpenseReport> RetrievePettyCashExpenseReports(int organisationId, Expression<Func<PettyCashExpenseReport, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrievePettyCashExpenseReports(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<EventBrainstorming> RetrieveEventBrainstormings(int organisationId, int centreId, Expression<Func<EventBrainstorming, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveEventBrainstormings(organisationId, predicate, orderBy, paging);
        }

        public EventBrainstorming RetrieveEventBrainstorming(int organisationId, int centreId, int eventBrainstormingId,
            Expression<Func<EventBrainstorming, bool>> predicate)
        {
            var eventBrainstorming = _nidanDataService.RetrieveEventBrainstorming(organisationId, eventBrainstormingId, p => true);
            return eventBrainstorming;
        }

        public PagedResult<Registration> RetrieveRegistrationSummaryByDate(int organisationId, int centreId, DateTime date, Expression<Func<Registration, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            var candidateFeeData = RetrieveCandidateFees(organisationId, e => e.CentreId == centreId && e.FeeTypeId == (int)FeeType.Registration && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year);
            var studentCodes = candidateFeeData.Items.Select(e => e.StudentCode).ToList();
            var registrationData = RetrieveRegistrations(organisationId, e => e.CentreId == centreId && studentCodes.Contains(e.StudentCode));
            return registrationData;
        }

        public PagedResult<SummaryReport> RetrieveDownpaymentSummaryByDate(int organisationId, int centreId, DateTime date, Expression<Func<SummaryReport, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            //var candidateFeeData = RetrieveCandidateFees(organisationId, e => e.CentreId == centreId && e.FeeTypeId == (int)FeeType.Admission && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year);
            var candidateFeeData = RetrieveSummaryReports(organisationId, centreId, e => e.CentreId == centreId && e.FeeTypeId == (int)FeeType.Admission && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year);
            //var studentCodes = candidateFeeData.Select(e => e.StudentCode).ToList();
            //var registrationData = RetrieveRegistrations(organisationId, e => e.CentreId == centreId && studentCodes.Contains(e.StudentCode));
            return candidateFeeData;
        }

        public PagedResult<SummaryReport> RetrieveInstallmentSummaryByDate(int organisationId, int centreId, DateTime date, Expression<Func<SummaryReport, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            //var candidateFeeData = RetrieveCandidateFees(organisationId, e => e.CentreId == centreId && e.FeeTypeId == (int)FeeType.Admission && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year);
            var candidateFeeData = RetrieveSummaryReports(organisationId, centreId, e => e.ISPaymentDone && e.CentreId == centreId && e.FeeTypeId == (int)FeeType.Installment && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year);
            //var studentCodes = candidateFeeData.Select(e => e.StudentCode).ToList();
            //var registrationData = RetrieveRegistrations(organisationId, e => e.CentreId == centreId && studentCodes.Contains(e.StudentCode));
            return candidateFeeData;
        }

        public IEnumerable<MobilizationSummaryReport> RetriveMobilizationCountReportByMonthWise(int organisationId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var _today = DateTime.UtcNow;
            var date = _today.Date;
            var mobilizationSummaryReports = new List<MobilizationSummaryReport>();
            var data = _nidanDataService.RetriveMobilizationCountReportByMonthWise(organisationId, e => e.Month == _today.Month && e.Year == _today.Year).Items.ToList();
            var centres = _nidanDataService.RetrieveCentres(organisationId, e => true).Items.ToList();
            foreach (var centre in centres)
            {
                var result = data.FirstOrDefault(e => e.CentreId == centre.CentreId);
                mobilizationSummaryReports.Add(new MobilizationSummaryReport()
                {
                    CentreId = result?.CentreId ?? centre.CentreId,
                    MobilizationCount = result?.MobilizationCount ?? 0,
                    CentreName = result?.CentreName ?? centre.Name,
                    AdmissionCount = result?.AdmissionCount ?? 0,
                    EnquiryCount = result?.EnquiryCount ?? 0,
                    RegistrationCount = result?.RegistrationCount ?? 0,
                    CounsellingCount = result?.CounsellingCount ?? 0,
                    CourseBooking = result?.CourseBooking ?? 0,
                    FeeCollected = result?.FeeCollected ?? 0,
                    Year = _today.Year
                });
            }
            mobilizationSummaryReports.Add(new MobilizationSummaryReport()
            {
                MonthName = "Total",
                CounsellingCount = mobilizationSummaryReports.Sum(e => e.CounsellingCount),
                CourseBooking = mobilizationSummaryReports.Sum(e => e.CourseBooking),
                EnquiryCount = mobilizationSummaryReports.Sum(e => e.EnquiryCount),
                FeeCollected = mobilizationSummaryReports.Sum(e => e.FeeCollected),
                RegistrationCount = mobilizationSummaryReports.Sum(e => e.RegistrationCount),
                AdmissionCount = mobilizationSummaryReports.Sum(e => e.AdmissionCount),
                MobilizationCount = mobilizationSummaryReports.Sum(e => e.MobilizationCount)
            });
            return mobilizationSummaryReports;
        }

        public CentreCandidateFeeSummaryReport RetriveCentreCandidateFeeByDate(int organisationId, int centreId, DateTime date)
        {
            var candidatefeeData = RetrieveCandidateFees(organisationId, e => e.CentreId == centreId && e.PaymentDate.Value.Day == date.Day && e.PaymentDate.Value.Month == date.Month && e.PaymentDate.Value.Year == date.Year).Items.ToList();
            var totalRegistrationAmount = candidatefeeData.Where(e => e.FeeTypeId == 1).Sum(e => e.PaidAmount);
            var totalInstallmentAmount = candidatefeeData.Where(e => e.FeeTypeId == 3).Sum(e => e.PaidAmount);
            var totalDownPaymentAmount = candidatefeeData.Where(e => e.FeeTypeId == 2).Sum(e => e.PaidAmount);
            var centreName = candidatefeeData.FirstOrDefault()?.Centre.Name ?? string.Empty;
            var centreCandidateFeeSummaryReport = new CentreCandidateFeeSummaryReport()
            {
                TotalRegistrationAmount = totalRegistrationAmount ?? 0,
                TotalDownPaymentAmount = totalDownPaymentAmount ?? 0,
                TotalInstallmentAmount = totalInstallmentAmount ?? 0,
                CentreName = centreName,
                Date = date.ToString("dd-MM-yyyy"),
                CentreId = centreId
            };
            return centreCandidateFeeSummaryReport;
        }

        public IEnumerable<MobilizationSummaryReport> RetriveMobilizationCountReportByMonthAndYear(int organisationId, int centreId, int year, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var startFiscalDate = new DateTime(year, 04, 01);
            var endFiscalDate = new DateTime(startFiscalDate.AddYears(1).Year, 03, 31);
            var mobilizationSummaryReports = new List<MobilizationSummaryReport>();
            var data = _nidanDataService.RetriveMobilizationCountReportByMonthAndYear(organisationId, centreId, e => e.CentreId == centreId).Items.ToList();
            var months = DateTimeExtensions.EachMonth(startFiscalDate, endFiscalDate);
            foreach (var item in months)
            {
                var result = data.FirstOrDefault(e => e.Month == item.Month && e.Year == item.Year);
                mobilizationSummaryReports.Add(new MobilizationSummaryReport()
                {
                    CentreId = result?.CentreId ?? 0,
                    MobilizationCount = result?.MobilizationCount ?? 0,
                    CentreName = result?.CentreName ?? String.Empty,
                    AdmissionCount = result?.AdmissionCount ?? 0,
                    EnquiryCount = result?.EnquiryCount ?? 0,
                    RegistrationCount = result?.RegistrationCount ?? 0,
                    CounsellingCount = result?.CounsellingCount ?? 0,
                    CourseBooking = result?.CourseBooking ?? 0,
                    FeeCollected = result?.FeeCollected ?? 0,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month),
                    Year = item.Year,
                    Month = item.Month
                });
            }
            mobilizationSummaryReports.Add(new MobilizationSummaryReport()
            {
                MonthName = "Total",
                CounsellingCount = mobilizationSummaryReports.Sum(e => e.CounsellingCount),
                CourseBooking = mobilizationSummaryReports.Sum(e => e.CourseBooking),
                EnquiryCount = mobilizationSummaryReports.Sum(e => e.EnquiryCount),
                FeeCollected = mobilizationSummaryReports.Sum(e => e.FeeCollected),
                RegistrationCount = mobilizationSummaryReports.Sum(e => e.RegistrationCount),
                AdmissionCount = mobilizationSummaryReports.Sum(e => e.AdmissionCount),
                MobilizationCount = mobilizationSummaryReports.Sum(e => e.MobilizationCount)
            });
            return mobilizationSummaryReports;
        }

        public IEnumerable<MobilizationSummaryReport> RetriveMobilizationCountReportByDate(int organisationId, int centreId, int year, int month, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var mobilizationSummaryReports = new List<MobilizationSummaryReport>();
            var days = DateTimeExtensions.EachDay(firstDayOfMonth, lastDayOfMonth);
            var data = _nidanDataService.RetriveMobilizationCountReportByDate(organisationId, centreId, e => e.Date >= firstDayOfMonth && e.Date <= lastDayOfMonth && e.CentreId == centreId, orderBy, paging).Items.ToList();
            foreach (var item in days)
            {
                var result = data.FirstOrDefault(e => e.Date.Month == item.Month && e.Date.Day == item.Day && e.Date.Year == item.Year);
                mobilizationSummaryReports.Add(new MobilizationSummaryReport()
                {
                    CentreId = result?.CentreId ?? 0,
                    MobilizationCount = result?.MobilizationCount ?? 0,
                    CentreName = result?.CentreName ?? String.Empty,
                    AdmissionCount = result?.AdmissionCount ?? 0,
                    EnquiryCount = result?.EnquiryCount ?? 0,
                    RegistrationCount = result?.RegistrationCount ?? 0,
                    CounsellingCount = result?.CounsellingCount ?? 0,
                    CourseBooking = result?.CourseBooking ?? 0,
                    FeeCollected = result?.FeeCollected ?? 0,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month),
                    Year = item.Year,
                    Month = item.Month,
                    Date = item.ToShortDateString()
                });
            }
            mobilizationSummaryReports.Add(new MobilizationSummaryReport()
            {
                Date = "Total",
                CounsellingCount = mobilizationSummaryReports.Sum(e => e.CounsellingCount),
                CourseBooking = mobilizationSummaryReports.Sum(e => e.CourseBooking),
                EnquiryCount = mobilizationSummaryReports.Sum(e => e.EnquiryCount),
                FeeCollected = mobilizationSummaryReports.Sum(e => e.FeeCollected),
                RegistrationCount = mobilizationSummaryReports.Sum(e => e.RegistrationCount),
                AdmissionCount = mobilizationSummaryReports.Sum(e => e.AdmissionCount),
                MobilizationCount = mobilizationSummaryReports.Sum(e => e.MobilizationCount)
            });
            return mobilizationSummaryReports;
        }

        public Gst RetrieveGst(int organisationId, Expression<Func<Gst, bool>> predicate)
        {
            var gst = _nidanDataService.RetrieveGst(organisationId, predicate);
            return gst;
        }

        public PagedResult<Gst> RetrieveGsts(int organisationId, Expression<Func<Gst, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveGsts(organisationId, predicate, orderBy, paging);
        }

        public EventBudget RetrieveEventBudget(int organisationId, int centreId, int eventBudgetId, Expression<Func<EventBudget, bool>> predicate)
        {
            var eventBudget = _nidanDataService.RetrieveEventBudget(organisationId, centreId, eventBudgetId, predicate);
            return eventBudget;
        }

        public PagedResult<EventBudget> RetrieveEventBudgets(int organisationId, int centreId, Expression<Func<EventBudget, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveEventBudgets(organisationId, centreId, predicate, orderBy, paging);
        }

        public EventPlanning RetrieveEventPlanning(int organisationId, int centreId, int eventPlanningId, Expression<Func<EventPlanning, bool>> predicate)
        {
            var eventPlanning = _nidanDataService.RetrieveEventPlanning(organisationId, centreId, eventPlanningId, predicate);
            return eventPlanning;
        }

        public PagedResult<EventPlanning> RetrieveEventPlannings(int organisationId, int centreId, Expression<Func<EventPlanning, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveEventPlannings(organisationId, centreId, predicate, orderBy, paging);
        }

        public CentreReceiptSetting RetrieveCentreReceiptSetting(int organisationId, Expression<Func<CentreReceiptSetting, bool>> predicate)
        {
            var centreReceiptSetting = _nidanDataService.RetrieveCentreReceiptSetting(organisationId, predicate);
            return centreReceiptSetting;
        }

        public PagedResult<CentreReceiptSetting> RetrieveCentreReceiptSettings(int organisationId, Expression<Func<CentreReceiptSetting, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreReceiptSettings(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<CentreEnrollmentReceiptSetting> RetrieveCentreEnrollmentReceiptSettings(int organisationId, Expression<Func<CentreEnrollmentReceiptSetting, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCentreEnrollmentReceiptSettings(organisationId, predicate, orderBy, paging);
        }

        public CentreEnrollmentReceiptSetting RetrieveCentreEnrollmentReceiptSetting(int organisationId, Expression<Func<CentreEnrollmentReceiptSetting, bool>> predicate)
        {
            var centreEnrollmentReceiptSetting = _nidanDataService.RetrieveCentreEnrollmentReceiptSetting(organisationId, predicate);
            return centreEnrollmentReceiptSetting;
        }

        public PagedResult<BiometricAttendanceGrid> RetrieveBiometricAttendanceGrid(int organisationId, Expression<Func<BiometricAttendanceGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveBiometricAttendanceGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<EventBrainStormingGrid> RetrieveEventBrainStormingGrid(int organisationId, Expression<Func<EventBrainStormingGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveEventBrainStormingGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<StockPurchase> RetrieveStockPurchases(int organisationId, int centreId, Expression<Func<StockPurchase, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveStockPurchases(organisationId, centreId, predicate, orderBy, paging);
        }

        public StockPurchase RetrieveStockPurchase(int organisationId, int centreId, int stockPurchaseId, Expression<Func<StockPurchase, bool>> predicate)
        {
            var stockPurchase = _nidanDataService.RetrieveStockPurchase(organisationId, stockPurchaseId, predicate);
            return stockPurchase;
        }

        public PagedResult<StockIssue> RetrieveStockIssues(int organisationId, int centreId, Expression<Func<StockIssue, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveStockIssues(organisationId, centreId, predicate, orderBy, paging);
        }

        public StockIssue RetrieveStockIssue(int organisationId, int centreId, int stockIssueId, Expression<Func<StockIssue, bool>> predicate)
        {
            var stockIssue = _nidanDataService.RetrieveStockIssue(organisationId, centreId, stockIssueId, predicate);
            return stockIssue;
        }

        public StockPurchase RetrieveStockPurchase(int organisationId, int id)
        {
            return _nidanDataService.RetrieveStockPurchase(organisationId, id, p => true);
        }

        public PagedResult<StockDataGrid> RetrieveStockDataGrid(int organisationId, Expression<Func<StockDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveStockDataGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<StockDataGrid> RetrieveStockDataGrid(int organisationId, string searchKeyword, Expression<Func<StockDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveStockDataGrid(organisationId, searchKeyword, predicate, orderBy, paging);
        }

        public List<StockType> RetrieveStockTypes(int organisationId, Expression<Func<StockType, bool>> predicate)
        {
            return _nidanDataService.Retrieve<StockType>(organisationId, predicate);
        }

        public PagedResult<StockReportDataGrid> RetrieveStockReportDataGrid(int organisationId, Expression<Func<StockReportDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveStockReportDataGrid(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<BatchPlanner> RetrieveBatchPlanners(int organisationId, Expression<Func<BatchPlanner, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchPlanners(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<BatchPlannerGrid> RetrieveBatchPlannerGrids(int organisationId, Expression<Func<BatchPlannerGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchPlannerGrids(organisationId, predicate, orderBy, paging);
        }

        public BatchPlanner RetrieveBatchPlanner(int organisationId, int batchPlannerId, Expression<Func<BatchPlanner, bool>> predicate)
        {
            return _nidanDataService.RetrieveBatchPlanner(organisationId, batchPlannerId, predicate);
        }

        public PagedResult<BatchPlannerDay> RetrieveBatchPlannerDays(int organisationId, Expression<Func<BatchPlannerDay, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchPlannerDays(organisationId, predicate, orderBy, paging);
        }

        public BatchPlannerDay RetrieveBatchPlannerDay(int organisationId, int batchPlannerDayId, Expression<Func<BatchPlannerDay, bool>> predicate)
        {
            return _nidanDataService.RetrieveBatchPlannerDay(organisationId, batchPlannerDayId, predicate);
        }

        public List<Product> RetrieveProducts(int organisationId, Expression<Func<Product, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Product>(organisationId, predicate);
        }

        public FixAsset RetrieveFixAsset(int organisationId, int fixAssetId, Expression<Func<FixAsset, bool>> predicate)
        {
            return _nidanDataService.RetrieveFixAsset(organisationId, fixAssetId, predicate);
        }

        public PagedResult<FixAssetDataGrid> RetrieveFixAssetDataGrid(int organisationId, Expression<Func<FixAssetDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveFixAssetDataGrid(organisationId, predicate, orderBy, paging);
        }

        public List<StudentKit> RetrieveStudentKits(int organisationId, Expression<Func<StudentKit, bool>> predicate)
        {
            return _nidanDataService.Retrieve<StudentKit>(organisationId, predicate);
        }


        public List<StudentAttendance> RetrieveStudentAttendanceByBatchId(int organisationId, int personnelId, int batchId, DateTime date)
        {
            var allCandidatesByBatchId = RetrieveAdmissionGrid(organisationId, e => e.BatchId == batchId).Items.ToList();
            var studentCodes = allCandidatesByBatchId.Select(e => e.StudentCode);
            //var candidatesByBatchId = RetrieveAttendanceGrid(organisationId, e => e.BatchId == batchId && studentCodes.Contains(e.StudentCode)).Items.ToList();
            var todaysBiometricAttendance = RetrieveBiometricAttendanceGrid(organisationId, e => studentCodes.Contains(e.StudentCode) && DbFunctions.TruncateTime(e.LogDateTime) == date);
            var todaysAttendance = RetrieveAttendances(organisationId, e => studentCodes.Contains(e.StudentCode) && DbFunctions.TruncateTime(e.AttendanceDate) == date && e.IsPresent == true);
            var studentAttendance = new List<StudentAttendance>();
            foreach (var candidate in allCandidatesByBatchId)
            {
                var biometricResult = todaysBiometricAttendance.Items.FirstOrDefault(e => e.StudentCode == candidate.StudentCode && e.Direction == "I");
                var attendanceResult = todaysAttendance.Items.FirstOrDefault(e => e.StudentCode == candidate.StudentCode);
                studentAttendance.Add(new StudentAttendance()
                {
                    CandidateName = candidate.CandidateName,
                    StudentCode = candidate.StudentCode,
                    PersonnelId = personnelId,
                    InHour = 0,
                    InMinute = 0,
                    InTimeSpan = String.Empty,
                    OutHour = 0,
                    OutMinute = 0,
                    OutTimeSpan = String.Empty,
                    IsPresent = biometricResult != null || attendanceResult != null,
                    AttendanceDate = date,
                    CentreId = candidate.CentreId,
                    OrganisationId = organisationId,
                    BiometricLogTime = biometricResult?.LogDateTime.ToString(),
                    Direction = biometricResult?.Direction,
                    Topic = String.Empty
                });
            }
            return studentAttendance;
        }

        public PagedResult<BatchAttendanceDataGrid> RetrieveBatchAttendanceDataGrid(int organisationId, Expression<Func<BatchAttendanceDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveBatchAttendanceDataGrid(organisationId, predicate, orderBy, paging);
        }


        public PagedResult<FixAsset> RetrieveFixAssets(int organisationId, Expression<Func<FixAsset, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveFixAssets(organisationId, predicate, orderBy, paging);
        }

        public PagedResult<SummaryReport> RetrieveSummaryReports(int organisationId, int centreId, Expression<Func<SummaryReport, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveSummaryReports(organisationId, centreId, predicate, orderBy, paging);
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
        //    //                CreatedByName = personnelId.ToString(),
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

        public Admission RetrieveAdmission(int organisationId, int centreId, int id)
        {
            return _nidanDataService.RetrieveAdmission(organisationId, id, a => a.CentreId == centreId);
        }

        public List<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate)
        {
            return _nidanDataService.Retrieve<Batch>(organisationId, predicate);
        }


        public PagedResult<CandidateFee> RetrieveCandidateFees(int organisationId,
            Expression<Func<CandidateFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var data = _nidanDataService.RetrieveCandidateFees(organisationId, predicate, orderBy, paging);
            return data;
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

        public PagedResult<CandidateFeeSearchField> RetrieveCandidateFeeBySearchKeyword(int organisationId, string searchKeyword,
            Expression<Func<CandidateFeeSearchField, bool>> predicate,
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

        public List<Room> RetrieveRooms(int organisationId, int centreId, Expression<Func<Room, bool>> predicate)
        {
            var roomAvailables = _nidanDataService.RetrieveRoomAvailables(organisationId, centreId, e => true);
            var rooms =
                 _nidanDataService.RetrieveRooms(organisationId, predicate).Items.ToList();
            return rooms;
        }

        public List<RoomAvailable> RetrieveRoomAvailables(int organisationId, int centreId, Expression<Func<RoomAvailable, bool>> predicate)
        {
            var roomAvailable =
                _nidanDataService.RetrieveRoomAvailables(organisationId, centreId, predicate).ToList();
            return roomAvailable;
        }

        public List<Trainer> RetrieveTrainers(int organisationId, int centreId, Expression<Func<Trainer, bool>> predicate)
        {
            var trainers =
                _nidanDataService.RetrieveTrainers(organisationId, predicate).Items.ToList();
            return trainers;
        }

        public Registration RetrieveRegistration(int organisationId, int id)
        {
            return _nidanDataService.RetrieveRegistration(organisationId, id, r => true);
        }

        //Update

        public PagedResult<CandidateAssessmentQuestionAnswer> RetrieveCandidateAssessmentQuestionAnswers(int organisationId, Expression<Func<CandidateAssessmentQuestionAnswer, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveCandidateAssessmentQuestionAnswers(organisationId, predicate, orderBy, paging);
        }

        public CandidateAssessmentQuestionAnswer RetrieveCandidateAssessmentQuestionAnswer(int organisationId,
            int candidateAssessmentQuestionAnswerId)
        {
            return _nidanDataService.RetrieveCandidateAssessmentQuestionAnswer(organisationId,candidateAssessmentQuestionAnswerId);
        }

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
                enquiryFollowUp.Course = null;
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
                enquiryCounselling.Course = null;
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
                mobilization.Remark = followUp.Remark;
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
                OrganisationId = organisationId,
                FollowUpBy = followUp.CreatedBy
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
                enquiryFollowUp.Course = null;
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
            var subjectCourseId = RetrieveSubjectCourses(organisationId, s => s.SubjectId == subject.SubjectId);
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
            // Retrieve CentreRecieptsetting where centreId = 
            var centreRecieptsettingData = _nidanDataService.RetrieveCentreReceiptSetting(organisationId, e => e.CentreId == candidateFee.CentreId);
            var receiptNumber = string.Format("{0}/{1}/{2}", centreRecieptsettingData.TaxYear, centreRecieptsettingData.Centre.CentreCode, centreRecieptsettingData.ReceiptNumber);
            candidateFee.ReceiptNumber = receiptNumber;
            // Increment RecieptNo by and Update.
            centreRecieptsettingData.ReceiptNumber = centreRecieptsettingData.ReceiptNumber + 1;
            centreRecieptsettingData.TaxYear = DateTime.UtcNow.FiscalYear();
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreRecieptsettingData);
            candidateFee.PaymentMode = null;
            var data = _nidanDataService.UpdateOrganisationEntityEntry<CandidateFee>(organisationId, candidateFee);

            //Send Email
            //SendCandidateInstallmentEmail(organisationId, candidateFee.CentreId, data);

            //Send SMS
            //SendInstallmetnSms(candidateFee);
            return data;
        }

        public Registration UpdateRegistartion(int organisationId, Registration registration)
        {
            // Update Paid Amount in CreateCandidateFee
            var candidateFeeData = RetrieveCandidateFee(organisationId, registration.CandidateFeeId);
            candidateFeeData.PaidAmount = registration.CandidateFee.PaidAmount;
            candidateFeeData.PaymentModeId = registration.CandidateFee.PaymentModeId;
            candidateFeeData.BankName = registration.CandidateFee.BankName;
            candidateFeeData.ChequeNumber = registration.CandidateFee.ChequeNumber;
            candidateFeeData.ChequeDate = registration.CandidateFee.ChequeDate;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateFeeData);
            // Update CandidateInstallment PaymentMethod
            var candidateInstallmentData = RetrieveCandidateInstallment(organisationId, registration.CandidateInstallmentId, e => true);
            candidateInstallmentData.PaymentMethod = registration.CandidateInstallment.PaymentMethod;
            _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateInstallmentData);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, registration);
        }

        public Module UpdateModule(int organisationId, Module module)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, module);
        }

        public ExpenseHeader UpdateExpenseHeader(int organisationId, ExpenseHeader expenseHeader)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, expenseHeader);
        }

        public OtherFee UpdateOtherFee(int organisationId, int centreId, OtherFee otherFee)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, otherFee);
        }

        public Expense UpdateExpense(int organisationId, int centreId, Expense expense, List<int> projectIds)
        {
            if (!expense.ExpenseProjects.Any() && projectIds.Any())
                CreateExpenseProject(organisationId, expense.CentreId, expense.ExpenseId, projectIds);
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, expense);
        }

        public CentrePettyCash UpdateCentrePettyCash(int organisationId, int centreId, int personnelId, CentrePettyCash centrePettyCash)
        {
            centrePettyCash.OrganisationId = organisationId;
            centrePettyCash.CentreId = centreId;
            centrePettyCash.CreatedBy = personnelId;
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centrePettyCash);
        }

        public Attendance UpdateAttendance(int organisationId, Attendance attendance)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, attendance);
        }

        public BatchAttendance UpdateBatchAttendance(int organisationId, BatchAttendance batchAttendance)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, batchAttendance);
        }

        public CentreVoucherNumber UpdateCentreVoucherNumber(int organisationId, int centreId, CentreVoucherNumber centreVoucherNumber)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreVoucherNumber);
        }

        public CentreReceiptSetting UpdateCentreReceiptSetting(int organisationId, int centreId,
            CentreReceiptSetting centreReceiptSetting)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreReceiptSetting);
        }

        public CentreEnrollmentReceiptSetting UpdateCentreEnrollmentReceiptSetting(int organisationId, int centreId,
            CentreEnrollmentReceiptSetting centreEnrollmentReceiptSetting)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreEnrollmentReceiptSetting);
        }

        public StockIssue UpdateStockIssue(int organisationId, int centreId, StockIssue stockIssue)
        {
            var stockPurchaseData = RetrieveStockPurchases(organisationId, centreId, e => e.StockPurchaseId == stockIssue.StockPurchaseId).Items.FirstOrDefault();
            if (stockPurchaseData != null)
            {
                var stockIssueData = new StockIssue()
                {
                    IssuedDate = stockIssue.IssuedDate,
                    IssuedQuantity = stockIssue.IssuedQuantity,
                    IssuedToPerson = stockIssue.IssuedToPerson,
                    BalanceQuantity = (stockPurchaseData.Quantity - stockIssue.IssuedQuantity)
                };
            }
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, stockIssue);
            _nidanDataService.Create<StockIssue>(organisationId, stockIssue);
            return data;
        }

        public StockPurchase UpdateStockPurchase(int organisationId, int centreId, StockPurchase stockPurchase)
        {
            var stockIssueData = new StockIssue()
            {
                //IssuedDate = stockIssue.IssuedDate,
                //IssuedQuantity = stockIssue.IssuedQuantity,
                //IssuedToPerson = stockIssue.IssuedToPerson,
                //BalanceQuantity = (stockPurchaseData.Quantity - stockIssue.IssuedQuantity)
            };
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, stockPurchase);
            return data;
        }

        public FixAsset UpdateFixAsset(int organisationId, FixAsset fixAsset)
        {
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, fixAsset);
            return data;
        }

        public CentreProductSetting UpdateCentreProductSetting(int organisationId, CentreProductSetting centreProductSetting)
        {
            var data = _nidanDataService.UpdateOrganisationEntityEntry(organisationId, centreProductSetting);
            return data;
        }

        public BatchPlanner UpdateBatchPlanner(int organisationId, BatchPlanner batchPlanner, BatchPlannerDay batchPlannerDay)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, batchPlanner);
        }

        public FixAssetMapping UpdateFixAssetMapping(int organisationId, FixAssetMapping fixAssetMapping)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, fixAssetMapping);
        }

        public bool AssignFixAssetMapping(int organisationId, int personnelId, int centreId, List<FixAssetMapping> fixAssetMappings)
        {
            try
            {
                foreach (var fixAssetMapping in fixAssetMappings)
                {
                    fixAssetMapping.AssetCode = String.Format("Nest/{0}/{1}/{2}/{3}", fixAssetMapping.Centre.CentreCode, fixAssetMapping.AssetOutOwner, fixAssetMapping.FixAsset.Item.ItemCode, fixAssetMapping.ItemSettingId); //add ItemId in FixAssetMapping
                    fixAssetMapping.AssetOutStatusId = (int)AssetOutStatus.InUse;
                    fixAssetMapping.CreatedBy = personnelId;
                    UpdateFixAssetMapping(organisationId, fixAssetMapping);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public BankDeposite UpdateBankDeposite(int organisationId, BankDeposite bankDeposite)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, bankDeposite);
        }

        public ActivityAssigneeGroup UpdateActivityAssigneeGroup(int organisationId, ActivityAssigneeGroup activityAssigneeGroup)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, activityAssigneeGroup);
        }

        public Activity UpdateActivity(int organisationId, Activity activity)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, activity);
        }

        public ActivityTask UpdateActivityTask(int organisationId, ActivityTask activityTask)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, activityTask);
        }

        public ActivityTaskState UpdateActivityTaskState(int organisationId, ActivityTaskState activityTaskState)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, activityTaskState);
        }

        public Assessment UpdateAssessment(int organisationId, Assessment assessment)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, assessment);
        }

        public bool AssignModuleExamSet(int organisationId, int personnelId, int assessmentId, List<CandidateAssessment> assessments)
        {
            try
            {
                foreach (var assessment in assessments)
                {
                    var assessmentData = RetrieveAssessment(organisationId, assessmentId, e => true);
                    assessment.AssessmentId = assessmentData.AssessmentId;
                    assessment.CreatedBy = personnelId;
                    CreateCandidateAssessment(organisationId, assessment);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CandidateAssessment UpdateCandidateAssessment(int organisationId, CandidateAssessment candidateAssessment)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateAssessment);
        }

        public bool CreateCandidateQuestionAnswer(int organisationId, int personnelId, int centreId, CandidateAssessmentQuestionAnswer candidateAssessment)
        {
            var candiateAssessmentQuestionAnswerData = RetrieveCandidateAssessmentQuestionAnswers(organisationId, e => e.CandidateAssessmentId == candidateAssessment.CandidateAssessmentId && e.ModuleExamSetId == candidateAssessment.ModuleExamSetId && e.ModuleExamQuestionSetId == candidateAssessment.ModuleExamQuestionSetId).Items.FirstOrDefault();
            try
            {
                candidateAssessment.PersonnelId = personnelId;
                candidateAssessment.OrganisationId = organisationId;
                candidateAssessment.CentreId = centreId;
                candidateAssessment.IsAttempted = true;
                if (candiateAssessmentQuestionAnswerData != null && candiateAssessmentQuestionAnswerData.IsAttempted)
                {
                    candiateAssessmentQuestionAnswerData.IsOptionA = candidateAssessment.IsOptionA;
                    candiateAssessmentQuestionAnswerData.IsOptionB = candidateAssessment.IsOptionB;
                    candiateAssessmentQuestionAnswerData.IsOptionC = candidateAssessment.IsOptionC;
                    candiateAssessmentQuestionAnswerData.IsOptionD = candidateAssessment.IsOptionD;
                    candiateAssessmentQuestionAnswerData.SubjectiveAnswer = candidateAssessment.SubjectiveAnswer;
                    UpdateCandidateAssessmentQuestionAnswer(organisationId, candiateAssessmentQuestionAnswerData);
                    return true;
                }
                CreateCandidateAssessmentQuestionAnswer(organisationId, candidateAssessment);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Partner UpdatePartner(int organisationId, Partner partner)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, partner);
        }

        public CandidateAssessmentQuestionAnswer UpdateCandidateAssessmentQuestionAnswer(int organisationId,CandidateAssessmentQuestionAnswer candidateAssessmentQuestionAnswer)
        {
            return _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateAssessmentQuestionAnswer);
        }


        public void AssignBatch(int organisationId, int centreId, int personnelId, Admission admission)
        {
            if (admission.BatchId != null)
            {
                var registrationData = RetrieveRegistration(organisationId, admission.RegistrationId);
                var candidateInstallment = RetrieveCandidateInstallment(organisationId, registrationData.CandidateInstallmentId, e => true);
                var batch = RetrieveBatch(organisationId, admission.BatchId ?? 0);
                var installmentDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 5, 0, 0, 0);
                var candidateFees = new List<CandidateFee>();
                for (int i = 1; i <= batch?.NumberOfInstallment; i++)
                {
                    candidateFees.Add(new CandidateFee
                    {
                        CandidateInstallmentId = candidateInstallment.CandidateInstallmentId,
                        PaymentModeId = registrationData.CandidateFee.PaymentModeId,
                        FeeTypeId = (int)FeeType.Installment,
                        FollowUpDate = batch?.BatchStartDate.AddMonths(batch.NumberOfInstallment),
                        FiscalYear = DateTime.UtcNow.FiscalYear(),
                        InstallmentAmount = (candidateInstallment.CourseFee - candidateInstallment.DownPayment) / batch?.NumberOfInstallment,
                        CentreId = centreId,
                        OrganisationId = organisationId,
                        PersonnelId = personnelId,
                        IsPaymentDone = false,
                        StudentCode = registrationData.StudentCode,
                        InstallmentNumber = i,
                        InstallmentDate = installmentDate.AddMonths(i)
                    });
                }
                candidateInstallment.NumberOfInstallment = batch?.NumberOfInstallment;
                _nidanDataService.UpdateOrganisationEntityEntry(organisationId, candidateInstallment);
                _nidanDataService.Create<CandidateFee>(organisationId, candidateFees);
                admission.Registration = null;
                UpdateAdmission(organisationId, centreId, personnelId, admission);
            }
        }

        public Admission UpdateAdmission(int organisationId, int centreId, int personnelId, Admission admission)
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
            //if (!batch.BatchTrainers.Any() && trainerIds.Any())
            //    CreateBatchTrainer(organisationId, batch.CentreId, batch.BatchId, trainerIds);
            UpdateBatchDay(organisationId, batchDays);

            var batchTrainers = RetrieveBatchTrainers(organisationId, batch.BatchId).ToList();
            var batchTrainerList = new List<BatchTrainer>();
            foreach (var item in trainerIds)
            {
                if (!batchTrainers.Any(e => e.TrainerId == item && e.BatchId == batch.BatchId))
                {
                    batchTrainerList.Add(new BatchTrainer()
                    {
                        OrganisationId = organisationId,
                        BatchId = batch.BatchId,
                        TrainerId = item,
                        CentreId = batch.CentreId
                    });
                }
            }
            if (batchTrainerList.Any())
                _nidanDataService.Create<BatchTrainer>(organisationId, batchTrainerList);
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

        public void DeleteOtherFee(int organisationId, int centreId, int otherFeeId)
        {
            _nidanDataService.Delete<OtherFee>(organisationId, p => p.CentreId == centreId && p.OtherFeeId == otherFeeId);
        }

        public void DeleteExpenseProject(int organisationId, int expenseId, int projectId)
        {
            _nidanDataService.Delete<ExpenseProject>(organisationId, p => p.ExpenseId == expenseId && p.ProjectId == projectId);
        }

        public void DeleteActivityAssignPersonnel(int organisationId, int centreId, int activityAssigneeGroupId, int personnelId)
        {
            _nidanDataService.Delete<ActivityAssignPersonnel>(organisationId, p => p.CentreId == centreId && p.ActivityAssigneeGroupId == activityAssigneeGroupId && p.PersonnelId == personnelId);
        }

        public void DeleteActivityTask(int organisationId, int activityTaskId)
        {
            _nidanDataService.Delete<ActivityTask>(organisationId, e => e.ActivityTaskId == activityTaskId);
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

        public PagedResult<DocumentType> RetrieveDocumentTypes(int organisationId, Expression<Func<DocumentType, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _nidanDataService.RetrieveDocumentTypes(organisationId, predicate, orderBy, paging);
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            return _nidanDataService.RetrieveDocument(organisationId, documentGuid);
        }

        public IEnumerable<StudentDocument> RetrieveAdmissionDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsAdmission);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        public IEnumerable<StudentDocument> RetrieveCounsellingDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsCounselling);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        public IEnumerable<StudentDocument> RetrieveTrainerDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsTrainer);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        public IEnumerable<StudentDocument> RetrieveExpenseDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsExpense);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        public IEnumerable<StudentDocument> RetrieveFixAssetDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsFixAsset);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        public IEnumerable<StudentDocument> RetrieveBankDepositeDocuments(int organisationId, int centreId, string studentCode)
        {
            var studentDocuments = RetrieveDocuments(organisationId, e => e.StudentCode == studentCode).Items.ToList();
            var documentTypes = RetrieveDocumentTypes(organisationId).Where(e => e.IsBankDepositeDocument);
            var studentDocumentTypeList = new List<StudentDocument>();
            foreach (var item in documentTypes)
            {
                var result = studentDocuments.FirstOrDefault(e => e.DocumentTypeId == item.DocumentTypeId);
                studentDocumentTypeList.Add(new StudentDocument()
                {
                    DocumentTypeId = item.DocumentTypeId,
                    Guid = result?.Guid,
                    StudentCode = studentCode,
                    IsPending = result == null,
                    Name = item.Name
                });
            }
            return studentDocumentTypeList;
        }

        #endregion

        //Template
        public byte[] CreateRegistrationRecieptBytes(int organisationId, int centreId, int id)
        {
            var candidateFeeData = _nidanDataService.RetrieveCandidateFee(organisationId, id, e => true);
            var totalInstallment = RetrieveCandidateInstallment(organisationId, candidateFeeData.CandidateInstallmentId ?? 0, e => true).NumberOfInstallment.ToString();
            var enquiry = RetrieveEnquiries(organisationId, e => e.StudentCode == candidateFeeData.StudentCode).FirstOrDefault();
            var centre = RetrieveCentre(organisationId, candidateFeeData.CentreId);
            var gstnumber = RetrieveGsts(organisationId, e => e.StateId == centre.StateId).Items.FirstOrDefault();
            int value = candidateFeeData.FeeTypeId;
            var rupeesinword = ConvertNumbertoWords((Int32)candidateFeeData.PaidAmount);
            FeeType feeType = (FeeType)value;
            var candidateFeeReceipt = new CandidateFeeReceipt()
            {
                OrganisationName = candidateFeeData.Organisation.Name,
                EmailId = enquiry?.EmailId,
                PaymentDate = candidateFeeData.PaymentDate.Value.ToShortDateString(),
                CandidateAddress =
                    string.Concat(enquiry.Address1, enquiry.Address2, enquiry.Address3, enquiry.Address4),
                CandidateName = enquiry.Title + " " + enquiry.FirstName + " " + enquiry.MiddleName + " " + enquiry.LastName,
                CentreName = candidateFeeData.Centre.Name,
                CentreAddress = string.Concat(centre.Address1, centre.Address2, centre.Address3, centre.Address4),
                CourseDuration = candidateFeeData.CandidateInstallment.CourseInstallment.Course.Duration.ToString(),
                CourseName = candidateFeeData.CandidateInstallment.CourseInstallment.Course.Name,
                FeeTypeName = feeType.ToString(),
                InvoiceNumber = candidateFeeData.ReceiptNumber,
                RecievedAmount = candidateFeeData.PaidAmount.ToString(),
                MobileNumber = enquiry.Mobile.ToString(),
                TotalCourseFee = candidateFeeData.CandidateInstallment.CourseFee.ToString(),
                TotalInstallment = totalInstallment,
                InstallmentNumber = candidateFeeData.InstallmentNumber.ToString(),
                State = candidateFeeData.Centre.State.Name,
                Gstin = gstnumber.GstNumber,
                GstStateCode = centre.State.GstStateCode.ToString(),
                FatherName = enquiry.MiddleName + " " + enquiry.LastName,
                PaymentMode = candidateFeeData.PaymentMode.Name,
                BankName = candidateFeeData.BankName != "null" ? candidateFeeData.BankName : "-",
                ChequeNumber = candidateFeeData.ChequeNumber != "null" ? candidateFeeData.ChequeNumber : "-",
                ChequeDate = candidateFeeData.ChequeDate?.ToShortDateString(),
                RupeesInWords = rupeesinword + "RUPEES ONLY"
            };
            if (value == 1)
            {
                return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(candidateFeeReceipt), "Registration");
            }
            else if (value == 3)
            {
                return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(candidateFeeReceipt), "Installment");
            }
            else
            {
                return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(candidateFeeReceipt), "Enrollment");
            }

        }

        public byte[] CreateEnrollmentBytes(int organisationId, int centreId, Admission admission, bool isCandidateAndCentre = true)
        {
            var modules = RetrieveSubjectCourses(organisationId, s => s.CourseId == admission.Registration.CourseId);
            var organisationName = RetrieveOrganisation(organisationId);
            var centre = RetrieveCentre(organisationId, admission.CentreId);
            var moduleList = new List<ModuleDetail>();
            var feeDetailList = new List<FeeDetail>();
            int number = 0;
            foreach (var item in modules)
            {
                number++;
                moduleList.Add(new ModuleDetail()
                {
                    Name = item.Subject.Name,
                    Number = number.ToString(),
                    AttemptAllowed = item.Subject.NoOfAttemptsAllowed.ToString(),
                    PassingMarks = item.Subject.PassingMarks.ToString(),
                    TotalMarks = item.Subject.TotalMarks.ToString()
                });
            }
            var candidateFeeData = RetrieveCandidateFees(organisationId, c => c.StudentCode == admission.Registration.StudentCode);
            var candidateFee = candidateFeeData.Items.ToList();
            //var rupeesinwords= ConvertNumbertoWords((Int32)candidateFeeData.PaidAmount);

            foreach (var item in candidateFee)
            {
                feeDetailList.Add(new FeeDetail()
                {
                    InstallmentDate = item.InstallmentDate?.ToString("dd/MM/yyyy"),
                    InstallmentAmount = item.InstallmentAmount.ToString(),
                    Paymentdate = item.IsPaymentDone ? item.PaymentDate?.ToString("dd/MM/yyyy") : String.Empty,
                    Status = item.IsPaymentDone ? "Paid" : "Pending",
                    Type = System.Enum.GetName(typeof(FeeType), item.FeeTypeId) == FeeType.Installment.ToString()
                          ? string.Format("{0}-{1}", System.Enum.GetName(typeof(FeeType), item.FeeTypeId), item.InstallmentNumber)
                          : System.Enum.GetName(typeof(FeeType), item.FeeTypeId),
                    AmountPaid = item.PaidAmount.ToString(),
                    PaymentMode = item.PaymentMode.Name,
                    BankName = item.BankName != null ? item.BankName : "-",
                    ChequeDate = item.ChequeDate != null ? item.ChequeDate.Value.ToShortDateString() : "-",
                    ChequeNumber = item.ChequeNumber != null ? item.ChequeNumber : ""
                });
            }
            var recievedAmount = candidateFee.Where(e => e.FeeTypeId == 2).Select(a => a.PaidAmount).FirstOrDefault();
            var gstnumber = RetrieveGsts(organisationId, e => e.StateId == centre.StateId).Items.FirstOrDefault();
            var enrollmentData = new CandidateEnrollment
            {
                EnrollmentDate = admission.AdmissionDate.ToShortDateString(),
                InvoiceNumber = admission.EnrollmentNumber,
                BatchEndDate = admission.Batch?.BatchStartDate.ToShortDateString() ?? " ",
                BatchStartDate = admission.Batch?.BatchEndDate.ToShortDateString() ?? " ",
                CandidateAddress =
                    string.Format("{0} {1} {2} {3}", admission.Registration.Enquiry.Address1,
                        admission.Registration.Enquiry.Address2, admission.Registration.Enquiry.Address3,
                        admission.Registration.Enquiry.Address4),
                CandidateName =
                    string.Format("{0} {1} {2}", admission.Registration.Enquiry.FirstName,
                        admission.Registration.Enquiry.MiddleName, admission.Registration.Enquiry.LastName),
                CentreAddress =
                    string.Format("{0} {1} {2} {3} {4}", centre.Address1, centre.Address2,
                        centre.Address3, centre.Address4, centre.PinCode),
                CentreName = centre.Name,
                CourseDuration = admission.Registration.CourseInstallment.Course.Duration.ToString(),
                CourseName = admission.Registration.CourseInstallment.Course.Name,
                EmailId = admission.Registration.Enquiry.EmailId,
                MobileNumber = admission.Registration.Enquiry.Mobile.ToString(),
                OrganisationName = organisationName.Name,
                Modules = moduleList,
                FeeDetails = feeDetailList,
                TotalCourseFee = admission.Registration.CandidateInstallment.CourseFee.ToString(),
                TotalAmountPaid = candidateFee.Sum(e => e.PaidAmount).ToString(),
                BalanceFee = admission.Registration.CandidateInstallment.PaymentMethod != "LumpsumAmount" ? (admission.Registration.CandidateInstallment.CourseFee - candidateFee.Sum(e => e.PaidAmount)).ToString()
                            : (admission.Registration.CandidateInstallment.LumpsumAmount - candidateFee.Sum(e => e.PaidAmount)).ToString(),
                State = centre.State.Name,
                Gstin = gstnumber?.GstNumber,
                GstStateCode = centre.State.GstStateCode.ToString(),
                RecievedAmount = recievedAmount.ToString(),
                FatherName = String.Format("{0} {1}", admission.Registration.Enquiry.MiddleName, admission.Registration.Enquiry.LastName)
            };

            var termsAndCondition = _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(string.Empty), "FeeTermsAndConditions");
            var enrollmentPdf = _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(enrollmentData), "Enrollment");
            var mergePdf1 = _templateService.MergePDF(enrollmentPdf, termsAndCondition);
            if (isCandidateAndCentre)
            {
                var studentPdfbytes = _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(enrollmentData), "CandidateFeeEnrollment");
                var mergePdf2 = _templateService.MergePDF(mergePdf1, studentPdfbytes);
                return mergePdf2;
            }
            return mergePdf1;
        }

        public byte[] CreateOtherFeeBytes(int organisationId, int centreId, Expense expense)
        {
            var expenseData = new ExpenseReceipt();
            expenseData.ExpenseHeader = expense.ExpenseHeader.Name;
            //otherFee.Project = expense.ExpenseProjects.Name,
            expenseData.DebitAmount = expense.DebitAmount;
            expenseData.Description = string.Format("{0} , ", expense.Particulars);
            expenseData.OrganisationName = expense.Organisation.Name;
            expenseData.CentreAddress = String.Format("{0} {1} {2} {3}.", expense.Centre.Address1, expense.Centre.Address2, expense.Centre.Address3, expense.Centre.Address4);
            expenseData.CentreName = expense.Centre.Name;
            expenseData.VoucherNumber = expense.VoucherNumber;
            expenseData.CashMemo = expense.CashMemoNumbers;
            expenseData.TotalDebitAmount = expense.DebitAmount;
            var rupeesInWords = ConvertNumbertoWords((Int32)expenseData.TotalDebitAmount);
            expenseData.PaidTo = expense.PaidTo;
            expenseData.RupeesInWords = rupeesInWords + " ONLY.";
            expenseData.VoucherCreatedDate = expense.CreatedDate.ToShortDateString();
            var otherFeeData = expenseData;
            return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(expenseData), "OtherFee");
        }

        public byte[] CreateExpenseBytes(int organisationId, int centreId, Expense expense)
        {
            var expenseReceipt = new ExpenseReceipt();
            var projectIds = expense.ExpenseProjects.Select(e => e.ProjectId);
            var projectName = "";
            foreach (var projectId in projectIds)
            {
                var projectData = RetrieveProject(organisationId, projectId, e => true);
                projectName = projectName == "" ? projectData.Name : projectName + " " + projectData.Name; ;
            }
            expenseReceipt.CentreName = expense.Centre.Name;
            expenseReceipt.VoucherCreatedDate = expense.CreatedDate.ToShortDateString();
            expenseReceipt.VoucherNumber = expense.VoucherNumber;
            expenseReceipt.CashMemo = expense.CashMemoNumbers;
            expenseReceipt.PaidTo = expense.PaidTo;
            expenseReceipt.Project = projectName;
            expenseReceipt.ExpenseHeader = expense.ExpenseHeader.Name;
            expenseReceipt.DebitAmount = expense.DebitAmount;
            expenseReceipt.TotalDebitAmount = expense.DebitAmount;
            expenseReceipt.RupeesInWords = expense.RupeesInWord;
            expenseReceipt.Particulars = expense.Particulars;
            var expenseData = expenseReceipt;
            return _templateService.CreatePDF(organisationId, JsonConvert.SerializeObject(expenseReceipt), "OtherFee");
        }

        //RupeesInWords
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
                    "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
                };
                var tensMap = new[]
                {
                    "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
                };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        //Email

        private void SendCandidateRegistrationEmail(int organisationId, int centreId, Registration registration)
        {
            var document = CreateRegistrationRecieptBytes(organisationId, centreId, registration.CandidateFeeId);
            var emailData = new EmailData()
            {
                BCCAddressList = new List<string> { "accounts@nidantech.com" },
                Body = String.Format("Dear {0}, Welcome to NEST.For your information and reference, you have registered for {1}.We have received your payment of Rs {2} towards the same and we thank you for it. Please find attached the receipt for the like amount. ", registration.Enquiry.FirstName, registration.Course.Name, registration.CandidateFee.PaidAmount),
                Subject = "Welcome To Nidan Education & Skill Training (NEST)",
                IsHtml = true,
                ToAddressList = new List<string> { registration.Enquiry.EmailId }
            };

            var registrationReciept = new Dictionary<string, byte[]>
            {
                //{registration.Enquiry.FirstName + " " +registration.Enquiry.LastName+" Registration Detail.pdf",document}
            };
            _emailService.SendEmail(emailData, registrationReciept);
        }

        private void SendCandidateEnrollmentEmail(int organisationId, int centreId, Admission admission)
        {
            var candidateFee = RetrieveCandidateFees(organisationId, e => e.StudentCode == admission.Registration.StudentCode && e.FeeTypeId == 2).Items.FirstOrDefault();
            var document = CreateEnrollmentBytes(organisationId, centreId, admission);
            var emailData = new EmailData()
            {
                BCCAddressList = new List<string> { "accounts@nidantech.com" },
                Body = String.Format("Dear {0}, We are in receipt of your payment of Rs.{1} towards your enrolment for the {2} and we thank you for the same. Please also find attached the receipt for the same.", admission.Registration.Enquiry.FirstName, candidateFee?.PaidAmount, admission.Registration.Course.Name),
                Subject = "Greetings From NEST",
                IsHtml = true,
                ToAddressList = new List<string> { admission.Registration.Enquiry.EmailId }
            };

            var enrollmentReciept = new Dictionary<string, byte[]>
            {
                //{admission.Registration.Enquiry.FirstName + " " +admission.Registration.Enquiry.LastName+" Enrollment Detail.pdf",document}
            };
            _emailService.SendEmail(emailData, enrollmentReciept);
        }

        private void SendCandidateInstallmentEmail(int organisationId, int centreId, CandidateFee candidateFee)
        {
            var enquiryData = RetrieveEnquiries(organisationId, e => e.StudentCode == candidateFee.StudentCode).FirstOrDefault();
            var candidateFeeList = RetrieveCandidateFees(organisationId, e => e.StudentCode == candidateFee.StudentCode);
            var document = CreateRegistrationRecieptBytes(organisationId, centreId, candidateFee.CandidateFeeId);
            if (enquiryData != null)
            {
                if (candidateFee.PaymentDate != null)
                {
                    var emailData = new EmailData()
                    {
                        BCCAddressList = new List<string> { "accounts@nidantech.com" },
                        Body = String.Format("Dear {0}, We are in receipt of your payment of Rs.{1}, towards your instalment number-{2} and for the month of {3}.Please find attached your receipt for the like amount. ", enquiryData.FirstName, candidateFee.PaidAmount, candidateFee.InstallmentNumber, candidateFee.PaymentDate.Value.ToString("MMMM")),
                        //"For your information, your next instalment date is {4} and the amount is {5}.Your pending balance is {6}.We trust you find the same in order."
                        Subject = "Greetings From NEST",
                        IsHtml = true,
                        ToAddressList = new List<string> { enquiryData.EmailId }
                    };

                    var installmentReciept = new Dictionary<string, byte[]>
                    {
                        //{enquiryData.FirstName + " " +enquiryData.LastName+" Installment Detail.pdf",document}
                    };
                    _emailService.SendEmail(emailData, installmentReciept);
                }
            }
        }

        //SMS

        private void SendRegistrationSms(Registration registration)
        {
            if (!string.IsNullOrEmpty(registration.Enquiry.Mobile.ToString()))
            {
                var smsData = new SmsData()
                {
                    To = registration.Enquiry.Mobile.ToString(),
                    MessageBody = string.Format("Dear {0}, Thank you for registring for {1}, We have received Rs.{2} as your registration fees, Kindly visit Branch for enrollment process.", registration.Enquiry.FirstName, registration.Course.Name, registration.CandidateFee.PaidAmount)
                };
                _smsService.SendSMS(smsData);
            }

        }

        private void SendAdmissionSms(Admission admission)
        {
            var candidateFee = RetrieveCandidateFees(admission.OrganisationId, c => c.StudentCode == admission.Registration.StudentCode).Items.ToList();
            var recievedAmount = candidateFee.Where(e => e.FeeTypeId == 2).Select(a => a.PaidAmount).FirstOrDefault();
            if (!string.IsNullOrEmpty(admission.Registration.Enquiry.Mobile.ToString()))
            {
                var smsData = new SmsData()
                {
                    To = admission.Registration.Enquiry.Mobile.ToString(),
                    MessageBody = string.Format("Dear {0}, your enrollment is confirmed for {1}, We have received Rs.{2}, Kindly visit Branch for details.", admission.Registration.Enquiry.FirstName, admission.Registration.Course.Name, recievedAmount)
                };
                _smsService.SendSMS(smsData);
            }

        }

        private void SendInstallmetnSms(CandidateFee candidateFee)
        {
            var enquiryData = RetrieveEnquiries(candidateFee.OrganisationId, e => e.StudentCode == candidateFee.StudentCode).FirstOrDefault();
            //var registration=RetrieveRegistrations(candidateFee.OrganisationId, e => e.StudentCode == candidateFee.StudentCode).Items.FirstOrDefault();
            if (enquiryData != null && !string.IsNullOrEmpty(enquiryData.Mobile.ToString()))
            {
                var smsData = new SmsData()
                {
                    To = enquiryData.Mobile.ToString(),
                    MessageBody = string.Format("Dear {0}, We have received your payment of Rs.{1} towards {2}, Installment-{3} for the month of {4}.", enquiryData.FirstName, candidateFee.PaidAmount, candidateFee.CandidateInstallment.CourseInstallment.Course.Name, candidateFee.InstallmentNumber, candidateFee.InstallmentDate.Value.ToString("MMMM yyyy"))
                };
                _smsService.SendSMS(smsData);
            }

        }

        public PagedResult<ModuleExamQuestionSetGrid> RetrieveModuleExamQuestionSetGrid(int organisationId,
            Expression<Func<ModuleExamQuestionSetGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _nidanDataService.RetrieveModuleExamQuestionSetGrid(organisationId, predicate, orderBy, paging);
        }

    }
}

