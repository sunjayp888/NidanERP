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
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var viewModel = new AdmissionViewModel
            {
                Admission = new Admission(),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
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
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, id.Value);
            if (admission == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = new Admission(),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
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
    }
}