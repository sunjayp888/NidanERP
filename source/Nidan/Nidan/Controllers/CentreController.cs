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

        [HttpPost]
        public ActionResult UnassignCentreCourse(int centreId, int courseId)
        {
            NidanBusinessService.DeleteCentreCourse(UserOrganisationId, centreId, courseId);
            return this.JsonNet("");
        }
    }
}