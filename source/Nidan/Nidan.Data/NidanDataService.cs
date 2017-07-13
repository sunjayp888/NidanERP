using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using HR.Entity;
using Nidan.Entity;
using Nidan.Data.Extensions;
using Nidan.Data.Interfaces;
using Nidan.Entity.Dto;
using System.Configuration;

namespace Nidan.Data
{
    public partial class NidanDataService : INidanDataService
    {
        private INidanDatabaseFactory _databaseFactory;
        private TransactionScope ReadUncommitedTransactionScope => new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });

        public NidanDataService(INidanDatabaseFactory factory)
        {
            _databaseFactory = factory;
        }

        #region // Create

        public AbsenceType CreateAbsenceType(int organisationId, AbsenceType absenceType)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                absenceType = context.AbsenceTypes.Add(absenceType);
                context.SaveChanges();

                return absenceType;
            }
        }

        public Trainer CreateTrainer(int organisationId, Trainer trainer)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                trainer = context.Trainers.Add(trainer);
                context.SaveChanges();

                return trainer;
            }
        }

        public AreaOfInterest CreateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                areaOfInterest = context.AreaOfInterests.Add(areaOfInterest);
                context.SaveChanges();

                return areaOfInterest;
            }
        }

        public Centre CreateCentre(int organisationId, Centre centre)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                centre = context.Centres.Add(centre);
                context.SaveChanges();

                return centre;
            }
        }

        public Batch CreateBatch(int organisationId, Batch batch)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                batch = context.Batches.Add(batch);
                context.SaveChanges();
                return batch;
            }
        }


        public Question CreateQuestion(int organisationId, Question question)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                question = context.Questions.Add(question);
                context.SaveChanges();

                return question;
            }
        }

        public Event CreateEvent(int organisationId, Event eventplan)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                eventplan = context.Events.Add(eventplan);
                context.SaveChanges();

                return eventplan;
            }
        }

        public Brainstorming CreateBrainstorming(int organisationId, Brainstorming brainstorming)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                brainstorming = context.Brainstormings.Add(brainstorming);
                context.SaveChanges();

                return brainstorming;
            }
        }

        public Planning CreatePlanning(int organisationId, Planning planning)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                planning = context.Plannings.Add(planning);
                context.SaveChanges();

                return planning;
            }
        }

        public Budget CreateBudget(int organisationId, Budget budget)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                budget = context.Budgets.Add(budget);
                context.SaveChanges();

                return budget;
            }
        }

        public Eventday CreateEventday(int organisationId, Eventday eventday)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                eventday = context.Eventdays.Add(eventday);
                context.SaveChanges();

                return eventday;
            }
        }

        public Postevent CreatePostevent(int organisationId, Postevent postevent)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                postevent = context.Postevents.Add(postevent);
                context.SaveChanges();

                return postevent;
            }
        }
        //public RegistrationPaymentReceipt CreateRegistrationPaymentReceipt(int organisationId,
        //    RegistrationPaymentReceipt registrationPaymentReceipt)
        //{
        //    using (var context = _databaseFactory.Create(organisationId))
        //    {
        //        registrationPaymentReceipt.Enquiry = null;
        //        registrationPaymentReceipt = context.RegistrationPaymentReceipts.Add(registrationPaymentReceipt);
        //        context.SaveChanges();

        //        return registrationPaymentReceipt;
        //    }
        //}

        public Course CreateCourse(int organisationId, Course course)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                course = context.Courses.Add(course);
                context.SaveChanges();

                return course;
            }
        }

        public CourseInstallment CreateCourseInstallment(int organisationId, CourseInstallment courseInstallment)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                courseInstallment = context.CourseInstallments.Add(courseInstallment);
                context.SaveChanges();

                return courseInstallment;
            }
        }

        public FollowUp CreateFollowUp(int organisationId, FollowUp followUp)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                followUp = context.FollowUps.Add(followUp);
                context.SaveChanges();

                return followUp;
            }
        }

        public Subject CreateSubject(int organisationId, Subject subject)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                subject = context.Subjects.Add(subject);
                context.SaveChanges();

                return subject;
            }
        }

        public Session CreateSession(int organisationId, Session session)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                session = context.Sessions.Add(session);
                context.SaveChanges();

                return session;
            }
        }

        public Room CreateRoom(int organisationId, Room room)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                room = context.Rooms.Add(room);
                context.SaveChanges();

                return room;
            }
        }

        public BatchDay CreateBatchDay(int organisationId, BatchDay batchDay)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                batchDay = context.BatchDays.Add(batchDay);
                context.SaveChanges();

                return batchDay;
            }
        }

        public Admission CreateAdmission(int organisationId, Admission admission)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                admission.Registration = null;
                admission = context.Admissions.Add(admission);
                context.SaveChanges();

                return admission;
            }
        }

        public Counselling CreateCounselling(int organisationId, Counselling counselling)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                counselling.Enquiry = null;
                counselling = context.Counsellings.Add(counselling);
                context.SaveChanges();

                return counselling;
            }
        }

        public CandidateFee CreateCandidateFee(int organisationId, CandidateFee candidateFee)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                //admission.RegistrationPaymentReceipt = null;
                candidateFee = context.CandidateFees.Add(candidateFee);
                context.SaveChanges();

                return candidateFee;
            }
        }

        public Module CreateModule(int organisationId, Module module)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                //admission.RegistrationPaymentReceipt = null;
                module = context.Modules.Add(module);
                context.SaveChanges();

                return module;
            }
        }

        public Attendance CreateAttendance(int organisationId, Attendance attendance)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                attendance = context.Attendances.Add(attendance);
                context.SaveChanges();
                return attendance;
            }
        }

        public BatchAttendance CreateBatchAttendance(int organisationId, BatchAttendance batchAttendance)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                batchAttendance = context.BatchAttendances.Add(batchAttendance);
                context.SaveChanges();
                return batchAttendance;
            }
        }


        public Enquiry CreateEnquiry(int organisationId, Enquiry enquiry)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                enquiry = context.Enquiries.Add(enquiry);
                context.SaveChanges();

                return enquiry;
            }
        }

        public Mobilization CreateMobilization(int organisationId, Mobilization mobilization)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                mobilization = context.Mobilizations.Add(mobilization);
                context.SaveChanges();

                return mobilization;
            }
        }


        public Personnel CreatePersonnel(int organisationId, Personnel personnel)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                personnel = context.Personnels.Add(personnel);
                context.SaveChanges();

                return personnel;
            }

        }

        public T Create<T>(int organisationId, T t) where T : class
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                t = context.Set<T>().Add(t);
                context.SaveChanges();
                return t;
            }
        }

        public void Create<T>(int organisationId, IEnumerable<T> t) where T : class
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                context.Set<T>().AddRange(t);
                context.SaveChanges();
            }
        }
        #endregion

        #region // Retrieve

        public Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Events
                    //.Include(c => c.)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.EventId == eventId);
            }
        }

        public PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {

            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Events
                    //.Include(c => c.EventBudgets)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy> {
                        new OrderBy { Property = "Name", Direction = System.ComponentModel.ListSortDirection.Ascending }
                    })
                    .Paginate(paging);

            }
        }

        public AbsenceType RetrieveAbsenceType(int organisationId, int absenceTypeId, Expression<Func<AbsenceType, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .AbsenceTypes
                    .Include(c => c.Colour)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.AbsenceTypeId == absenceTypeId);
            }
        }

        public PagedResult<AbsenceType> RetrieveAbsenceTypes(int organisationId, Expression<Func<AbsenceType, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .AbsenceTypes
                    .AsNoTracking()
                    .Include(c => c.Colour)
                    //.Include(a => a.CountryAbsenceTypes)
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy> {
                        new OrderBy { Property = "Name", Direction = System.ComponentModel.ListSortDirection.Ascending }
                    })
                    .Paginate(paging);
            }
        }

        public IEnumerable<Colour> RetrieveColours(Expression<Func<Colour, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context.Colours
                    .AsNoTracking()
                    .Where(predicate)
                    .ToList();
            }
        }

        public AreaOfInterest RetrieveAreaOfInterest(int organisationId, int areaOfInterestId, Expression<Func<AreaOfInterest, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .AreaOfInterests
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.AreaOfInterestId == areaOfInterestId);
            }
        }

        public PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, Expression<Func<AreaOfInterest, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .AreaOfInterests
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public bool PersonnelEmploymentHasAbsences(int organisationId, int personnelId, int employmentId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return true;
            }
        }

        public IEnumerable<Host> RetrieveHosts()
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Hosts
                    .Include(o => o.Organisation)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public IEnumerable<Organisation> RetrieveOrganisations()
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Organisations
                    .Include(o => o.Hosts)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Personnel RetrievePersonnel(int organisationId, int personnelId, Expression<Func<Personnel, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Personnels
                    .Include(o => o.Admissions)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.PersonnelId == personnelId);

            }
        }

        public IEnumerable<Personnel> RetrievePersonnel(int organisationId, IEnumerable<int> companyIds, IEnumerable<int> departmentIds, IEnumerable<int> divisionIds)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var personnel = context
                    .Personnels
                    //.Include(p => p.Employments.Select(e => e.Division))
                    .AsNoTracking();

                //if (companyIds != null && companyIds.Any())
                //    personnel = personnel.Where(p => companyIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().Division.CompanyId));

                //if (departmentIds != null && departmentIds.Any())
                //    personnel = personnel.Where(p => departmentIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DepartmentId));

                //if (divisionIds != null && divisionIds.Any())
                //    personnel = personnel.Where(p => divisionIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DivisionId));

                return personnel.ToList();
            }
        }

        public PagedResult<Personnel> RetrievePersonnel(int organisationId, Expression<Func<Personnel, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Personnels
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Forenames",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Questions
                    .Include(p => p.Organisation)
                    .Include(p => p.EventFunctionType)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Description",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Question RetrieveQuestion(int organisationId, int questionId, Expression<Func<Question, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Questions
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.QuestionId == questionId);
            }
        }

        public UserAuthorisationFilter RetrieveUserAuthorisation(string aspNetUserId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .UserAuthorisationFilters
                    .AsNoTracking()
                    .FirstOrDefault(u => u.AspNetUsersId == aspNetUserId);
            }
        }

        public PagedResult<Mobilization> RetrieveMobilizations(int organisationId, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Mobilizations
                    .Include(p => p.Organisation)
                    .Include(p => p.Course)
                    .Include(p => p.Qualification)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                return context.Database
                    .SqlQuery<PersonnelSearchField>("SearchPersonnel @SearchKeyword", category).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Forenames",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public List<T> Retrieve<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var returnItems = context.Set<T>().Where(predicate).ToList();
                return returnItems;
            }
        }

        public PagedResult<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Enquiries
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .Include(p => p.EnquiryCourses)
                    .Include(p => p.Qualification)
                    .Include(p => p.Religion)
                    .Include(p => p.CasteCategory)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ConversionProspect",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Enquiry RetrieveEnquiry(int organisationId, int enquiryId, Expression<Func<Enquiry, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var data = context
                    .Enquiries
                    .Include(e => e.Counsellings)
                    .Include(e => e.EnquiryCourses)
                    .Include(e => e.Sector)
                    .Include(e => e.Scheme)
                    //  .Include(e => e.EnquiryCourses.Select(c => c.Course))
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.EnquiryId == enquiryId);

                return data;

            }
        }

        public Mobilization RetrieveMobilization(int organisationId, int mobilizationId, Expression<Func<Mobilization, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Mobilizations
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.MobilizationId == mobilizationId);

            }
        }

        public PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .FollowUps
                    .Include(p => p.Organisation)
                    .Include(p => p.Enquiry.Course)
                    .Include(p => p.Registration.Course)
                    .Include(p => p.Course)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpDateTime",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public FollowUp RetrieveFollowUp(int organisationId, int followUpId, Expression<Func<FollowUp, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .FollowUps
                    .Include(f => f.Mobilization)
                    .Include(f => f.Enquiry)
                    .Include(f => f.Enquiry.EnquiryCourses)
                    .Include(f => f.Enquiry.Counsellings)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.FollowUpId == followUpId);

            }
        }

        public PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<MobilizationSearchField>("SearchMobilization @SearchKeyword", category).ToList();

                var mobilizations = context.Mobilizations.Include(e => e.Course).Include(e => e.Qualification).Include(e => e.MobilizationType).Include(e => e.Centre);

                var data = searchData.Join(mobilizations, e => e.MobilizationId, m => m.MobilizationId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData =
                    context.Database.SqlQuery<EnquirySearchField>("SearchEnquiry @SearchKeyword", category).ToList();

                var enquiries = context.EnquirySearchFields;
                //.Include(e => e.Sector).Include(e => e.Scheme).Include(e => e.Religion)
                //.Include(e => e.State).Include(e => e.District).Include(e => e.CasteCategory).Include(e => e.HowDidYouKnowAbout)
                //.Include(e => e.Qualification).Include(e => e.Taluka);

                var data = searchData.Join(enquiries, e => e.EnquiryId, m => m.EnquiryId, (e, m) => m).ToList().AsQueryable().

                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "EnquiryDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }


        public Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Centres
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CentreId == centreId);

            }
        }

        public Course RetrieveCourse(int organisationId, int courseId, Expression<Func<Course, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Courses
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CourseId == courseId);

            }
        }

        public PagedResult<Course> RetrieveCourseBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Course, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<CourseSearchField>("SearchCourse @SearchKeyword", category).ToList();

                var courses = context.Courses.Include(e => e.Scheme).Include(e => e.Sector);

                var data = searchData.Join(courses, e => e.CourseId, m => m.CourseId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CourseId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Courses
                    .Include(p => p.Organisation)
                    .Include(p => p.Scheme)
                    .Include(p => p.Sector)
                    .Include(p => p.CentreCourses)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Counselling> RetrieveCounsellings(int organisationId, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Counsellings
                    .Include(p => p.Organisation)
                    .Include(p => p.Enquiry)
                    .Include(p => p.Course)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ConversionProspect",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Counselling RetrieveCounselling(int organisationId, int counsellingId, Expression<Func<Counselling, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Counsellings
                    .Include(e => e.Enquiry)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CounsellingId == counsellingId);

            }
        }

        public PagedResult<Counselling> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<CounsellingSearchField>("SearchCounselling @SearchKeyword", category).ToList();

                var counsellings = context.Counsellings.Include(e => e.Course);

                var data = searchData.Join(counsellings, e => e.CounsellingId, m => m.CounsellingId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }


        public Brainstorming RetrieveBrainstorming(int organisationId, int brainstormingId, Expression<Func<Brainstorming, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Brainstormings
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.BrainstormingId == brainstormingId);

            }
        }

        public PagedResult<Brainstorming> RetrieveBrainstormings(int organisationId, Expression<Func<Brainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Brainstormings
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "BrainstormingId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Planning RetrievePlanning(int organisationId, int planningId, Expression<Func<Planning, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Plannings
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.PlanningId == planningId);

            }
        }

        public PagedResult<Planning> RetrievePlannings(int organisationId, Expression<Func<Planning, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Plannings
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "PlanningId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Budget RetrieveBudget(int organisationId, int budgetId, Expression<Func<Budget, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Budgets
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.BudgetId == budgetId);

            }
        }

        public PagedResult<Budget> RetrieveBudgets(int organisationId, Expression<Func<Budget, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Budgets
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "BudgetId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Eventday RetrieveEventday(int organisationId, int eventdayId, Expression<Func<Eventday, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Eventdays
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.EventdayId == eventdayId);

            }
        }

        // public PagedResult<Eventday> RetrieveEventdays(int organisationId, Expression<Func<Eventday, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)

        public PagedResult<Registration> RetrieveRegistrations(int organisationId, Expression<Func<Registration, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                  .Registrations
                    .Include(p => p.Organisation)
                    .Include(p => p.Enquiry)
                    .Include(p => p.CandidateFee)
                    .Include(p => p.CandidateFee.PaymentMode)
                    .Include(p => p.CourseInstallment)
                    .Include(p => p.CandidateInstallment)
                    .Include(p => p.CandidateInstallment.CandidateFees)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "RegistrationDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }


        public Postevent RetrievePostevent(int organisationId, int posteventId, Expression<Func<Postevent, bool>> predicate)
        {
            return null;
        }

        public PagedResult<CourseInstallment> RetrieveCourseInstallments(int organisationId, Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CourseInstallments
                    .Include(p => p.Organisation)
                    .Include(p => p.Course)
                    .Include(p => p.CentreCourseInstallments)
                    .Include(p => p.Course.CentreCourses)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CourseId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }


        public CourseInstallment RetrieveCourseInstallment(int organisationId, int courseInstallmentId, Expression<Func<CourseInstallment, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CourseInstallments
                    .Include(c => c.Course)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CourseInstallmentId == courseInstallmentId);

            }
        }

        public PagedResult<CourseInstallment> RetrieveCourseInstallmentBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CourseInstallment, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<CourseInstallmentSearchField>("SearchCourseInstallment @SearchKeyword", category).ToList();

                var courseInstallments = context.CourseInstallments.Include(e => e.Course);

                var data = searchData.Join(courseInstallments, e => e.CourseInstallmentId, m => m.CourseInstallmentId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<Subject> RetrieveSubjects(int organisationId, Expression<Func<Subject, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Subjects
                    .Include(p => p.Organisation)
                    .Include(p => p.Course)
                    .Include(p => p.CourseType)
                    .Include(p => p.SubjectCourses)
                    .Include(p => p.SubjectTrainers)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Subject RetrieveSubject(int organisationId, int subjectId, Expression<Func<Subject, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Subjects
                    .Include(p => p.SubjectCourses)
                    .Include(p => p.SubjectTrainers)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.SubjectId == subjectId);

            }
        }

        public Batch RetrieveBatch(int organisationId, int batchId, Expression<Func<Batch, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Batches
                    .AsNoTracking()
                    .Include(p => p.BatchTrainers)
                    .Include(p => p.Room)
                    .Include(p => p.CourseInstallment)
                    .Include(p => p.Course)
                    .Include(p => p.BatchDays)
                    .Where(predicate)
                    .SingleOrDefault(p => p.BatchId == batchId);

            }
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Centres
                    .Include(p => p.Organisation)
                    .Include(p => p.Enquiries)
                    .Include(p => p.Mobilizations)
                    .Include(p => p.Registrations)
                    .Include(p => p.Admissions)
                    .Include(p => p.Counsellings)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Batches
                    .Include(p => p.Organisation)
                    .Include(p => p.Course)
                    .Include(p => p.Centre)
                    .Include(p => p.BatchTrainers)
                    .Include(p => p.Room)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "BatchId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Template RetrieveTemplateDetails(int organisationId, string name)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var template = context
                    .Templates
                    .AsNoTracking()
                    .SingleOrDefault(p => p.Name.ToLower() == name.ToLower());

                if (template != null)
                {
                    return new Template
                    {
                        Name = template.Name,
                        FileName = template.FileName,
                        Type = template.Type,
                        FilePath = Path.Combine(ConfigurationManager.AppSettings["TemplateRootFilePath"], template.FileName)
                    };
                }
                return null;

            }
        }

        public PagedResult<Trainer> RetrieveTrainers(int organisationId, Expression<Func<Trainer, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Trainers
                    .Include(p => p.Organisation)
                    .Include(p => p.Sector)
                    .Include(p => p.Course)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Trainer RetrieveTrainer(int organisationId, int trainerId, Expression<Func<Trainer, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Trainers
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.TrainerId == trainerId);

            }
        }

        public PagedResult<Trainer> RetrieveTrainerBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Trainer, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<TrainerSearchField>("SearchTrainer @SearchKeyword", category).ToList();

                var trainers = context.Trainers.Include(e => e.Sector);

                var data = searchData.Join(trainers, e => e.TrainerId, m => m.TrainerId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<Holiday> RetrieveHolidays(int organisationId, Expression<Func<Holiday, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Holidays
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "HolidayId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Holiday RetrieveHoliday(int organisationId, int holidayId, Expression<Func<Holiday, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Holidays
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.HolidayId == holidayId);

            }
        }

        public PagedResult<Session> RetrieveSessions(int organisationId, Expression<Func<Session, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Sessions
                    .Include(p => p.Organisation)
                    .Include(p => p.CourseType)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "SessionId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Session RetrieveSession(int organisationId, int sessionId, Expression<Func<Session, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Sessions
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.SessionId == sessionId);

            }
        }

        public PagedResult<Room> RetrieveRooms(int organisationId, Expression<Func<Room, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Rooms
                    .Include(p => p.Organisation)
                    .Include(p => p.RoomType)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "RoomId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Room RetrieveRoom(int organisationId, int roomId, Expression<Func<Room, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Rooms
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.RoomId == roomId);

            }
        }

        public BatchDay RetrieveBatchDay(int organisationId, int batchId, Expression<Func<BatchDay, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .BatchDays
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.BatchId == batchId);

            }
        }

        public PagedResult<BatchDay> RetrieveBatchDays(int organisationId, Expression<Func<BatchDay, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .BatchDays
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "BatchDayId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public IEnumerable<EnquiryCourse> RetrieveEnquiryCourses(int organisationId, int centreId, int enquiryId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .EnquiryCourses
                    .Where(a => a.EnquiryId == enquiryId && a.CentreId == centreId)
                    .Include(e => e.Enquiry)
                    .Include(e => e.Course)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public IEnumerable<SubjectCourse> RetrieveSubjectCourses(int organisationId, Expression<Func<SubjectCourse, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .SubjectCourses
                    .Include(e => e.Subject)
                    .Include(e => e.Course)
                    .Where(predicate)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public IEnumerable<SubjectTrainer> RetrieveSubjectTrainers(int organisationId, int subjectId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .SubjectTrainers
                    .Where(a => a.SubjectId == subjectId)
                    .Include(e => e.Subject)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public PagedResult<CentreCourse> RetrieveCentreCourses(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CentreCourses
                    .Include(p => p.Centre)
                    .Include(p => p.Course)
                    .AsNoTracking()
                    .Where(c => c.CentreId == centreId)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CentreCourseId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public IEnumerable<BatchTrainer> RetrieveBatchTrainers(int organisationId, int batchId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
               .BatchTrainers
               .Where(a => a.BatchId == batchId)
               .Include(e => e.Batch)
               .AsNoTracking()
               .ToList();
            }
        }
        public PagedResult<CentreCourseInstallment> RetrieveCentreCourseInstallments(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CentreCourseInstallments
                    .Include(p => p.Centre)
                    .Include(p => p.CourseInstallment)
                    .AsNoTracking()
                    .Where(c => c.CentreId == centreId)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CentreCourseInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<CentreScheme> RetrieveCentreSchemes(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CentreSchemes
                    .Include(p => p.Centre)
                    .Include(p => p.Scheme)
                    .AsNoTracking()
                    .Where(c => c.CentreId == centreId)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CentreSchemeId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<CentreSector> RetrieveCentreSectors(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CentreSectors
                    .Include(p => p.Centre)
                    .Include(p => p.Sector)
                    .AsNoTracking()
                    .Where(c => c.CentreId == centreId)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CentreSectorId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Scheme> RetrieveSchemes(int organisationId, Expression<Func<Scheme, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Schemes
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Sector> RetrieveSectors(int organisationId, Expression<Func<Sector, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Sectors
                    .Include(p => p.Organisation)
                     .Include(p => p.Scheme)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Admission> RetrieveAdmissions(int organisationId, Expression<Func<Admission, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Admissions
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .Include(p => p.Registration)
                    .Include(p => p.Batch)
                    .Include(p => p.Registration.Enquiry)
                    .Include(p => p.Registration.CandidateFee)
                    .Include(p => p.Registration.CandidateInstallment)
                    .Include(p => p.Registration.CandidateInstallment.CandidateFees)
                    .Include(p => p.Registration.Course)
                    .Include(p => p.Registration.CourseInstallment)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "AdmissionId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Admission RetrieveAdmission(int organisationId, int admissionId, Expression<Func<Admission, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Admissions
                    .Include(p => p.Registration)
                    .Include(p => p.Registration.Course)
                    .Include(p => p.Registration.CandidateFee)
                    .Include(p => p.Registration.CandidateInstallment)
                    .Include(p => p.Registration.CandidateInstallment.CandidateFees)
                    .Include(p => p.Registration.CourseInstallment)
                    .Include(p => p.Registration.CourseInstallment.Course)
                    .Include(p => p.Registration.Enquiry)
                    .Include(p => p.Batch)
                    .Include(p => p.Batch.BatchTrainers)
                    .Include(p => p.Batch.BatchTrainers.Select(e => e.Trainer))
                    .Include(p => p.Batch.BatchDays)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.AdmissionId == admissionId);

            }
        }


        public PagedResult<CandidateFee> RetrieveCandidateFees(int organisationId, Expression<Func<CandidateFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                      .CandidateFees
                      .Include(p => p.Organisation)
                      .Include(p => p.CandidateInstallment)
                      .Include(p => p.Registrations)
                      .Include(p => p.PaymentMode)
                      .Include(p => p.Registrations.Select(e => e.Enquiry))
                      .Include(p => p.Centre)
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "CandidateFeeId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                        }
                      ).Paginate(paging);
            }
        }

        public PagedResult<CandidateFeeSearchField> RetrieveCandidateFeeBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CandidateFeeSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<CandidateFeeSearchField>("SearchCandidateFee @SearchKeyword", category)
                    .ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CandidateInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
                return searchData;
            }
        }

        public IEnumerable<CentreCourse> RetrieveCentreCourses(int organisationId, int centreId, Expression<Func<CentreCourse, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .CentreCourses
                      .Include(c => c.Organisation)
                      .Include(c => c.Centre)
                      .Include(c => c.Course)
                      .Where(predicate)
                      .AsNoTracking().ToList();
            }
        }

        public IEnumerable<CentreScheme> RetrieveCentreSchemes(int organisationId, int centreId, Expression<Func<CentreScheme, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CentreSchemes
                    .Include(c => c.Organisation)
                    .Include(c => c.Centre)
                    .Include(c => c.Scheme)
                    .Where(predicate)
                    .AsNoTracking().ToList();
            }
        }

        public IEnumerable<CentreSector> RetrieveCentreSectors(int organisationId, int centreId, Expression<Func<CentreSector, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CentreSectors
                    .Include(c => c.Organisation)
                    .Include(c => c.Centre)
                    .Include(c => c.Sector)
                    .Where(predicate)
                    .AsNoTracking().ToList();
            }
        }

        public IEnumerable<RoomAvailable> RetrieveRoomAvailables(int organisationId, int centreId, Expression<Func<RoomAvailable, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .RoomAvailables
                    .Include(c => c.Organisation)
                    .Include(c => c.Centre)
                    .Include(c => c.Room)
                    .Where(predicate)
                    .AsNoTracking().ToList();
            }
        }

        public IEnumerable<TrainerAvailable> RetrieveTrainerAvailables(int organisationId, int centreId, Expression<Func<TrainerAvailable, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .TrainerAvailables
                    .Include(c => c.Organisation)
                    .Include(c => c.Centre)
                    .Include(c => c.Trainer)
                    .Where(predicate)
                    .AsNoTracking().ToList();
            }
        }

        public PagedResult<CandidateInstallment> RetrieveCandidateInstallments(int organisationId, Expression<Func<CandidateInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .CandidateInstallments
                      .Include(p => p.Organisation)
                      .Include(p => p.CourseInstallment)

                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "CandidateInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public CandidateInstallment RetrieveCandidateInstallment(int organisationId, int candidateInstallmentId, Expression<Func<CandidateInstallment, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CandidateInstallments
                    .Include(p => p.CourseInstallment)
                    .Include(p => p.Registrations)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CandidateInstallmentId == candidateInstallmentId);
            }
        }

        public CandidateFee RetrieveCandidateFee(int organisationId, int candidateFeeId, Expression<Func<CandidateFee, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CandidateFees
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .Include(p => p.CandidateInstallment)
                    .Include(p => p.CandidateInstallment.CourseInstallment)
                    .Include(p => p.CandidateInstallment.CourseInstallment.Course)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CandidateFeeId == candidateFeeId);
            }
        }

        public Registration RetrieveRegistration(int organisationId, int registrationId, Expression<Func<Registration, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Registrations
                    .Include(p => p.Enquiry)
                    .Include(p => p.Course)
                    .Include(p => p.Enquiry.EnquiryCourses)
                    .Include(p => p.CourseInstallment)
                    .Include(p => p.CourseInstallment.Course)
                    .Include(p => p.CandidateFee)
                    .Include(p => p.CandidateFee.PaymentMode)
                    .Include(p => p.CandidateInstallment)
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.RegistrationId == registrationId);
            }
        }

        public Registration RetrieveRegistration(int organisationId, int centreId, int registrationId, Expression<Func<Registration, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Registrations
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.RegistrationId == registrationId);
            }
        }

        public PagedResult<FollowUpHistory> RetrieveFollowUpHistories(int organisationId, Expression<Func<FollowUpHistory, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .FollowUpHistories
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpHistoryId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public FollowUpHistory RetrieveFollowUpHistory(int organisationId, int followUpHistoryId, Expression<Func<FollowUpHistory, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .FollowUpHistories
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.FollowUpHistoryId == followUpHistoryId);
            }
        }

        public PagedResult<FollowUp> RetrieveFollowUpBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<FollowUp, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<FollowUpSearchField>("SearchFollowUp @SearchKeyword", category).ToList();

                var followUps = context.FollowUps.Include(e => e.Course);

                var data = searchData.Join(followUps, e => e.FollowUpId, m => m.FollowUpId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<Registration> RetrieveRegistrationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Registration, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<RegistrationSearchField>("SearchRegistration @SearchKeyword", category).ToList();

                var registrations = context.Registrations.Include(e => e.Course).Include(e => e.Enquiry).Include(e => e.CandidateFee).Include(e=>e.CandidateFee.PaymentMode);

                var data = searchData.Join(registrations, e => e.RegistrationId, m => m.RegistrationId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "RegistrationDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<AdmissionSearchField> RetrieveAdmissionBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<AdmissionSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<AdmissionSearchField>("SearchAdmission @SearchKeyword", category).ToList();

                var admissions = context.AdmissionSearchFields;

                var data = searchData.Join(admissions, e => e.AdmissionId, m => m.AdmissionId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "AdmissionDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<Module> RetrieveModules(int organisationId, Expression<Func<Module, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Modules
                    .Include(p => p.Organisation)
                    .Include(p => p.Course)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ModuleId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Module RetrieveModule(int organisationId, int moduleId, Expression<Func<Module, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Modules
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.ModuleId == moduleId);
            }
        }

        public PagedResult<CandidateInstallmentSearchField> RetrieveCandidateInstallmentBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CandidateInstallmentSearchField, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<CandidateInstallmentSearchField>("SearchCandidateInstallment @SearchKeyword", category)
                    .ToList()
                    .AsQueryable().

                //var candidateFeeSearchFields = context.CandidateFeeSearchFields;

                //var data = searchData.Join(candidateFeeSearchFields, e => e.CandidateInstallmentId, m => m.CandidateInstallmentId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CandidateInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
                return searchData;
            }
        }

        public PagedResult<AdmissionGrid> RetrieveAdmissionGrid(int organisationId, Expression<Func<AdmissionGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .AdmissionGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "AdmissionDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<CandidateInstallmentGrid> RetrieveCandidateInstallmentGrid(int organisationId, Expression<Func<CandidateInstallmentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .CandidateInstallmentGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "CandidateInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<ExpenseHeader> RetrieveExpenseHeaders(int organisationId, Expression<Func<ExpenseHeader, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .ExpenseHeaders
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ExpenseHeaderId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public ExpenseHeader RetrieveExpenseHeader(int organisationId, int expenseHeaderId, Expression<Func<ExpenseHeader, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .ExpenseHeaders
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.ExpenseHeaderId == expenseHeaderId);
            }
        }

        public PagedResult<OtherFee> RetrieveOtherFees(int organisationId, int centreId, Expression<Func<OtherFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .OtherFees
                    .Include(p => p.Organisation)
                    .Include(p => p.Project)
                    .Include(p => p.Centre)
                    .Include(p => p.ExpenseHeader)
                    .Include(p => p.Voucher)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public OtherFee RetrieveOtherFee(int organisationId, int centreId, int otherFeeId, Expression<Func<OtherFee, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .OtherFees
                    .Include(p => p.Project)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.OtherFeeId == otherFeeId);
            }
        }
        
        public PagedResult<CandidateFeeGrid> RetrieveCandidateFeeGrid(int organisationId, Expression<Func<CandidateFeeGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .CandidateFeeGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "CandidateInstallmentId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId, Expression<Func<MobilizationDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .MobilizationDataGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "MobilizationId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<EnquiryDataGrid> RetrieveEnquiryDataGrid(int organisationId, Expression<Func<EnquiryDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .EnquiryDataGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "EnquiryId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<FollowUpDataGrid> RetrieveFollowUpDataGrid(int organisationId, Expression<Func<FollowUpDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .FollowUpDataGrids
                      .Include(p=>p.FollowUp)
                      .Include(p=>p.FollowUp.Course)
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "FollowUpId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<Voucher> RetrieveVouchers(int organisationId, int centreId, Expression<Func<Voucher, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Vouchers
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "VoucherId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Voucher RetrieveVoucher(int organisationId, int centreId, int voucherId, Expression<Func<Voucher, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Vouchers
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.VoucherId == voucherId);
            }
        }

        public PagedResult<VoucherGrid> RetrieveVoucherGrids(int organisationId, int centreId, Expression<Func<VoucherGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .VoucherGrids
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<RegistrationGrid> RetrieveRegistrationGrid(int organisationId, Expression<Func<RegistrationGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .RegistrationGrids
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "RegistrationDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }
        
        public PagedResult<Attendance> RetrieveAttendances(int organisationId, Expression<Func<Attendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Attendances
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .Include(p => p.Personnel)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "AttendanceId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Attendance RetrieveAttendance(int organisationId, int attendanceId, Expression<Func<Attendance, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Attendances
                    .Include(c => c.Personnel)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.AttendanceId == attendanceId);

            }
        }

        public PagedResult<BatchAttendance> RetrieveBatchAttendances(int organisationId, Expression<Func<BatchAttendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .BatchAttendances
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .Include(p => p.Personnel)
                    .Include(p => p.Batch)
                    .Include(p => p.Attendance)
                    .Include(p => p.Subject)
                    .Include(p => p.Session)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "BatchAttendanceId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public BatchAttendance RetrieveBatchAttendance(int organisationId, int batchattendanceId, Expression<Func<BatchAttendance, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .BatchAttendances
                    .Include(c => c.Personnel)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.BatchAttendanceId == batchattendanceId);
            }
        }

        public PagedResult<AttendanceGrid> RetrieveAttendanceGrid(int organisationId, Expression<Func<AttendanceGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                      .AttendanceGrids
                      .AsNoTracking()
                      .Where(predicate)
                      .OrderBy(orderBy ?? new List<OrderBy>
                      {
                        new OrderBy
                        {
                            Property = "StudentCode",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                      })
                      .Paginate(paging);
            }
        }

        public PagedResult<CounsellingDataGrid> RetrieveCounsellingGrid(int organisationId, Expression<Func<CounsellingDataGrid, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CounsellingDataGrids
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CounsellingId",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<ExpenseDataGrid> RetrieveExpenseDataGrid(int organisationId, Expression<Func<ExpenseDataGrid, bool>> predicate, List<OrderBy> orderBy = null,Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .ExpenseDataGrids
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId, string searchKeyword, Expression<Func<MobilizationDataGrid, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                var searchData = context.Database
                    .SqlQuery<MobilizationDataGrid>("SearchMobilization @SearchKeyword", category).ToList();

                //var mobilizations = context.Mobilizations.Include(e => e.Course).Include(e => e.Qualification).Include(e => e.MobilizationType).Include(e => e.Centre);

                var data = searchData.ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Where(predicate)
                    .Paginate(paging);
                return data;
            }
        }

        public PagedResult<PettyCashExpenseReport> RetrievePettyCashExpenseReports(int organisationId, Expression<Func<PettyCashExpenseReport, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .PettyCashExpenseReports
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ExpenseCreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public PagedResult<Expense> RetrieveExpenses(int organisationId, int centreId, Expression<Func<Expense, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Expenses
                    .Include(e=>e.ExpenseHeader)
                    .Include(e => e.Centre)
                    .Include(e => e.Organisation)
                    .Include(e => e.ExpenseProjects)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDate",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }
        
        public Expense RetrieveExpense(int organisationId, int centreId, int expenseId, Expression<Func<Expense, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Expenses
                    .Include(e=>e.ExpenseProjects)
                    .Include(e => e.Centre)
                    .Include(e => e.ExpenseHeader)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.ExpenseId == expenseId);
            }
        }
        
        public IEnumerable<ExpenseProject> RetrieveExpenseProjects(int organisationId, int centreId, int expenseId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .ExpenseProjects
                    .Where(a => a.ExpenseId == expenseId && a.CentreId == centreId)
                    .Include(e => e.Expense)
                    .Include(e => e.Project)
                    .AsNoTracking()
                    .ToList();
            }
        }
        
        public CentreVoucherNumber RetrieveCentreVoucherNumber(int organisationId, int centreId, Expression<Func<CentreVoucherNumber, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CentreVoucherNumbers
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CentreId == centreId);
            }
        }

        public PagedResult<CentrePettyCash> RetrieveCentrePettyCashs(int organisationId, int centreId,
            Expression<Func<CentrePettyCash, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CentrePettyCashes
                    .Include(p => p.Organisation)
                    .Include(p => p.Centre)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                             {
                                 new OrderBy
                                 {
                                     Property = "CreatedDate",
                                     Direction = System.ComponentModel.ListSortDirection.Descending
                                 }
                             })
                    .Paginate(paging);
            }
        }

        public CentrePettyCash RetrieveCentrePettyCash(int organisationId, int centreId, int centrePettyCashId, Expression<Func<CentrePettyCash, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .CentrePettyCashes
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CentrePettyCashId == centrePettyCashId);
            }
        }


        public PagedResult<Project> RetrieveProjects(int organisationId, Expression<Func<Project, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Projects
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "ProjectId",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Project RetrieveProject(int organisationId, int projectId, Expression<Func<Project, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Projects
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.ProjectId == projectId);
            }
        }

       

        #endregion

        #region // Update


        public T UpdateEntityEntry<T>(T t) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();

                return t;
            }
        }

        public T UpdateOrganisationEntityEntry<T>(int organisationId, T t) where T : class
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                //context.Set<T>().Attach(t);
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();

                return t;
            }
        }

        #endregion

        #region //Delete
        public void Delete<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                var items = context.Set<T>().Where(predicate).FirstOrDefault();
                if (items != null) context.Set<T>().Remove(items);
                context.SaveChanges();
            }
        }

        #endregion

        //Document

        public IEnumerable<DocumentType> RetrieveDocumentTypes(int organisationId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .DocumentTypes
                    .AsNoTracking().ToList();
            }
        }

        public IEnumerable<Document> RetrieveDocuments(int organisationId, int centreId, string category, string studentCode)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Documents
                    .Include(e => e.DocumentType)
                    .AsNoTracking()
                    .Where(e =>
                     e.CentreId == centreId && e.DocumentType.Name.ToLower() == category.ToLower() &&
                     e.StudentCode == studentCode);
            }
        }

        public PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Documents
                    .Include(p => p.Organisation)
                    .Include(p => p.DocumentType)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDateTime",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .Documents
                    .FirstOrDefault(e => e.Guid == documentGuid);

            }
        }

        public PagedResult<Eventday> RetrieveEventdays(int organisationId, Expression<Func<Eventday, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            throw new NotImplementedException();
        }

    }
}
