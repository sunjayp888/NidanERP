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
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public BatchController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
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
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var courseFeeBreakUp = NidanBusinessService.RetrieveCourseFeeBreakUps(organisationId, e => e.CentreId == UserCentreId);
            var viewModel = new BatchViewModel
            {
                Batch = new Batch(),
                BatchDay = new BatchDay(),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Name"),
                CourseFeeBreakUps = new SelectList(courseFeeBreakUp, "CourseFeeBreakUpId", "Name")
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
            batchViewModel.Batch.CreatedDate = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                batchViewModel.Batch.OrganisationId = organisationId;
                batchViewModel.Batch.CentreId = UserCentreId;
                //batchViewModel.Batch.BatchDayId = 1;
                batchViewModel.Batch = NidanBusinessService.CreateBatch(organisationId, batchViewModel.Batch, batchViewModel.BatchDay);
                //  NidanBusinessService.CreateBatchDay(organisationId, batchDay);
                // batchViewModel.Batch.BatchDayId = batchDay.BatchDayId;
                // NidanBusinessService.UpdateBatch(organisationId, batchViewModel.Batch);

                return RedirectToAction("Index");
            }
            batchViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            batchViewModel.Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(organisationId, e => true).ToList());
            batchViewModel.CourseFeeBreakUps = new SelectList(NidanBusinessService.RetrieveCourseFeeBreakUps(organisationId, e => e.CentreId == UserCentreId).ToList());
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
            var batchday = batch.BatchDays.FirstOrDefault();
            var viewModel = new BatchViewModel
            {
                Batch = batch,
                BatchDay = batchday,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(UserOrganisationId, e => true).ToList(), "TrainerId", "Name"),
                CourseFeeBreakUps = new SelectList(NidanBusinessService.RetrieveCourseFeeBreakUps(UserOrganisationId, e => e.CentreId == UserCentreId).ToList(), "CourseFeeBreakUpId", "Name")
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
                batchViewModel.Batch.CentreId = UserCentreId;
                batchViewModel.Batch = NidanBusinessService.UpdateBatch(UserOrganisationId, batchViewModel.Batch);
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
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveBatches(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, e => e.Sector.SectorId == sectorId).ToList();
            return this.JsonNet(data);
        }
    }
}