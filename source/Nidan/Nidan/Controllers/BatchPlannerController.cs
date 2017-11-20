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
    public class BatchPlannerController : BaseController
    {
        // GET: BatchPlanner
        public BatchPlannerController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: BatchPlanner/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList();
            var viewModel = new BatchPlannerViewModel()
            {
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Fullname"),
                Centres = new SelectList(centres, "CentreId", "Name"),
                Rooms = new SelectList(rooms, "RoomId", "Description")
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: BatchPlanner/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchPlannerViewModel batchPlannerViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                batchPlannerViewModel.BatchPlanner.OrganisationId = organisationId;
                batchPlannerViewModel.BatchPlanner = NidanBusinessService.CreateBatchPlanner(organisationId, batchPlannerViewModel.BatchPlanner, batchPlannerViewModel.BatchPlannerDay);
                return RedirectToAction("Index");
            }
            batchPlannerViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            batchPlannerViewModel.Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(organisationId, e => true).ToList());
            batchPlannerViewModel.Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => true).ToList());
            return View(batchPlannerViewModel);
        }

        // GET: BatchPlanner/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var batchPlanner = NidanBusinessService.RetrieveBatchPlanner(organisationId, id.Value, e => true);
            var batchPlannerDay = NidanBusinessService.RetrieveBatchPlannerDays(organisationId, e => e.BatchPlannerId == batchPlanner.BatchPlannerId).Items.FirstOrDefault();
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList();
            if (batchPlanner == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchPlannerViewModel
            {
                BatchPlanner = batchPlanner,
                BatchPlannerDay = batchPlannerDay,
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Fullname"),
                Centres = new SelectList(centres, "CentreId", "Name"),
                Rooms = new SelectList(rooms, "RoomId", "Description")
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: BatchPlanner/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BatchPlannerViewModel batchPlannerViewModel)
        {
            if (ModelState.IsValid)
            {
                batchPlannerViewModel.BatchPlanner.OrganisationId = UserOrganisationId;
                batchPlannerViewModel.BatchPlanner = NidanBusinessService.UpdateBatchPlanner(UserOrganisationId, batchPlannerViewModel.BatchPlanner, batchPlannerViewModel.BatchPlannerDay);
                return RedirectToAction("Index");
            }
            var viewModel = new BatchPlannerViewModel
            {
                BatchPlanner = batchPlannerViewModel.BatchPlanner
            };
            return View(viewModel);
        }

        // GET: BatchPlanner/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var batchPlannerGrid = NidanBusinessService.RetrieveBatchPlannerGrids(organisationId, e => e.BatchPlannerId == id).Items.FirstOrDefault();
            var batchPlannerData = NidanBusinessService.RetrieveBatchPlanner(organisationId, id.Value, e => true);
            if (batchPlannerGrid == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchPlannerViewModel
            {
                BatchPlannerGrid = batchPlannerGrid,
                BatchPlanner = batchPlannerData
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            return this.JsonNet(NidanBusinessService.RetrieveBatchPlannerGrids(UserOrganisationId, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetClassRoom(int centreId)
        {
            var data = NidanBusinessService.RetrieveRooms(UserOrganisationId, centreId, e => e.CentreId == centreId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourse(int centreId)
        {
            var data = NidanBusinessService.RetrieveCentreCourses(UserOrganisationId, centreId, e => e.CentreId == centreId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetTrainer(int centreId)
        {
            var data = NidanBusinessService.RetrieveTrainers(UserOrganisationId, e => e.CentreId == centreId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourseData(int courseId)
        {
            var data = NidanBusinessService.RetrieveCourse(UserOrganisationId, courseId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetMonth(int centreId, int roomId, DateTime startDate, int numberOfCourseHours, int dailyBatchHours, int courseId, int numberOfWeekDays)
        {
            var data = NidanBusinessService.GetBatchPlannerDetail(UserOrganisationId, centreId, roomId, startDate, numberOfCourseHours, dailyBatchHours, courseId, numberOfWeekDays);
            return this.JsonNet(data);
        }
    }
}