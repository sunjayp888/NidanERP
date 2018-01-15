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
    public class ActivityAssigneeGroupController : BaseController
    {
        public ActivityAssigneeGroupController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: ActivityAssigneeGroup
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActivityAssigneeGroup/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var viewModel = new ActivityAssigneeGroupViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                ActivityAssigneeGroup = new ActivityAssigneeGroup()
            };
            return View(viewModel);
        }

        // POST: ActivityAssigneeGroup/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityAssigneeGroupViewModel activityAssigneeGroupViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                activityAssigneeGroupViewModel.ActivityAssigneeGroup.OrganisationId = organisationId;
                activityAssigneeGroupViewModel.ActivityAssigneeGroup = NidanBusinessService.CreateActivityAssigneeGroup(organisationId, activityAssigneeGroupViewModel.ActivityAssigneeGroup);
                return RedirectToAction("Edit", "ActivityAssigneeGroup", new { id = activityAssigneeGroupViewModel.ActivityAssigneeGroup.ActivityAssigneeGroupId });
            }
            activityAssigneeGroupViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).ToList());
            return View(activityAssigneeGroupViewModel);
        }

        // GET: ActivityAssigneeGroup/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var activityAssigneeGroup = NidanBusinessService.RetrieveActivityAssigneeGroup(organisationId, id.Value, e => true);
            if (activityAssigneeGroup == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ActivityAssigneeGroupViewModel
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                ActivityAssigneeGroup = activityAssigneeGroup
            };
            return View(viewModel);
        }

        // POST: ActivityAssigneeGroup/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityAssigneeGroupViewModel activityAssigneeGroupViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                activityAssigneeGroupViewModel.ActivityAssigneeGroup.OrganisationId = organisationId;
                activityAssigneeGroupViewModel.ActivityAssigneeGroup = NidanBusinessService.UpdateActivityAssigneeGroup(organisationId, activityAssigneeGroupViewModel.ActivityAssigneeGroup);
                return RedirectToAction("Index");
            }
            var viewModel = new ActivityAssigneeGroupViewModel
            {
                ActivityAssigneeGroup = activityAssigneeGroupViewModel.ActivityAssigneeGroup
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveActivityAssigneeGroups(UserOrganisationId, e => isSuperAdmin || e.CentreId == UserCentreId, orderBy, paging));
        }

        public ActionResult AssignPersonnel(int centreId, int activityAssigneeGroupId, int personnelId)
        {
            var data = NidanBusinessService.CreateActivityAssignPersonnel(UserOrganisationId, centreId, activityAssigneeGroupId, personnelId);
            return this.JsonNet(data);
        }

        public ActionResult UnassignedPersonnels(int centreId, int activityAssigneeGroupId)
        {
            var data = NidanBusinessService.RetrieveUnassignedPersonnels(UserOrganisationId, centreId, activityAssigneeGroupId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult UnassignedActivityAsignGroupPersonnel(int centreId, int activityAssigneeGroupId,int personnelId)
        {
            NidanBusinessService.DeleteActivityAssignPersonnel(UserOrganisationId, centreId, activityAssigneeGroupId, personnelId);
            return this.JsonNet("");
        }

        public ActionResult ActivityAssignPersonnel(int centreId, int activityAssigneeGroupId)
        {
            var data = NidanBusinessService.RetrieveActivityAssignPersonnels(UserOrganisationId, centreId, activityAssigneeGroupId);
            return this.JsonNet(data);
        }
    }
}