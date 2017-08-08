﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nidan.Business.Models;
using Nidan.Entity;
using Nidan.Entity.Dto;

namespace Nidan.Business.Interfaces
{
    public interface INidanBusinessService
    {
        //Create
        Personnel CreatePersonnel(int organisationId, Personnel personnel);
        Question CreateQuestion(int organisationId, Question personnel);
        Enquiry CreateEnquiry(int organisationId, int personnelId, Enquiry enquiry, List<int> courseIds);
        Mobilization CreateMobilization(int organisationId, Mobilization mobilization);
        ValidationResult<AreaOfInterest> CreateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest);
        Centre CreateCentre(int organisationId, Centre centre);
        Batch CreateBatch(int organisationId, Batch batch, BatchDay batchDays, List<int> trainerIds);
        BatchDay CreateBatchDay(int organisationId, BatchDay batchDay);
        void UploadMobilization(int organisationId, int centreId, int eventId, int personnelId, DateTime generateDateTime, List<Mobilization> mobilizations);
        void UploadSession(int organisationId, List<Session> sessions);
        Counselling CreateCounselling(int organisationId, Counselling counselling);
        //RegistrationPaymentReceipt CreateRegistrationPaymentReceipt(int organisationId, RegistrationPaymentReceipt registrationPaymentReceipt);
        Enquiry CreateEnquiryFromMobilization(int organisationId, int centreId, int mobilizationId);
        Course CreateCourse(int organisationId, Course course);
        CourseInstallment CreateCourseInstallment(int organisationId, CourseInstallment courseInstallment);
        Event CreateEvent(int organisationId, Event eventplan);
        Brainstorming CreateBrainstorming(int organisationId, Brainstorming brainstorming);
        Planning CreatePlanning(int organisationId, Planning planning);
        Budget CreateBudget(int organisationId, Budget budget);
        Eventday CreateEventday(int organisationId, Eventday eventday);
        PostEvent CreatePostEvent(int organisationId, PostEvent postEvent);
        Trainer CreatetTrainer(int organisationId, Trainer trainer);
        FollowUp CreateFollowUp(int organisationId, FollowUp followUp);
        Subject CreateSubject(int organisationId, Subject subject, List<int> courseIds, List<int> trainerIds);
        Session CreateSession(int organisationId, Session session);
        Room CreateRoom(int organisationId, Room room);
        EnquiryCourse CreateEnquiryCourse(int organisationId, EnquiryCourse employmentDepartment);
        SubjectCourse CreateSubjectCourse(int organisationId, SubjectCourse subjectCourse);
        SubjectTrainer CreateSubjectTrainer(int organisationId, SubjectTrainer subjectTrainer);
        BatchTrainer CreateBatchTrainer(int organisationId, BatchTrainer batchTrainer);
        CentreCourse CreateCentreCourse(int organisationId, int centreId, int courseId);
        CentreCourseInstallment CreateCentreCourseInstallment(int organisationId, int centreId, int courseInstallmentId);
        CentreScheme CreateCentreScheme(int organisationId, int centreId, int schemeId);
        CentreSector CreateCentreSector(int organisationId, int centreId, int sectorId);
        Admission CreateAdmission(int organisationId, int centreId, int personnelId, Admission admission, CandidateFee candidateFee);
        CandidateFee CreateCandidateFee(int organisationId, CandidateFee candidateFee);
        FollowUpHistory CreateFollowUpHistory(int organisationId, FollowUpHistory followUpHistory);
        Module CreateModule(int organisationId, Module module);
        RoomAvailable CreateRoomAvailable(int organisationId, RoomAvailable roomAvailable);
        TrainerAvailable CreateTrainerAvailable(int organisationId, TrainerAvailable trainerAvailable);
        ExpenseHeader CreateExpenseHeader(int organisationId, ExpenseHeader expenseHeader);
        void AssignBatch(int organisationId, int centreId, int personnelId, Admission admission);
        OtherFee CreateOtherFee(int organisationId, int centreId, OtherFee otherFee);
        Expense CreateExpense(int organisationId, int centreId, Expense expense, List<int> projectIds);
        CentrePettyCash CreateCentrePettyCash(int organisationId, int centreId, int personnelId, CentrePettyCash centrePettyCash);
        Voucher CreateVoucher(int organisationId, int centreId, int personnelId, Voucher voucher);
        ExpenseProject CreateExpenseProject(int organisationId, ExpenseProject expenseProject);
        Attendance CreateAttendance(int organisationId, int centreId, int personnelId, Attendance attendance);
        BatchAttendance CreateBatchAttendance(int organisationId, int centreId, int personnelId, BatchAttendance batchAttendance);
        EventBrainstorming CreateEventBrainstorming(int organisationId, int centreId, EventBrainstorming eventBrainstorming);
        bool MarkAttendance(int organisationId, int centreId, int personnelId, List<AttendanceGrid> attendances, int subjectId, int sessionId);
        EventBudget CreateEventBudget(int organisationId, EventBudget eventBudget);
        EventPlanning CreateEventPlanning(int organisationId, EventPlanning eventPlanning);

        // Retrieve
        PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate);
        AbsenceType RetrieveAbsenceType(int organisationId, int absenceTypeId);
        PagedResult<AbsenceType> RetrieveAbsenceTypes(int organisationId, List<OrderBy> orderBy, Paging paging);
        IEnumerable<Colour> RetrieveColours();
        Organisation RetrieveOrganisation(int organisationId);
        IAuthorisation RetrieveUserAuthorisation(string aspNetUserId);
        Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId, int? personnelId = null);
        PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy, Paging paging);
        Personnel RetrievePersonnel(int organisationId, int id);
        PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Question RetrieveQuestion(int organisationId, int questionId, Expression<Func<Question, bool>> predicate);
        List<EventActivityType> RetrieveActivityTypes(int organisationId);
        PagedResult<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Enquiry RetrieveEnquiry(int organisationId, int enquiryId, Expression<Func<Enquiry, bool>> predicate);
        Mobilization RetrieveMobilization(int organisationId, int id);
        PagedResult<Mobilization> RetrieveMobilizations(int organisationId, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Mobilization RetrieveMobilization(int organisationId, int mobilizationId, Expression<Func<Mobilization, bool>> predicate);
        Enquiry RetrieveEnquiry(int organisationId, int id);
        AreaOfInterest RetrieveAreaOfInterest(int organisationId, int areaOfInterestId);
        PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, List<OrderBy> orderBy, Paging paging);
        PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        FollowUp RetrieveFollowUp(int organisationId, int followUpId);
        List<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate);
        List<CourseType> RetrieveCourseTypes(int organisationId, Expression<Func<CourseType, bool>> predicate);
        List<RoomType> RetrieveRoomTypes(int organisationId, Expression<Func<RoomType, bool>> predicate);
        List<Trainer> RetrieveTrainers(int organisationId, Expression<Func<Trainer, bool>> predicate);
        List<OtherFee> RetrieveOtherFees(int organisationId, Expression<Func<OtherFee, bool>> predicate);
        List<Qualification> RetrieveQualifications(int organisationId, Expression<Func<Qualification, bool>> predicate);
        List<Religion> RetrieveReligions(int organisationId, Expression<Func<Religion, bool>> predicate);
        List<CasteCategory> RetrieveCasteCategories(int organisationId, Expression<Func<CasteCategory, bool>> predicate);
        List<HowDidYouKnowAbout> RetrieveHowDidYouKnowAbouts(int organisationId, Expression<Func<HowDidYouKnowAbout, bool>> predicate);
        List<Occupation> RetrieveOccupations(int organisationId, Expression<Func<Occupation, bool>> predicate);
        List<Scheme> RetrieveSchemes(int organisationId, Expression<Func<Scheme, bool>> predicate);
        List<Sector> RetrieveSectors(int organisationId, Expression<Func<Sector, bool>> predicate);
        List<BatchTimePrefer> RetrieveBatchTimePrefers(int organisationId, Expression<Func<BatchTimePrefer, bool>> predicate);
        List<StudentType> RetrieveStudentTypes(int organisationId, Expression<Func<StudentType, bool>> predicate);
        List<EnquiryType> RetrieveEnquiryTypes(int organisationId, Expression<Func<EnquiryType, bool>> predicate);
        List<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate);
        List<State> RetrieveStates(int organisationId, Expression<Func<State, bool>> predicate);
        List<District> RetrieveDistricts(int organisationId, Expression<Func<District, bool>> predicate);
        List<Taluka> RetrieveTalukas(int organisationId, Expression<Func<Taluka, bool>> predicate);
        List<EventFunctionType> RetrieveEventFunctionTypes(int organisationId, Expression<Func<EventFunctionType, bool>> predicate);
        List<PaymentMode> RetrievePaymentModes(int organisationId, Expression<Func<PaymentMode, bool>> predicate);
        List<CourseInstallment> RetrieveCourseInstallments(int organisationId, Expression<Func<CourseInstallment, bool>> predicate);
        List<Room> RetrieveRooms(int organisationId, Expression<Func<Room, bool>> predicate);
        PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        List<MobilizationType> RetrieveMobilizationTypes(int organisationId, Expression<Func<MobilizationType, bool>> predicate);
        PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate);
        Centre RetrieveCentre(int organisationId, int id);
        PagedResult<Counselling> RetrieveCounsellings(int organisationId, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Counselling RetrieveCounselling(int organisationId, int counsellingId, Expression<Func<Counselling, bool>> predicate);
        Counselling RetrieveCounselling(int organisationId, int id);
        Batch RetrieveBatch(int organisationId, int id);
        PagedResult<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Batch RetrieveBatch(int organisationId, int batchId, Expression<Func<Batch, bool>> predicate);
        //List<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate);
        List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate);
        PagedResult<Counselling> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Brainstorming RetrieveBrainstorming(int organisationId, int id);
        Brainstorming RetrieveBrainstorming(int organisationId, int brainstormingId, Expression<Func<Brainstorming, bool>> predicate);
        PagedResult<Brainstorming> RetrieveBrainstormings(int organisationId, Expression<Func<Brainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Planning RetrievePlanning(int organisationId, int id);
        Planning RetrievePlanning(int organisationId, int planningId, Expression<Func<Planning, bool>> predicate);
        PagedResult<Planning> RetrievePlannings(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Budget RetrieveBudget(int organisationId, int id);
        Budget RetrieveBudget(int organisationId, int budgetId, Expression<Func<Budget, bool>> predicate);
        PagedResult<Budget> RetrieveBudgets(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Eventday RetrieveEventday(int organisationId, int id);
        Eventday RetrieveEventday(int organisationId, int eventdayId, Expression<Func<Eventday, bool>> predicate);
        PagedResult<Eventday> RetrieveEventdays(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        PostEvent RetrievePostEvent(int organisationId, int postEventId, Expression<Func<PostEvent, bool>> predicate);
        PagedResult<PostEvent> RetrievePostEvents(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Trainer RetrieveTrainer(int organisationId, int id);
        PagedResult<Trainer> RetrieveTrainers(int organisationId, Expression<Func<Trainer, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Trainer RetrieveTrainer(int organisationId, int trainerId, Expression<Func<Trainer, bool>> predicate);
        PagedResult<Registration> RetrieveRegistrations(int organisationId, Expression<Func<Registration, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        //RegistrationPaymentReceipt RetrieverRegistrationPaymentReceipt(int organisationId, int registrationPaymentReceiptId, Expression<Func<RegistrationPaymentReceipt, bool>> predicate);
        //RegistrationPaymentReceipt RetrieveRegistrationPaymentReceipt(int organisationId, int id);
        PagedResult<Trainer> RetrieveTrainerBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Trainer, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Holiday> RetrieveHolidays(int organisationId, Expression<Func<Holiday, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Holiday RetrieveHoliday(int organisationId, int holidayId, Expression<Func<Holiday, bool>> predicate);
        Holiday RetrieveHoliday(int organisationId, int id);
        Course RetrieveCourse(int organisationId, int id);
        PagedResult<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Course RetrieveCourse(int organisationId, int courseId, Expression<Func<Course, bool>> predicate);
        PagedResult<Course> RetrieveCourseBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Course, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<CourseInstallment> RetrieveCourseInstallments(int organisationId, Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        CourseInstallment RetrieveCourseInstallment(int organisationId, int courseInstallmentId, Expression<Func<CourseInstallment, bool>> predicate);
        CourseInstallment RetrieveCourseInstallment(int organisationId, int id);
        PagedResult<CourseInstallment> RetrieveCourseInstallmentBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CourseInstallment, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Subject> RetrieveSubjects(int organisationId, Expression<Func<Subject, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Subject RetrieveSubject(int organisationId, int subjectId, Expression<Func<Subject, bool>> predicate);
        Subject RetrieveSubject(int organisationId, int id);
        PagedResult<Session> RetrieveSessions(int organisationId, Expression<Func<Session, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Session RetrieveSession(int organisationId, int sessionId, Expression<Func<Session, bool>> predicate);
        Session RetrieveSession(int organisationId, int id);
        List<Subject> RetrieveSubjects(int organisationId, Expression<Func<Subject, bool>> predicate);
        PagedResult<Room> RetrieveRooms(int organisationId, Expression<Func<Room, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Room RetrieveRoom(int organisationId, int roomId, Expression<Func<Room, bool>> predicate);
        Room RetrieveRoom(int organisationId, int id);
        BatchDay RetrieveBatchDay(int organisationId, int id);
        PagedResult<BatchDay> RetrieveBatchDays(int organisationId, Expression<Func<BatchDay, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        BatchDay RetrieveBatchDay(int organisationId, int batchDayId, Expression<Func<BatchDay, bool>> predicate);
        IEnumerable<EnquiryCourse> RetrieveEnquiryCourses(int organisationId, int centreId, int enquiryId);
        IEnumerable<Course> RetrieveUnassignedCentreCourses(int organisationId, int centreId);
        PagedResult<CentreCourse> RetrieveCentreCourses(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        IEnumerable<SubjectCourse> RetrieveSubjectCourses(int organisationId, Expression<Func<SubjectCourse, bool>> predicate);
        IEnumerable<SubjectTrainer> RetrieveSubjectTrainers(int organisationId, int subjectId);
        IEnumerable<BatchTrainer> RetrieveBatchTrainers(int organisationId, int batchId);
        IEnumerable<CourseInstallment> RetrieveUnassignedCentreCourseInstallments(int organisationId, int centreId);
        PagedResult<CentreCourseInstallment> RetrieveCentreCourseInstallments(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        IEnumerable<Scheme> RetrieveUnassignedCentreSchemes(int organisationId, int centreId);
        PagedResult<CentreScheme> RetrieveCentreSchemes(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        IEnumerable<Sector> RetrieveUnassignedCentreSectors(int organisationId, int centreId);
        PagedResult<CentreSector> RetrieveCentreSectors(int organisationId, int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Admission> RetrieveAdmissions(int organisationId, Expression<Func<Admission, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Admission RetrieveAdmission(int organisationId, int admissionId, Expression<Func<Admission, bool>> predicate);
        Admission RetrieveAdmission(int organisationId, int centreId, int id);
        List<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate);
        PagedResult<CandidateFee> RetrieveCandidateFees(int organisationId, Expression<Func<CandidateFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        CandidateFee RetrieveCandidateFee(int organisationId, int candidateFeeId, Expression<Func<CandidateFee, bool>> predicate);
        CandidateFee RetrieveCandidateFee(int organisationId, int id);
        PagedResult<CandidateInstallment> RetrieveCandidateInstallments(int organisationId, Expression<Func<CandidateInstallment, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        CandidateInstallment RetrieveCandidateInstallment(int organisationId, int candidateInstallmentId, Expression<Func<CandidateInstallment, bool>> predicate);
        PagedResult<CandidateFeeSearchField> RetrieveCandidateFeeBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CandidateFeeSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        List<Course> RetrieveCentreCourses(int organisationId, int centreId, Expression<Func<CentreCourse, bool>> predicate);
        List<Scheme> RetrieveCentreSchemes(int organisationId, int centreId, Expression<Func<CentreScheme, bool>> predicate);
        List<Sector> RetrieveCentreSectors(int organisationId, int centreId, Expression<Func<CentreSector, bool>> predicate);
        List<Room> RetrieveRooms(int organisationId, int centreId, Expression<Func<Room, bool>> predicate);
        List<RoomAvailable> RetrieveRoomAvailables(int organisationId, int centreId, Expression<Func<RoomAvailable, bool>> predicate);
        List<Trainer> RetrieveTrainers(int organisationId, int centreId, Expression<Func<TrainerAvailable, bool>> predicate);
        Registration RetrieveRegistration(int organisationId, int id);
        List<CourseInstallment> RetrieveCourseInstallments(int organisationId, int centreId);
        List<Graph> RetrievePieGraphStatistics(int organisationId, Expression<Func<Centre, bool>> predicate);
        Registration CreateCandidateRegistration(int organisationId, int centreId, int personnelId, string studentCode, Registration registration);
        List<Graph> RetrieveBarGraphStatistics(int organisationId, Expression<Func<Centre, bool>> predicate);
        Registration RetrieveRegistration(int organisationId, int centreId, int registraionId);
        PagedResult<FollowUpHistory> RetrieveFollowUpHistories(int organisationId, Expression<Func<FollowUpHistory, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        FollowUpHistory RetrieveFollowUpHistory(int organisationId, int followUpHistoryId, Expression<Func<FollowUpHistory, bool>> predicate);
        PagedResult<FollowUp> RetrieveFollowUpBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Registration> RetrieveRegistrationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Registration, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<AdmissionSearchField> RetrieveAdmissionBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<AdmissionSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Module RetrieveModule(int organisationId, int id);
        PagedResult<Module> RetrieveModules(int organisationId, Expression<Func<Module, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Module RetrieveModule(int organisationId, int moduleId, Expression<Func<Module, bool>> predicate);
        PagedResult<CandidateInstallmentSearchField> RetrieveCandidateInstallmentBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<CandidateInstallmentSearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<AdmissionGrid> RetrieveAdmissionGrid(int organisationId, Expression<Func<AdmissionGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<CandidateInstallmentGrid> RetrieveCandidateInstallmentGrid(int organisationId, Expression<Func<CandidateInstallmentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        BatchMonth GetBatchDetail(int organisationId, int centreId, int numberOfCourseHours, DateTime startDate, int dailyBatchHours, int numberOfWeekDays, int courseFee, int downPayment);
        PagedResult<ExpenseHeader> RetrieveExpenseHeaders(int organisationId, Expression<Func<ExpenseHeader, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        ExpenseHeader RetrieveExpenseHeader(int organisationId, int expenseHeaderId, Expression<Func<ExpenseHeader, bool>> predicate);
        PagedResult<OtherFee> RetrieveOtherFees(int organisationId, int centreId, Expression<Func<OtherFee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        OtherFee RetrieveOtherFee(int organisationId, int centreId, int otherFeeId, Expression<Func<OtherFee, bool>> predicate);
        PagedResult<Expense> RetrieveExpenses(int organisationId, int centreId, Expression<Func<Expense, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Expense RetrieveExpense(int organisationId, int centreId, int expenseId, Expression<Func<Expense, bool>> predicate);
        IEnumerable<ExpenseProject> RetrieveExpenseProjects(int organisationId, int centreId, int expenseId);
        List<Project> RetrieveProjects(int organisationId, int projectId, Expression<Func<Project, bool>> predicate);
        PagedResult<Project> RetrieveProjects(int organisationId, Expression<Func<Project, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Project RetrieveProject(int organisationId, int projectId, Expression<Func<Project, bool>> predicate);
        PagedResult<CentrePettyCash> RetrieveCentrePettyCashs(int organisationId, int centreId, Expression<Func<CentrePettyCash, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        CentrePettyCash RetrieveCentrePettyCash(int organisationId, int centreId, int centrePettyCashId, Expression<Func<CentrePettyCash, bool>> predicate);
        PagedResult<CandidateFeeGrid> RetrieveCandidateFeeGrid(int organisationId, Expression<Func<CandidateFeeGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId, Expression<Func<MobilizationDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<EnquiryDataGrid> RetrieveEnquiryDataGrid(int organisationId, Expression<Func<EnquiryDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<FollowUpDataGrid> RetrieveFollowUpDataGrid(int organisationId, Expression<Func<FollowUpDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Voucher> RetrieveVouchers(int organisationId, int centreId, Expression<Func<Voucher, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Voucher RetrieveVoucher(int organisationId, int centreId, int voucherId, Expression<Func<Voucher, bool>> predicate);
        PagedResult<VoucherGrid> RetrieveVoucherGrids(int organisationId, int centreId, Expression<Func<VoucherGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<RegistrationGrid> RetrieveRegistrationGrid(int organisationId, Expression<Func<RegistrationGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        CentreVoucherNumber RetrieveCentreVoucherNumber(int organisationId, int centreId, Expression<Func<CentreVoucherNumber, bool>> predicate);
        PagedResult<Attendance> RetrieveAttendances(int organisationId, Expression<Func<Attendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Attendance RetrieveAttendance(int organisationId, int attendanceId, Expression<Func<Attendance, bool>> predicate);
        Attendance RetrieveAttendance(int organisationId, int id);
        PagedResult<BatchAttendance> RetrieveBatchAttendances(int organisationId, Expression<Func<BatchAttendance, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        BatchAttendance RetrieveBatchAttendance(int organisationId, int batchAttendanceId, Expression<Func<BatchAttendance, bool>> predicate);
        BatchAttendance RetrieveBatchAttendance(int organisationId, int id);
        PagedResult<AttendanceGrid> RetrieveAttendanceGrid(int organisationId, Expression<Func<AttendanceGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<CounsellingDataGrid> RetrieveCounsellingGrid(int organisationId, Expression<Func<CounsellingDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<ExpenseDataGrid> RetrieveExpenseDataGrid(int organisationId, Expression<Func<ExpenseDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        IEnumerable<ExpensePettyCashData> RetrieveExpensePettyCashDataByCentre(int organisationId, int centreId, DateTime startDate, DateTime endDate);
        PagedResult<MobilizationDataGrid> RetrieveMobilizationDataGrid(int organisationId, string searchKeyword, Expression<Func<MobilizationDataGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<PettyCashExpenseReport> RetrievePettyCashExpenseReports(int organisationId, Expression<Func<PettyCashExpenseReport, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<EventBrainstorming> RetrieveEventBrainstormings(int organisationId, int centreId, Expression<Func<EventBrainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        EventBrainstorming RetrieveEventBrainstorming(int organisationId, int centreId, int eventBrainstormingId, Expression<Func<EventBrainstorming, bool>> predicate);
        PagedResult<MobilizationCentreReportMonthWise> RetriveMobilizationCountReportByMonthAndYear(int organisationId, int centreId, Expression<Func<MobilizationCentreReportMonthWise, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<MobilizationCentreReport> RetriveMobilizationCountReportByDate(int organisationId, int centreId, Expression<Func<MobilizationCentreReport, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Gst RetrieveGst(int organisationId, Expression<Func<Gst, bool>> predicate);
        PagedResult<Gst> RetrieveGsts(int organisationId, Expression<Func<Gst, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        EventBudget RetrieveEventBudget(int organisationId, int centreId, int eventBudgetId, Expression<Func<EventBudget, bool>> predicate);
        PagedResult<EventBudget> RetrieveEventBudgets(int organisationId, int centreId, Expression<Func<EventBudget, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        EventPlanning RetrieveEventPlanning(int organisationId, int centreId, int eventPlanningId, Expression<Func<EventPlanning, bool>> predicate);
        PagedResult<EventPlanning> RetrieveEventPlannings(int organisationId, int centreId, Expression<Func<EventPlanning, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);

        // Update
        //void UploadPhoto(int organisationId, int personnelId, byte[] photo);
        Personnel UpdatePersonnel(int organisationId, Personnel personnel);
        Enquiry UpdateEnquiry(int organisationId, Enquiry enquiry, List<int> courseIds);
        Mobilization UpdateMobilization(int organisationId, Mobilization mobilization);
        ValidationResult<AreaOfInterest> UpdateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest);
        FollowUp UpdateFollowUp(int organisationId, FollowUp followUp);
        Centre UpdateCentre(int organisationId, Centre centre);
        Counselling UpdateCounselling(int organisationId, Counselling counselling);
        //RegistrationPaymentReceipt UpdateRegistrationPaymentReceipt(int organisationId, RegistrationPaymentReceipt registrationPaymentReceipt);
        Batch UpdateBatch(int organisationId, Batch batch);
        Batch UpdateBatch(int organisationId, Batch batch, BatchDay batchDays, List<int> trainerIds);
        Admission UpdateAdmission(int organisationId, int centreId, int personnelId, Admission admission);
        Course UpdateCourse(int organisationId, Course course);
        CourseInstallment UpdateCourseInstallment(int organisationId, CourseInstallment courseInstallment);
        Question UpdateQuestion(int organisationId, Question question);
        Event UpdateEvent(int organisationId, Event eventplan);
        Trainer UpdateTrainer(int organisationId, Trainer trainer);
        Subject UpdateSubject(int organisationId, Subject subject, List<int> courseIds, List<int> trainerIds);
        Session UpdateSession(int organisationId, Session session);
        Room UpdateRoom(int organisationId, Room room);
        BatchDay UpdateBatchDay(int organisationId, BatchDay batchDay);
        CandidateFee UpdateCandidateFee(int organisationId, CandidateFee candidateFee);
        Registration UpdateRegistartion(int organisationId, Registration registration);
        Module UpdateModule(int organisationId, Module module);
        ExpenseHeader UpdateExpenseHeader(int organisationId, ExpenseHeader expenseHeader);
        OtherFee UpdateOtherFee(int organisationId, int centreId, OtherFee otherFee);
        Expense UpdateExpense(int organisationId, int centreId, Expense expense, List<int> projectIds);
        CentrePettyCash UpdateCentrePettyCash(int organisationId, int centreId, int personnelId, CentrePettyCash centrePettyCash);
        Attendance UpdateAttendance(int organisationId, Attendance attendance);
        BatchAttendance UpdateBatchAttendance(int organisationId, BatchAttendance batchAttendance);
        CentreVoucherNumber UpdateCentreVoucherNumber(int organisationId, int centreId, CentreVoucherNumber centreVoucherNumber);

        //Delete
        void DeletePersonnel(int organisationId, int personnelId);
        void DeleteFollowUp(int organisationId, int followUpId);

        void MarkAsReadFollowUp(int organisationId, int id);
        void DeleteEnquiryCourse(int organisationId, int enquiryId, int courseId);
        void DeleteSubjectCourse(int organisationId, int subjectId, int courseId);
        void DeleteSubjectTrainer(int organisationId, int subjectId, int trainerId);
        void DeleteBatchTrainer(int organisationId, int batchId, int trainerId);
        void DeleteCentreCourse(int organisationId, int centreId, int courseId);
        void DeleteCentreScheme(int organisationId, int centreId, int schemeId);
        void DeleteCentreSector(int organisationId, int centreId, int sectorId);
        void DeleteCentreCourseInstallment(int organisationId, int centreId, int courseInstallmentId);
        void DeleteOtherFee(int organisationId, int centreId, int otherFeeId);
        void DeleteExpenseProject(int organisationId, int expenseId, int projectId);

        //Document
        List<DocumentType> RetrieveDocumentTypes(int organisationId);
        PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Document RetrieveDocument(int organisationId, Guid documentGuid);
        //Document CreateDocument(int organisationId, int centreId, int documentTypeId, string filePath,
        //    string studentCode, string studentName, string description, string fileName);

        //Template
        byte[] CreateRegistrationRecieptBytes(int organisationId, int centreId, int registrationId);
        byte[] CreateEnrollmentBytes(int organisationId, int centreId, Admission admission);
        byte[] CreateOtherFeeBytes(int organisationId, int centreId, List<OtherFee> otherFees);
        byte[] CreateExpenseBytes(int organisationId, int centreId, Expense expense);
    }
}
