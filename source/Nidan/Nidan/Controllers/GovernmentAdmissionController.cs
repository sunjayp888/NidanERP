using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class GovernmentAdmissionController : BaseController
    {
        private ApplicationRoleManager _roleManager;

        public GovernmentAdmissionController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: GovernmentAdmission
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: GovernmentAdmission/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var subSectors = NidanBusinessService.RetrieveSubSectors(organisationId, e => true);
            var disabilities = NidanBusinessService.RetrieveDisabilities(organisationId, e => true);
            var alternateIdTypes = NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var viewModel = new GovernmentAdmissionViewModel
            {
                GovernmentAdmission = new GovernmentAdmission(),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                SubSectors = new SelectList(subSectors, "SubSectorId", "Name"),
                Disabilities = new SelectList(disabilities, "DisabilityId", "Name"),
                AlternateIdTypes = new SelectList(alternateIdTypes, "AlternateIdTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: GovernmentAdmission/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GovernmentAdmissionViewModel governmentAdmissionViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                governmentAdmissionViewModel.GovernmentAdmission.OrganisationId = UserOrganisationId;
                governmentAdmissionViewModel.GovernmentAdmission.CentreId = 1;
                governmentAdmissionViewModel.GovernmentAdmission.EnquiryId = 7;
                governmentAdmissionViewModel.GovernmentAdmission.AdmissionDate = DateTime.Now;
                governmentAdmissionViewModel.GovernmentAdmission =
                    NidanBusinessService.CreateGovernmentAdmission(UserOrganisationId,
                        governmentAdmissionViewModel.GovernmentAdmission);
                return RedirectToAction("Index");
            }
            governmentAdmissionViewModel.CasteCategories =
                new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            governmentAdmissionViewModel.Religions =
                new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true).ToList());
            governmentAdmissionViewModel.EducationalQualifications =
                new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            governmentAdmissionViewModel.Courses =
                new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            governmentAdmissionViewModel.Schemes =
                new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            governmentAdmissionViewModel.Sectors =
                new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            governmentAdmissionViewModel.SubSectors =
                new SelectList(NidanBusinessService.RetrieveSubSectors(organisationId, e => true).ToList());
            governmentAdmissionViewModel.Disabilities =
                new SelectList(NidanBusinessService.RetrieveDisabilities(organisationId, e => true).ToList());
            governmentAdmissionViewModel.AlternateIdTypes =
                new SelectList(NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true).ToList());
            governmentAdmissionViewModel.HowDidYouKnowAbouts =
                new SelectList(NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true).ToList());
            return View(governmentAdmissionViewModel);
        }

        // GET: GovernmentAdmission/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var subSectors = NidanBusinessService.RetrieveSubSectors(organisationId, e => true);
            var disabilities = NidanBusinessService.RetrieveDisabilities(organisationId, e => true);
            var alternateIdTypes = NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var governmentAdmission = NidanBusinessService.RetrieveGovernmentAdmission(organisationId, id.Value);
            if (governmentAdmission == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GovernmentAdmissionViewModel
            {
                GovernmentAdmission = governmentAdmission,
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                SubSectors = new SelectList(subSectors, "SubSectorId", "Name"),
                Disabilities = new SelectList(disabilities, "DisabilityId", "Name"),
                AlternateIdTypes = new SelectList(alternateIdTypes, "AlternateIdTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: GovernmentAdmission/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GovernmentAdmissionViewModel governmentAdmissionViewModel)
        {
            if (ModelState.IsValid)
            {
                governmentAdmissionViewModel.GovernmentAdmission.OrganisationId = UserOrganisationId;
                governmentAdmissionViewModel.GovernmentAdmission.CentreId = 1;
                governmentAdmissionViewModel.GovernmentAdmission.EnquiryId = 7;
                governmentAdmissionViewModel.GovernmentAdmission.AdmissionDate = DateTime.Now;
                governmentAdmissionViewModel.GovernmentAdmission = NidanBusinessService.UpdateGovernmentAdmission(UserOrganisationId, governmentAdmissionViewModel.GovernmentAdmission);
            }
            var viewModel = new GovernmentAdmissionViewModel
            {
                GovernmentAdmission = governmentAdmissionViewModel.GovernmentAdmission
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveGovernmentAdmissions(UserOrganisationId, orderBy, paging));
        }

    }
}