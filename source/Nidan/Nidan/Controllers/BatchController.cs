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
    [Authorize]
    public class BatchController : BaseController
    {
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public BatchController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
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
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var courseInstallments = NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var viewModel = new BatchViewModel()
            {
                Batch = new Batch(),
                BatchDay = new BatchDay(),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Name"),
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                SelectedTrainerIds = new List<int> { }
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
            batchViewModel.Batch.CourseInstallment.Name = "Test";
            batchViewModel.Batch.Course.Name = "Test";
            if (ModelState.IsValid)
            {
                batchViewModel.Batch.OrganisationId = organisationId;
                batchViewModel.Batch.CentreId = UserCentreId;

                batchViewModel.Batch = NidanBusinessService.CreateBatch(organisationId, batchViewModel.Batch, batchViewModel.BatchDay);
                return RedirectToAction("Index");
            }
            batchViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            batchViewModel.Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(organisationId, e => true).ToList());
            batchViewModel.CourseInstallments = new SelectList(NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true).ToList());
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
            var batchDay= NidanBusinessService.RetrieveBatchDay(UserOrganisationId, id.Value);
            if (batch == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchViewModel
            {
                Batch = batch,
                BatchDay = batchDay,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(UserOrganisationId, e => true).ToList(), "TrainerId", "Name"),
                CourseInstallments = new SelectList(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, e => true).ToList(), "CourseInstallmentId", "Name"),
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
                batchViewModel.BatchDay = NidanBusinessService.UpdateBatchDay(UserOrganisationId, batchViewModel.BatchDay);
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
        public ActionResult GetCourse(int courseInstallmentId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCourseInstallment(UserOrganisationId, courseInstallmentId));
        }

    }
}