using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nidan.Business.Dto;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class ActivityController : BaseController
    {
        public ActivityController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Activity
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Activity
        public ActionResult ActivityTaskList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(new ActivityViewModel() { ActivityId = id.Value });
        }

        // GET: Activity/Create
        //[Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var activityAssigneeGroups = NidanBusinessService.RetrieveActivityAssigneeGroups(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).Items.ToList();
            var projects = NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == UserCentreId).Items.ToList();
            var activityTypes = NidanBusinessService.RetrieveActivityTypes(organisationId, e => true).Items.ToList();
            var viewModel = new ActivityViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                ActivityAssigneeGroups = new SelectList(activityAssigneeGroups, "ActivityAssigneeGroupId", "Name"),
                Projects = new SelectList(projects, "ProjectId", "Name"),
                ActivityTypes = new SelectList(activityTypes, "ActivityTypeId", "Name"),
                Activity = new Activity()
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Activity/Create
        //[Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityViewModel activityViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                activityViewModel.Activity.OrganisationId = organisationId;
                activityViewModel.Activity = NidanBusinessService.CreateActivity(organisationId, personnelId, centreId, activityViewModel.Activity);
                return RedirectToAction("Index");
            }
            activityViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).ToList());
            activityViewModel.ActivityAssigneeGroups = new SelectList(NidanBusinessService.RetrieveActivityAssigneeGroups(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).Items.ToList());
            activityViewModel.Projects = new SelectList(NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == UserCentreId).Items.ToList());
            activityViewModel.ActivityTypes = new SelectList(NidanBusinessService.RetrieveActivityTypes(organisationId, e => true).Items.ToList());
            return View(activityViewModel);
        }

        // GET: Activity/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var activityAssigneeGroups = NidanBusinessService.RetrieveActivityAssigneeGroups(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).Items.ToList();
            var projects = NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == UserCentreId).Items.ToList();
            var activityTypes = NidanBusinessService.RetrieveActivityTypes(organisationId, e => true).Items.ToList();
            var taskStates = NidanBusinessService.RetrieveTaskStates(organisationId, e => true).Items.ToList();
            var activity = NidanBusinessService.RetrieveActivity(organisationId, id.Value, e => true);
            if (activity == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ActivityViewModel
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                ActivityAssigneeGroups = new SelectList(activityAssigneeGroups, "ActivityAssigneeGroupId", "Name"),
                Projects = new SelectList(projects, "ProjectId", "Name"),
                TaskStates = new SelectList(taskStates, "TaskStateId", "Name"),
                ActivityTypes = new SelectList(activityTypes, "ActivityTypeId", "Name"),
                Activity = activity
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Activity/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityViewModel activityViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                activityViewModel.Activity.OrganisationId = organisationId;
                activityViewModel.Activity = NidanBusinessService.UpdateActivity(organisationId, activityViewModel.Activity);
                return RedirectToAction("Index");
            }
            var viewModel = new ActivityViewModel
            {
                Activity = activityViewModel.Activity
            };
            return View(viewModel);
        }

        public ActionResult ViewActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var activity = NidanBusinessService.RetrieveActivityDataGrids(organisationId, e => e.ActivityId == id.Value).Items.FirstOrDefault();
            if (activity == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ActivityTaskViewModel
            {
                ActivityDataGrid = activity
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var activityIds = NidanBusinessService.RetrieveActivityTaskDataGrids(organisationId, e => isSuperAdmin || e.AssignTo == UserPersonnelId, orderBy, paging).Items.Select(e=>e.ActivityId);
            var data = NidanBusinessService.RetrieveActivityDataGrids(UserOrganisationId, e => isSuperAdmin || activityIds.Contains(e.ActivityId) || e.CreatedBy==UserPersonnelId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var activityIds = NidanBusinessService.RetrieveActivityTaskDataGrids(organisationId, e => isSuperAdmin || e.AssignTo == UserPersonnelId, orderBy, paging).Items.Select(e => e.ActivityId);
            var data = NidanBusinessService.RetrieveActivityBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || activityIds.Contains(p.ActivityId) || p.CreatedBy == UserPersonnelId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var activityIds = NidanBusinessService.RetrieveActivityTaskDataGrids(organisationId, e => isSuperAdmin || e.AssignTo == UserPersonnelId, orderBy, paging).Items.Select(e => e.ActivityId);
            return this.JsonNet(NidanBusinessService.RetrieveActivityDataGrids(UserOrganisationId, e => (isSuperAdmin || activityIds.Contains(e.ActivityId) || e.CreatedBy == UserPersonnelId) && e.StartDate >= fromDate && e.StartDate <= toDate, orderBy, paging));
        }
    }
}