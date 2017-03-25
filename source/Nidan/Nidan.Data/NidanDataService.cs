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

        public Admission CreateAdmission(int organisationId, Admission admission)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                admission = context.Admissions.Add(admission);
                context.SaveChanges();

                return admission;
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
        public RegistrationPaymentReceipt CreateRegistrationPaymentReceipt(int organisationId,
            RegistrationPaymentReceipt registrationPaymentReceipt)
        {
            using (var context = _databaseFactory.Create(organisationId))
            {
                registrationPaymentReceipt.Enquiry = null;
                registrationPaymentReceipt = context.RegistrationPaymentReceipts.Add(registrationPaymentReceipt);
                context.SaveChanges();

                return registrationPaymentReceipt;
            }
        }

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
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "GeneratedDate",
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

        //public PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy, Paging paging)
        //{
        //    using (ReadUncommitedTransactionScope)
        //    using (var context = _databaseFactory.Create(organisationId))
        //    {
        //        var category = new SqlParameter("@SearchKeyword", searchKeyword);

        //        return context.Database
        //            .SqlQuery<EnquirySearchField>("SearchEnquiry @SearchKeyword", category).ToList().AsQueryable().
        //            OrderBy(orderBy ?? new List<OrderBy>
        //            {
        //                new OrderBy
        //                {
        //                    Property = "CandidateName",
        //                    Direction = System.ComponentModel.ListSortDirection.Ascending
        //                }
        //            })
        //            .Paginate(paging);
        //    }
        //}

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
                    .Include(p => p.Course)
                    .Include(p => p.Qualification)
                    .Include(p => p.Religion)
                    .Include(p => p.CasteCategory)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "EnquiryDate",
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
                return context
                    .Enquiries
                    .Include(e => e.Counsellings)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.EnquiryId == enquiryId);

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
                    .Include(p => p.Course)
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

                var mobilizations = context.Mobilizations.Include(e => e.Course).Include(e => e.Qualification);

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
                return context.Database
                    .SqlQuery<EnquirySearchField>("SearchEnquiry @SearchKeyword", category).ToList()
                    .AsQueryable()
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CandidateName",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
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

        public PagedResult<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Courses
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

        public PagedResult<Admission> RetrieveAdmissions(int organisationId, Expression<Func<Admission, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .Admissions
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "AdmissionDate",
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
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.AdmissionId == admissionId);

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

                var counsellings = context.Counsellings.Include(e => e.Course).Include(e => e.Enquiry);

                var data = searchData.Join(counsellings, e => e.EnquiryId, m => m.EnquiryId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
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

        public PagedResult<RegistrationPaymentReceipt> RetrieveRegistrationPaymentReceipts(int organisationId, Expression<Func<RegistrationPaymentReceipt, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                  .RegistrationPaymentReceipts
                    .Include(p => p.Organisation)
                    .Include(p => p.Enquiry)
                    .Include(p => p.PaymentMode)
                    .Include(p => p.Course)
                    .Include(p => p.Enquiry.Scheme)
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

        public RegistrationPaymentReceipt RetrieveRegistrationPaymentReceipt(int organisationId, int registrationPaymentReceiptId,
            Expression<Func<RegistrationPaymentReceipt, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {
                return context
                    .RegistrationPaymentReceipts
                    .Include(e => e.Enquiry)
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.RegistrationPaymentReceiptId == registrationPaymentReceiptId);

            }
        }

  //public PagedResult<Postevent> RetrievePostevents(int organisationId, Expression<Func<Postevent, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
  //{
  //}
        public PagedResult<CourseInstallment> RetrieveCourseInstallments(int organisationId, Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create(organisationId))
            {

                return context
                    .CourseInstallments
                    .Include(p => p.Organisation)
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
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CourseInstallmentId == courseInstallmentId);

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
                    .Include(p => p.Trainer)
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

                var trainers = context.Trainers.Include(e => e.Course).Include(e => e.Sector);

                var data = searchData.Join(trainers, e => e.TrainerId, m => m.TrainerId, (e, m) => m).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "FollowUpDate",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
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
                context.Set<T>().Remove(items);
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
