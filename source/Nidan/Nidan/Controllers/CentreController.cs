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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Centre/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var viewModel = new CentreViewModel
            {
                Centre = new Centre
                {
                    OrganisationId = UserOrganisationId
                }
            };
            return View(viewModel);
        }

        // POST: Centre/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CentreViewModel centreViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                centreViewModel.Centre.OrganisationId = UserOrganisationId;
                centreViewModel.Centre = NidanBusinessService.CreateCentre(UserOrganisationId, centreViewModel.Centre);
                return RedirectToAction("Index");
            }
            return View(centreViewModel);
        }

        // GET: Centre/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, id.Value);
            if (centre == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CentreViewModel
            {
                Centre = centre

            };
            return View(viewModel);
        }

        // POST: Centre/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CentreViewModel centreViewModel)
        {
            if (ModelState.IsValid)
            {
                centreViewModel.Centre.OrganisationId = UserOrganisationId;
                centreViewModel.Centre = NidanBusinessService.UpdateCentre(UserOrganisationId, centreViewModel.Centre);
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
            return this.JsonNet(NidanBusinessService.RetrieveCentreCourses(UserOrganisationId, centreId));
        }

        public ActionResult AssignCentreCourseInstallment(int centreId, int courseInstallmentId)
        {
            return this.JsonNet(NidanBusinessService.CreateCentreCourseInstallment(UserOrganisationId, centreId, courseInstallmentId));
        }

        public ActionResult UnassignedCentreCourseInstallments(int centreId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveUnassignedCentreCourseInstallments(UserOrganisationId, centreId));
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
            return this.JsonNet(NidanBusinessService.RetrieveUnassignedCentreSchemes(UserOrganisationId, centreId));
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
    }
}