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
    public class ActivityTaskController : BaseController
    {
        public ActivityTaskController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: ActivityTask
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActivityTask/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var activityData = NidanBusinessService.RetrieveActivity(organisationId, id.Value, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == centreId);
            var assignTos = NidanBusinessService.RetrieveActivityAssignPersonnels(organisationId,centreId,activityData.ActivityAssigneeGroupId).Items.ToList();
            var startDate = activityData.StartDate;
            var endDate= activityData.EndDate;
            var numberOfDays = (endDate - startDate).TotalDays;
            var startTime = activityData.StartHour + ":" + activityData.StartMinute + " " + activityData.StartTimeSpan;
            var endTime = activityData.EndHour + ":" + activityData.EndMinute + " " + activityData.EndTimeSpan;
            var numberOfHours = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            var viewModel = new ActivityTaskViewModel()
            {
                NumberOfDays = numberOfDays,
                NumberOfHours = numberOfHours,
                Centres = new SelectList(centres, "CentreId", "Name"),
                AssignToList = new SelectList(assignTos, "PersonnelId", "FullName"),
                Activity = activityData,
                ActivityTask = new ActivityTask()
                {
                    Activity = activityData
                }
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: ActivityTask/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityTaskViewModel activityTaskViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                activityTaskViewModel.ActivityTask.OrganisationId = organisationId;
                activityTaskViewModel.ActivityTask.ActivityId = activityTaskViewModel.ActivityTask.Activity.ActivityId;
                activityTaskViewModel.ActivityTask = NidanBusinessService.CreateActivityTask(organisationId, personnelId, centreId, activityTaskViewModel.ActivityTask);
                return RedirectToAction("Create","ActivityTask",new {id=activityTaskViewModel.ActivityTask.ActivityId});
            }
            activityTaskViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).ToList());
            activityTaskViewModel.AssignToList = new SelectList(NidanBusinessService.RetrieveActivityAssignPersonnels(organisationId, centreId, activityTaskViewModel.Activity.ActivityAssigneeGroupId).Items.ToList());
            return View(activityTaskViewModel);
        }

        // GET: ActivityTask/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == centreId);
            var activityTask = NidanBusinessService.RetrieveActivityTask(organisationId, id.Value, e => true);
            if (activityTask == null)
            {
                return HttpNotFound();
            }
            var activityData = NidanBusinessService.RetrieveActivity(organisationId, activityTask.ActivityId, e => true);
            var assignTos = NidanBusinessService.RetrieveActivityAssignPersonnels(organisationId, centreId, activityData.ActivityAssigneeGroupId).Items.ToList();
            var startDate = activityData.StartDate;
            var endDate = activityData.EndDate;
            var numberOfDays = (endDate - startDate).TotalDays;
            var startTime = activityData.StartHour + ":" + activityData.StartMinute + " " + activityData.StartTimeSpan;
            var endTime = activityData.EndHour + ":" + activityData.EndMinute + " " + activityData.EndTimeSpan;
            var numberOfHours = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            var viewModel = new ActivityTaskViewModel
            {
                NumberOfDays = numberOfDays,
                NumberOfHours = numberOfHours,
                Centres = new SelectList(centres, "CentreId", "Name"),
                AssignToList = new SelectList(assignTos, "PersonnelId", "FullName"),
                ActivityTask = activityTask,
                Activity = activityData
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: ActivityTask/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityTaskViewModel activityTaskViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                activityTaskViewModel.ActivityTask.OrganisationId = organisationId;
                activityTaskViewModel.ActivityTask = NidanBusinessService.UpdateActivityTask(organisationId, activityTaskViewModel.ActivityTask);
                return RedirectToAction("Index");
            }
            var viewModel = new ActivityTaskViewModel
            {
                ActivityTask = activityTaskViewModel.ActivityTask
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveActivityTaskDataGrids(UserOrganisationId, e => isSuperAdmin || e.CentreId == UserCentreId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveActivityTaskBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveActivityTaskDataGrids(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.StartDate >= fromDate && e.StartDate <= toDate, orderBy, paging));
        }

        [HttpPost]
        public ActionResult ActivityTaskByActivityId(int activityId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveActivityTaskDataGrids(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.ActivityId == activityId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetPersonnel(int centreId,int activityAssigneeGroupId, Paging paging, List<OrderBy> orderBy)
        {
            var assignTos = NidanBusinessService.RetrieveActivityAssignPersonnels(UserOrganisationId, centreId, activityAssigneeGroupId).Items.ToList();
            return this.JsonNet(assignTos);
        }

        [HttpPost]
        public ActionResult Delete(int activityTaskId)
        {
            NidanBusinessService.DeleteActivityTask(UserOrganisationId, activityTaskId);
            return this.JsonNet("");
        }
        //AddTaskStatus
        //GET: ActivityTask
        public ActionResult AddTaskStatus(int? activityTaskId)
        {
            var organisationId = UserOrganisationId;
            var taskStates = NidanBusinessService.RetrieveTaskStates(organisationId, e => true).Items.ToList();
            return this.JsonNet(taskStates);
        }

        [HttpPost]
        public ActionResult GetTaskStates()
        {
            var data = NidanBusinessService.RetrieveTaskStates(UserOrganisationId, e => true).Items.ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CreateActivityTaskStatus(ActivityTaskState activityTaskState)
        {
            var organisationId = UserOrganisationId;
            //var centreId = UserCentreId;
            try
            {
                activityTaskState.OrganisationId = organisationId;
                //activityTaskState.CentreId = centreId;
                var data = NidanBusinessService.CreateActivityTaskState(organisationId, activityTaskState);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }

        }
        
        [HttpPost]
        public ActionResult ActivityTaskStatesByActivityTaskId(int activityTaskId, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveActivityTaskStates(UserOrganisationId, e => e.ActivityTaskId == activityTaskId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}