using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Nidan.Controllers
{
    public class CentreController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public CentreController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Centre
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Centre/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var viewModel = new CentreViewModel
            {
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                Centre = new Centre
                {
                    OrganisationId = organisationId
                }
            };
            return View(viewModel);
        }

        // POST: Centre/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CentreViewModel centreViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                centreViewModel.Centre.OrganisationId = organisationId;
                centreViewModel.Centre = NidanBusinessService.CreateCentre(organisationId, centreViewModel.Centre);
                return RedirectToAction("Index");
            }
            centreViewModel.Talukas = new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            centreViewModel.Districts = new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            centreViewModel.States = new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            return View(centreViewModel);
        }

        // GET: Centre/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var centre = NidanBusinessService.RetrieveCentre(organisationId, id.Value);
            if (centre == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CentreViewModel
            {
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                Centre = centre

            };
            return View(viewModel);
        }

        // POST: Centre/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CentreViewModel centreViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                centreViewModel.Centre.OrganisationId = organisationId;
                centreViewModel.Centre = NidanBusinessService.UpdateCentre(organisationId, centreViewModel.Centre);
                return RedirectToAction("Index");
            }
            var viewModel = new CentreViewModel
            {
                Centre = centreViewModel.Centre
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCentres(UserOrganisationId, orderBy, paging));
        }

        public ActionResult AssignCentreCourse(int centreId, int courseId)
        {
            return this.JsonNet(NidanBusinessService.CreateCentreCourse(UserOrganisationId, centreId, courseId));
        }

        public ActionResult UnassignedCentreCourses(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveUnassignedCentreCourses(UserOrganisationId, centreId));
        }

        public ActionResult CentreCourses(int centreId)
        {
            var data = NidanBusinessService.RetrieveCentreCourses(UserOrganisationId, centreId);
            return this.JsonNet(data);
        }

        public ActionResult AssignCentreCourseInstallment(int centreId, int courseInstallmentId)
        {
            return this.JsonNet(NidanBusinessService.CreateCentreCourseInstallment(UserOrganisationId, centreId, courseInstallmentId));
        }

        public ActionResult UnassignedCentreCourseInstallments(int centreId)
        {
            var data = NidanBusinessService.RetrieveUnassignedCentreCourseInstallments(UserOrganisationId, centreId);
            return this.JsonNet(data);
        }

        public ActionResult CentreCourseInstallments(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCentreCourseInstallments(UserOrganisationId, centreId));
        }

        public ActionResult AssignCentreScheme(int centreId, int schemeId)
        {
            return this.JsonNet(NidanBusinessService.CreateCentreScheme(UserOrganisationId, centreId, schemeId));
        }

        public ActionResult UnassignedCentreSchemes(int centreId)
        {
            var data = NidanBusinessService.RetrieveUnassignedCentreSchemes(UserOrganisationId, centreId);
            return this.JsonNet(data);
        }

        public ActionResult CentreSchemes(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCentreSchemes(UserOrganisationId, centreId));
        }

        public ActionResult AssignCentreSector(int centreId, int sectorId)
        {
            return this.JsonNet(NidanBusinessService.CreateCentreSector(UserOrganisationId, centreId, sectorId));
        }

        public ActionResult UnassignedCentreSectors(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveUnassignedCentreSectors(UserOrganisationId, centreId));
        }

        public ActionResult CentreSectors(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCentreSectors(UserOrganisationId, centreId));
        }

        [HttpPost]
        public ActionResult GetCourseInstallment(int courseId)
        {
            var data = NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, e => e.CourseId==courseId).ToList();
            return this.JsonNet(data);
        }
        
        [HttpPost]
        public ActionResult UnassignCentreCourse(int centreId, int courseId)
        {
            NidanBusinessService.DeleteCentreCourse(UserOrganisationId, centreId, courseId);
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult UnassignCentreScheme(int centreId, int schemeId)
        {
            NidanBusinessService.DeleteCentreScheme(UserOrganisationId, centreId, schemeId);
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult UnassignCentreSector(int centreId, int sectorId)
        {
            NidanBusinessService.DeleteCentreSector(UserOrganisationId, centreId, sectorId);
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult UnassignCentreCourseInstallment(int centreId, int courseInstallmentId)
        {
            NidanBusinessService.DeleteCentreCourseInstallment(UserOrganisationId, centreId, courseInstallmentId);
            return this.JsonNet("");
        }
    }
}