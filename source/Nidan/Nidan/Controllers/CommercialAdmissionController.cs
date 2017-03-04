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
    public class CommercialAdmissionController : BaseController
    {
        private ApplicationRoleManager _roleManager;

        public CommercialAdmissionController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: CommercialAdmission
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CommercialAdmission/Create
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
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var viewModel = new CommercialAdmissionViewModel
            {
                CommercialAdmission = new CommercialAdmission(),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: CommercialAdmission/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommercialAdmissionViewModel commercialAdmissionViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                commercialAdmissionViewModel.CommercialAdmission.OrganisationId = UserOrganisationId;
                commercialAdmissionViewModel.CommercialAdmission.CentreId = 1;
                commercialAdmissionViewModel.CommercialAdmission.EnquiryId = 7;
                commercialAdmissionViewModel.CommercialAdmission.AdmissionDate = DateTime.Now;
                commercialAdmissionViewModel.CommercialAdmission =
                    NidanBusinessService.CreateCommercialAdmission(UserOrganisationId,
                        commercialAdmissionViewModel.CommercialAdmission);
                return RedirectToAction("Index");
            }
            commercialAdmissionViewModel.CasteCategories =
                new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            commercialAdmissionViewModel.Religions =
                new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true).ToList());
            commercialAdmissionViewModel.Talukas =
                new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            commercialAdmissionViewModel.Districts =
                new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            commercialAdmissionViewModel.States =
                new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            commercialAdmissionViewModel.EducationalQualifications =
                new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            commercialAdmissionViewModel.Courses =
                new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            commercialAdmissionViewModel.Sectors =
                new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            commercialAdmissionViewModel.StudentTypes =
                new SelectList(NidanBusinessService.RetrieveStudentTypes(organisationId, e => true).ToList());
            return View(commercialAdmissionViewModel);
        }

        // GET: CommercialAdmission/Edit/{id}
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
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var commercialAdmission= NidanBusinessService.RetrieveCommercialAdmission(organisationId, id.Value);
            if (commercialAdmission == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CommercialAdmissionViewModel
            {
                CommercialAdmission = commercialAdmission,
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: CommercialAdmission/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommercialAdmissionViewModel commercialAdmissionViewModel)
        {
            if (ModelState.IsValid)
            {
                commercialAdmissionViewModel.CommercialAdmission.OrganisationId = UserOrganisationId;
                commercialAdmissionViewModel.CommercialAdmission.CentreId = 1;
                commercialAdmissionViewModel.CommercialAdmission.EnquiryId = 7;
                commercialAdmissionViewModel.CommercialAdmission.AdmissionDate = DateTime.Now;
                commercialAdmissionViewModel.CommercialAdmission = NidanBusinessService.UpdateCommercialAdmission(UserOrganisationId, commercialAdmissionViewModel.CommercialAdmission);
            }
            var viewModel = new CommercialAdmissionViewModel
            {
                CommercialAdmission = commercialAdmissionViewModel.CommercialAdmission
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCommercialAdmissions(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCommercialAdmissionBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }
    }
}