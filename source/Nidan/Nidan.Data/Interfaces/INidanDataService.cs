using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nidan.Entity;
using Nidan.Entity.Dto;


namespace Nidan.Data.Interfaces
{
    public interface INidanDataService
    {

        // Create
        Personnel CreatePersonnel(int organisationId, Personnel personnel);
        T Create<T>(int organisationId, T t) where T : class;
        void Create<T>(int organisationId, IEnumerable<T> t) where T : class;

        AbsenceType CreateAbsenceType(int organisationId, AbsenceType absenceType);

        Enquiry CreateEnquiry(int organisationId, Enquiry enquiry);

        Mobilization CreateMobilization(int organisationId, Mobilization mobilization);

        AreaOfInterest CreateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest);
        Centre CreateCentre(int organisationId, Centre centre);
        Admission CreateAdmission(int organisationId, Admission admission);
        Batch CreateBatch(int organisationId, Batch batch);
        Question CreateQuestion(int organisationId, Question question);
        Event CreateEvent(int organisationId, Event eventplan);
        Brainstorming CreateBrainstorming(int organisationId, Brainstorming brainstorming);
        Planning CreatePlanning(int organisationId, Planning planning);
        Budget CreateBudget(int organisationId, Budget budget);
        Eventday CreateEventday(int organisationId, Eventday eventday);
        Postevent CreatePostevent(int organisationId, Postevent postevent);

        // Retrieve
        PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate);
        AbsenceType RetrieveAbsenceType(int organisationId, int absenceTypeId, Expression<Func<AbsenceType, bool>> predicate);
        PagedResult<AbsenceType> RetrieveAbsenceTypes(int organisationId, Expression<Func<AbsenceType, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        IEnumerable<Colour> RetrieveColours(Expression<Func<Colour, bool>> predicate);
        IEnumerable<Host> RetrieveHosts();
        IEnumerable<Organisation> RetrieveOrganisations();
        Personnel RetrievePersonnel(int organisationId, int personnelId, Expression<Func<Personnel, bool>> predicate);
        IEnumerable<Personnel> RetrievePersonnel(int organisationId, IEnumerable<int> companyIds, IEnumerable<int> departmentIds, IEnumerable<int> divisionIds);
        PagedResult<Personnel> RetrievePersonnel(int organisationId, Expression<Func<Personnel, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        UserAuthorisationFilter RetrieveUserAuthorisation(string aspNetUserId);
        PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        List<T> Retrieve<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class;
        bool PersonnelEmploymentHasAbsences(int organisationId, int personnelId, int employmentId);
        Question RetrieveQuestion(int organisationId, int questionId, Expression<Func<Question, bool>> predicate);
        PagedResult<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Enquiry RetrieveEnquiry(int organisationId, int enquiryId, Expression<Func<Enquiry, bool>> predicate);
        PagedResult<Mobilization> RetrieveMobilizations(int organisationId, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Mobilization RetrieveMobilization(int organisationId, int mobilizationId, Expression<Func<Mobilization, bool>> predicate);
        AreaOfInterest RetrieveAreaOfInterest(int organisationId, int areaOfInterestId, Expression<Func<AreaOfInterest, bool>> predicate);
        PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, Expression<Func<AreaOfInterest, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        FollowUp RetrieveFollowUp(int organisationId, int followUpId, Expression<Func<FollowUp, bool>> predicate);
        PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Mobilization, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<EnquirySearchField, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate);
        PagedResult<Counselling> RetrieveCounsellings(int organisationId, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Counselling RetrieveCounselling(int organisationId, int counsellingId, Expression<Func<Counselling, bool>> predicate);
        PagedResult<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Batch RetrieveBatch(int organisationId, int batchId, Expression<Func<Batch, bool>> predicate);
        PagedResult<Admission> RetrieveAdmissions(int organisationId, Expression<Func<Admission, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Admission RetrieveAdmission(int organisationId, int admissionId, Expression<Func<Admission, bool>> predicate);
        PagedResult<Counselling> RetrieveCounsellingBySearchKeyword(int organisationId, string searchKeyword, Expression<Func<Counselling, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Brainstorming RetrieveBrainstorming(int organisationId, int brainstormingId, Expression<Func<Brainstorming, bool>> predicate);
        PagedResult<Brainstorming> RetrieveBrainstormings(int organisationId, Expression<Func<Brainstorming, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Planning RetrievePlanning(int organisationId, int planningId, Expression<Func<Planning, bool>> predicate);
        PagedResult<Planning> RetrievePlannings(int organisationId, Expression<Func<Planning, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Budget RetrieveBudget(int organisationId, int budgetId, Expression<Func<Budget, bool>> predicate);
        PagedResult<Budget> RetrieveBudgets(int organisationId, Expression<Func<Budget, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Eventday RetrieveEventday(int organisationId, int eventdayId, Expression<Func<Eventday, bool>> predicate);
        PagedResult<Eventday> RetrieveEventdays(int organisationId, Expression<Func<Eventday, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Postevent RetrievePostevent(int organisationId, int posteventId, Expression<Func<Postevent, bool>> predicate);
        PagedResult<Postevent> RetrievePostevents(int organisationId, Expression<Func<Postevent, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Template RetrieveTemplateDetails(int organisationId,string name);

        // Update

        T UpdateEntityEntry<T>(T t) where T : class;
        T UpdateOrganisationEntityEntry<T>(int organisationId, T t) where T : class;
        // Delete
        void Delete<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class;


        //Document
        IEnumerable<DocumentType> RetrieveDocumentTypes(int organisationId);
        IEnumerable<Document> RetrieveDocuments(int organisationId, int centreId, string category, string studentCode);
        PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Document RetrieveDocument(int organisationId, Guid documentGuid);
    }
}
