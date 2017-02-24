using Nidan.Business.Interfaces;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;
using System.Net;
using Nidan.Entity.Dto;
using Nidan.Extensions;

namespace Nidan.Controllers
{
    [Authorize]
    public class BatchController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public BatchController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Batch
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Batch/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var viewModel = new BatchViewModel
            {
                Batch = new Batch(),
              //OrganisationId = UserOrganisationId,
                Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name")
                
            };
            return View(viewModel);
        }

        // POST: Batch/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchViewModel batchViewModel)
        {
            var organisationId = UserOrganisationId;
            batchViewModel.Batch.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                batchViewModel.Batch.OrganisationId = UserOrganisationId;
                batchViewModel.Batch.CentreId = 1;
                batchViewModel.Batch.PersonnelId = UserPersonnelId;
                batchViewModel.Batch = NidanBusinessService.CreateBatch(UserOrganisationId, batchViewModel.Batch);
                return RedirectToAction("Index");
            }
            batchViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            batchViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            batchViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            return View(batchViewModel);
        }

        // GET: Batch/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var batch = NidanBusinessService.RetrieveBatch(UserOrganisationId, id.Value);
            if (batch == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchViewModel
            {
                Batch = batch,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(UserOrganisationId, e => true).ToList(), "SchemeId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name")

            };
            return View(viewModel);
        }

        // POST: Centre/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BatchViewModel batchViewModel)
        {
            if (ModelState.IsValid)
            {
                batchViewModel.Batch.OrganisationId = UserOrganisationId;
                batchViewModel.Batch.CentreId = 1;
                batchViewModel.Batch.PersonnelId = UserPersonnelId;
                batchViewModel.Batch = NidanBusinessService.UpdateBatch(UserOrganisationId, batchViewModel.Batch);
                return RedirectToAction("Index");
            }
            var viewModel = new BatchViewModel
            {
                Batch = batchViewModel.Batch
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveBatches(UserOrganisationId, orderBy, paging));
        }
    }
}