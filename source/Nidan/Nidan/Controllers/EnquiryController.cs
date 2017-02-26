using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Nidan.Controllers
{
    [Authorize]
    public class EnquiryController : BaseController
    {
        private static string EnquiryName { get; set; }
        private ApplicationRoleManager _roleManager;

        public EnquiryController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Enquiry

        public ActionResult Index()
        {
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
            var mobilization = NidanBusinessService.RetrieveMobilization(organisationId, id.Value);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var enquiryTypes = NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);

            var viewModel = new EnquiryViewModel
            {
                Enquiry = new Enquiry(),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                // AreaOfInterests = new SelectList(areaOfInterests, "AreaOfInterestId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name")
            };
            return View(viewModel);
        }

        // POST: Enquiry/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnquiryViewModel enquiryViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                enquiryViewModel.Enquiry.OrganisationId = UserOrganisationId;
                enquiryViewModel.Enquiry.CentreId = UserCentreId;
                enquiryViewModel.Enquiry.EnquiryDate = DateTime.Now;
                enquiryViewModel.Enquiry = NidanBusinessService.CreateEnquiry(UserOrganisationId, enquiryViewModel.Enquiry);
                var counselling = new Counselling
                {
                    OrganisationId = UserOrganisationId,
                    EnquiryId = enquiryViewModel.Enquiry.EnquiryId,
                    CentreId = UserCentreId,
                    CourseOfferedId = enquiryViewModel.Counselling.CourseOfferedId,
                    FollowUpDate = enquiryViewModel.Counselling.FollowUpDate
                };
                NidanBusinessService.CreateCounselling(UserOrganisationId, counselling);
                return RedirectToAction("Index");
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
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name")
            };
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
                enquiryViewModel.Enquiry = NidanBusinessService.UpdateEnquiry(UserOrganisationId, enquiryViewModel.Enquiry);
                var counselling = new Counselling
                {
                    EnquiryId = enquiryViewModel.Enquiry.EnquiryId,
                    CentreId = UserCentreId,
                    OrganisationId = UserOrganisationId,
                    CourseOfferedId = enquiryViewModel.Counselling.CourseOfferedId,
                    FollowUpDate = enquiryViewModel.Counselling.FollowUpDate,
                    Remarks = enquiryViewModel.Counselling.Remarks,
                    RemarkByBranchManager = enquiryViewModel.Counselling.RemarkByBranchManager,
                };
                NidanBusinessService.UpdateCounselling(UserOrganisationId, counselling);
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
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveEnquiries(UserOrganisationId, e => e.EnquiryDate >= fromDate && e.EnquiryDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_roleManager != null)
        //        {
        //            _roleManager.Dispose();
        //            _roleManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
    }
}