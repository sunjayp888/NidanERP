﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Security.Policy;
using Nidan.Business.Models;
using Nidan.Entity;
using Nidan.Entity.Dto;
using System.Linq.Expressions;

namespace Nidan.Business.Interfaces
{
    public interface INidanBusinessService
    {
        //Create
        Personnel CreatePersonnel(int organisationId, Personnel personnel);
        Question CreateQuestion(int organisationId, Question personnel);
        Enquiry CreateEnquiry(int organisationId, Enquiry enquiry);
        Mobilization CreateMobilization(int organisationId, Mobilization mobilization);
        ValidationResult<AreaOfInterest> CreateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest);
        Centre CreateCentre(int organisationId, Centre centre);
        Batch CreateBatch(int organisationId, Batch batch);
        void UploadMobilization(int organisationId, int eventId, int personnelId, DateTime generateDateTime, List<Mobilization> mobilizations);
        Admission CreateAdmission(int organisationId, Admission admission);

        // Retrieve
        PagedResult<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Event RetrieveEvent(int organisationId, int eventId, Expression<Func<Event, bool>> predicate);
        AbsenceType RetrieveAbsenceType(int organisationId, int absenceTypeId);
        PagedResult<AbsenceType> RetrieveAbsenceTypes(int organisationId, List<OrderBy> orderBy, Paging paging);
        IEnumerable<Colour> RetrieveColours();
        Organisation RetrieveOrganisation(int organisationId);
        IAuthorisation RetrieveUserAuthorisation(string aspNetUserId);
        Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId, int? personnelId = null);
        PagedResult<Personnel> RetrievePersonnel(int organisationId,int centreId, List<OrderBy> orderBy, Paging paging);
        Personnel RetrievePersonnel(int organisationId, int id);
        PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Question> RetrieveQuestions(int organisationId, Expression<Func<Question, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Question RetrieveQuestion(int organisationId, int questionId, Expression<Func<Question, bool>> predicate);
        List<EventActivityType> RetrieveActivityTypes(int organisationId); 
        PagedResult<Enquiry> RetrieveEnquiries(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Enquiry RetrieveEnquiry(int organisationId, int enquiryId, Expression<Func<Enquiry, bool>> predicate);
        Mobilization RetrieveMobilization(int organisationId, int id);
        PagedResult<Mobilization> RetrieveMobilizations(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Mobilization RetrieveMobilization(int organisationId, int mobilizationId, Expression<Func<Mobilization, bool>> predicate);
        Enquiry RetrieveEnquiry(int organisationId, int id);
        AreaOfInterest RetrieveAreaOfInterest(int organisationId, int areaOfInterestId);
        PagedResult<AreaOfInterest> RetrieveAreaOfInterests(int organisationId, List<OrderBy> orderBy, Paging paging);
        PagedResult<FollowUp> RetrieveFollowUps(int organisationId, Expression<Func<FollowUp, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        FollowUp RetrieveFollowUp(int organisationId, int followUpId);
        List<Course> RetrieveCourses(int organisationId, Expression<Func<Course, bool>> predicate);
        List<Qualification> RetrieveQualifications(int organisationId, Expression<Func<Qualification, bool>> predicate);
        List<Religion> RetrieveReligions(int organisationId, Expression<Func<Religion, bool>> predicate);
        List<CasteCategory> RetrieveCasteCategories(int organisationId, Expression<Func<CasteCategory, bool>> predicate);
        List<HowDidYouKnowAbout> RetrieveHowDidYouKnowAbouts(int organisationId, Expression<Func<HowDidYouKnowAbout, bool>> predicate);
        List<Occupation> RetrieveOccupations(int organisationId, Expression<Func<Occupation, bool>> predicate);
        List<Scheme> RetrieveSchemes(int organisationId, Expression<Func<Scheme, bool>> predicate);
        List<SchemeType> RetrieveSchemeTypes(int organisationId, Expression<Func<SchemeType, bool>> predicate);
        List<Sector> RetrieveSectors(int organisationId, Expression<Func<Sector, bool>> predicate);
        List<BatchTimePrefer> RetrieveBatchTimePrefers(int organisationId, Expression<Func<BatchTimePrefer, bool>> predicate);
        List<StudentType> RetrieveStudentTypes(int organisationId, Expression<Func<StudentType, bool>> predicate);
        List<EnquiryType> RetrieveEnquiryTypes(int organisationId, Expression<Func<EnquiryType, bool>> predicate);
        List<Enquiry> RetrieveEnquiries(int organisationId, Expression<Func<Enquiry, bool>> predicate);
        List<State> RetrieveStates(int organisationId, Expression<Func<State, bool>> predicate);
        List<District> RetrieveDistricts(int organisationId, Expression<Func<District, bool>> predicate);
        List<Taluka> RetrieveTalukas(int organisationId, Expression<Func<Taluka, bool>> predicate);
        List<Batch> RetrieveBatches(int organisationId, Expression<Func<Batch, bool>> predicate);
        List<SubSector> RetrieveSubSectors(int organisationId, Expression<Func<SubSector, bool>> predicate);
        List<Disability> RetrieveDisabilities(int organisationId, Expression<Func<Disability, bool>> predicate);
        List<AlternateIdType> RetrieveAlternateIdTypes(int organisationId, Expression<Func<AlternateIdType, bool>> predicate);
        PagedResult<Mobilization> RetrieveMobilizationBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<EnquirySearchField> RetrieveEnquiryBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        List<MobilizationType> RetrieveMobilizationTypes(int organisationId, Expression<Func<MobilizationType, bool>> predicate);
        PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate);
        Centre RetrieveCentre(int organisationId, int id);
        PagedResult<Counselling> RetrieveCounsellings(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Counselling RetrieveCounselling(int organisationId, int counsellingId, Expression<Func<Counselling, bool>> predicate);
        Counselling RetrieveCounselling(int organisationId, int id);
        Batch RetrieveBatch(int organisationId, int id);
        PagedResult<Batch> RetrieveBatches(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Batch RetrieveBatch(int organisationId, int batchId, Expression<Func<Batch, bool>> predicate);
        PagedResult<Admission> RetrieveAdmissions(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Admission RetrieveAdmission(int organisationId, int admissionId, Expression<Func<Admission, bool>> predicate);
        Admission RetrieveAdmission(int organisationId, int id);
        PagedResult<AdmissionSearchField> RetrieveAdmissionBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        //List<Event> RetrieveEvents(int organisationId, Expression<Func<Event, bool>> predicate);
        List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate);

        // Update
        //void UploadPhoto(int organisationId, int personnelId, byte[] photo);
        Personnel UpdatePersonnel(int organisationId, Personnel personnel);
        Enquiry UpdateEnquiry(int organisationId, Enquiry enquiry);
        Mobilization UpdateMobilization(int organisationId, Mobilization mobilization);
        ValidationResult<AreaOfInterest> UpdateAreaOfInterest(int organisationId, AreaOfInterest areaOfInterest);
        FollowUp UpdateFollowUp(int organisationId, FollowUp followUp);
        Centre UpdateCentre(int organisationId, Centre centre);
        Counselling UpdateCounselling(int organisationId, Counselling counselling);
        Batch UpdateBatch(int organisationId, Batch batch);
        Admission UpdateAdmission(int organisationId, Admission admission);

        //Delete
        void DeletePersonnel(int organisationId, int personnelId);

        void MarkAsReadFollowUp(int organisationId, int id);

    }
}
