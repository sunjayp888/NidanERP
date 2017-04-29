using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;

namespace Nidan.Controllers
{
    [Authorize]
    public class EnquiryController : BaseController
    {
        private static string EnquiryName { get; set; }
        private ApplicationRoleManager _roleManager;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);

        public EnquiryController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Enquiry

        public ActionResult Index()
        {
            EnquiryViewModel model = new EnquiryViewModel();

            return View(new BaseViewModel());
        }

        // GET: Enquiry/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var occupations = NidanBusinessService.RetrieveOccupations(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            //var areaOfInterests = NidanBusinessService.RetrieveAreaOfInterests(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);

            var followUp = NidanBusinessService.RetrieveFollowUps(organisationId, e => e.MobilizationId == (id.Value == 0 ? -1 : id.Value)).Items.FirstOrDefault();
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var enquiryTypes = NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);
            var enquiryFromMobilization = id.Value != 0
                ? NidanBusinessService.CreateEnquiryFromMobilization(UserOrganisationId, UserCentreId, id.Value)
                : new Enquiry();


            var viewModel = new EnquiryViewModel
            {
                CreateEnquiryFromMobilizationFollowUpId = followUp?.FollowUpId ?? 0,
                MobilizationId = id.Value,
                Enquiry = enquiryFromMobilization,
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                // AreaOfInterests = new SelectList(areaOfInterests, "AreaOfInterestId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                SelectedCourseIds = new List<int> { },
            };

            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.PreferredMonthForJoiningList = new SelectList(viewModel.PreferredMonthForJoiningType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Enquiry/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnquiryViewModel enquiryViewModel)
        {
            var organisationId = UserOrganisationId;
            enquiryViewModel.Enquiry.StudentCode = "ABC";
            if (ModelState.IsValid)
            {
                if (enquiryViewModel.CreateEnquiryFromMobilizationFollowUpId != 0)
                {
                    //Close Mobilization 
                    var mobilization = NidanBusinessService.RetrieveMobilization(UserOrganisationId, enquiryViewModel.MobilizationId);
                    mobilization.Close = "Yes";
                    NidanBusinessService.UpdateMobilization(UserOrganisationId, mobilization);
                    NidanBusinessService.DeleteFollowUp(UserOrganisationId, enquiryViewModel.CreateEnquiryFromMobilizationFollowUpId);
                }
                enquiryViewModel.Enquiry.OrganisationId = UserOrganisationId;
                enquiryViewModel.Enquiry.CentreId = UserCentreId;
                enquiryViewModel.Enquiry.EnquiryDate = DateTime.UtcNow;
                enquiryViewModel.Enquiry.FollowUpDate = DateTime.UtcNow.AddDays(2);
                enquiryViewModel.Enquiry.Close = "No";
                enquiryViewModel.Enquiry.EnquiryStatus = "Enquiry";
                enquiryViewModel.Enquiry = NidanBusinessService.CreateEnquiry(UserOrganisationId, UserPersonnelId, enquiryViewModel.Enquiry, enquiryViewModel.SelectedCourseIds);
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", new { id = enquiryViewModel.Enquiry.EnquiryId });
            }
            enquiryViewModel.EducationalQualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            enquiryViewModel.Occupations = new SelectList(NidanBusinessService.RetrieveOccupations(organisationId, e => true).ToList());
            enquiryViewModel.Religions = new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true).ToList());
            enquiryViewModel.CasteCategories = new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            //enquiryViewModel.AreaOfInterests = new SelectList(NidanBusinessService.RetrieveAreaOfInterests(organisationId, e => true).ToList());
            enquiryViewModel.HowDidYouKnowAbouts = new SelectList(NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true).ToList());
            enquiryViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            enquiryViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            enquiryViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            enquiryViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList());
            enquiryViewModel.StudentTypes = new SelectList(NidanBusinessService.RetrieveStudentTypes(organisationId, e => true).ToList());
            enquiryViewModel.EnquiryTypes = new SelectList(NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true).ToList());
            enquiryViewModel.Talukas =
                new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            enquiryViewModel.Districts =
                new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            enquiryViewModel.States =
                new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            return View(enquiryViewModel);
        }

        // GET: Enquiry/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var occupations = NidanBusinessService.RetrieveOccupations(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            //var areaOfInterests = NidanBusinessService.RetrieveAreaOfInterests(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var enquiry = NidanBusinessService.RetrieveEnquiry(UserOrganisationId, id.Value);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var enquiryTypes = NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);

            if (enquiry == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EnquiryViewModel
            {
                Enquiry = enquiry,
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                // AreaOfInterests = new SelectList(areaOfInterests, "AreaOfInterestId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                SelectedCourseIds = enquiry?.EnquiryCourses.Select(e => e.CourseId).ToList()
            };
            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.PreferredMonthForJoiningList = new SelectList(viewModel.PreferredMonthForJoiningType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Enquiry/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnquiryViewModel enquiryViewModel)
        {
            if (ModelState.IsValid)
            {
                enquiryViewModel.Enquiry.OrganisationId = UserOrganisationId;
                enquiryViewModel.Enquiry.CentreId = UserCentreId;
                enquiryViewModel.Enquiry.Close = "No";
                //enquiryViewModel.Enquiry.FollowUpDate = DateTime.UtcNow.AddDays(2);
                enquiryViewModel.Enquiry = NidanBusinessService.UpdateEnquiry(UserOrganisationId, enquiryViewModel.Enquiry, enquiryViewModel.SelectedCourseIds);

            }
            var viewModel = new EnquiryViewModel
            {
                Enquiry = enquiryViewModel.Enquiry
            };
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Registered == false && p.Close != "Yes", orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword,
                p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveEnquiries(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.EnquiryDate >= fromDate && e.EnquiryDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, e => e.SectorId == sectorId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetSector(int schemeId)
        {
            var data = NidanBusinessService.RetrieveSectors(UserOrganisationId, e => e.Scheme.SchemeId == schemeId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetTaluka(int districtId)
        {
            var data = NidanBusinessService.RetrieveTalukas(UserOrganisationId, e => e.District.DistrictId == districtId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetDistrict(int stateId)
        {
            var data = NidanBusinessService.RetrieveDistricts(UserOrganisationId, e => e.State.StateId == stateId).ToList();
            return this.JsonNet(data);
        }
    }
}