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
    public class AdmissionController : BaseController
    {
        private ApplicationRoleManager _roleManager;

        public AdmissionController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Admission
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Admission/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var schemeTypes = NidanBusinessService.RetrieveSchemeTypes(organisationId, e => true);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var subSectors = NidanBusinessService.RetrieveSubSectors(organisationId, e => true);
            var disabilities = NidanBusinessService.RetrieveDisabilities(organisationId, e => true);
            var alternateIdTypes = NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var viewModel = new AdmissionViewModel
            {
                Admission = new Admission(),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                SchemeTypes = new SelectList(schemeTypes, "SchemeTypeId", "Name"),
                Batches = new SelectList(batches, "BatcheId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                SubSectors = new SelectList(subSectors, "SubSectorId", "Name"),
                Disabilities = new SelectList(disabilities, "DisabilityId", "Name"),
                AlternateIdTypes = new SelectList(alternateIdTypes, "AlternateIdTypeId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Admission/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission.OrganisationId = UserOrganisationId;
                admissionViewModel.Admission.CentreId = 1;
                admissionViewModel.Admission.EnquiryId = 7;
                admissionViewModel.Admission.AdmissionDate = DateTime.Now;
                admissionViewModel.Admission = NidanBusinessService.CreateAdmission(UserOrganisationId, admissionViewModel.Admission);
                return RedirectToAction("Index");
            }
            admissionViewModel.CasteCategories =
                new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            admissionViewModel.Religions =
                new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true).ToList());
            admissionViewModel.Talukas =
                new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            admissionViewModel.Districts =
                new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            admissionViewModel.States =
                new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            admissionViewModel.EducationalQualifications =
                new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            admissionViewModel.Courses =
               new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            admissionViewModel.Schemes =
               new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            admissionViewModel.SchemeTypes =
               new SelectList(NidanBusinessService.RetrieveSchemeTypes(organisationId, e => true).ToList());
            admissionViewModel.Batches =
               new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList());
            admissionViewModel.Sectors =
               new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            admissionViewModel.SubSectors =
               new SelectList(NidanBusinessService.RetrieveSubSectors(organisationId, e => true).ToList());
            admissionViewModel.Disabilities =
               new SelectList(NidanBusinessService.RetrieveDisabilities(organisationId, e => true).ToList());
            admissionViewModel.AlternateIdTypes =
               new SelectList(NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true).ToList());
            return View(admissionViewModel);
        }

        // GET: Admission/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var schemeTypes = NidanBusinessService.RetrieveSchemeTypes(organisationId, e => true);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var subSectors = NidanBusinessService.RetrieveSubSectors(organisationId, e => true);
            var disabilities = NidanBusinessService.RetrieveDisabilities(organisationId, e => true);
            var alternateIdTypes = NidanBusinessService.RetrieveAlternateIdTypes(organisationId, e => true);
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, id.Value);
            if (admission == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admission,
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                SchemeTypes = new SelectList(schemeTypes, "SchemeTypeId", "Name"),
                Batches = new SelectList(batches, "BatcheId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                SubSectors = new SelectList(subSectors, "SubSectorId", "Name"),
                Disabilities = new SelectList(disabilities, "DisabilityId", "Name"),
                AlternateIdTypes = new SelectList(alternateIdTypes, "AlternateIdTypeId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Admission/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdmissionViewModel admissionViewModel)
        {
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission.OrganisationId = UserOrganisationId;
                admissionViewModel.Admission.CentreId = 1;
                admissionViewModel.Admission.EnquiryId = 7;
                admissionViewModel.Admission.AdmissionDate = DateTime.Now;
                admissionViewModel.Admission = NidanBusinessService.UpdateAdmission(UserOrganisationId, admissionViewModel.Admission);
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admissionViewModel.Admission
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveAdmissions(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveAdmissionBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }
    }
}