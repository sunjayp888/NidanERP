using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity.Dto;
using Microsoft.Ajax.Utilities;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Extensions;
using Nidan.Models;
using EventApproveState = Nidan.Business.Enum.EventApproveState;
using EventFunctionType = Nidan.Business.Enum.EventFunctionType;

namespace Nidan.Controllers
{
    public class EventController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public EventController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: Event
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Event/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var viewModel = new EventViewModel
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                Event = new Event()
                {
                    OrganisationId = UserOrganisationId,
                }
            };
            return View(viewModel);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                eventViewModel.Event.OrganisationId = UserOrganisationId;
                //eventViewModel.Event.CentreId = UserCentreId;
                eventViewModel.Event.CreatedDateTime = _today;
                eventViewModel.Event.CreatedBy = UserPersonnelId;
                eventViewModel.Event = NidanBusinessService.CreateEvent(UserOrganisationId, eventViewModel.Event);
                return RedirectToAction("Index");
            }
            eventViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId).ToList());
            return View(eventViewModel);
        }

        // GET: Event/Edit
        public ActionResult Edit(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventData = NidanBusinessService.RetrieveEvent(organisationId, id.Value, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId);
            var viewModel = new EventViewModel()
            {
                Event = eventData,
                Centres = new SelectList(centres, "CentreId", "Name"),
                //Brainstorming = brainstorming
            };
            return View(viewModel);
        }

        // POST: Event/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                eventViewModel.Event.OrganisationId = UserOrganisationId;
                eventViewModel.Event.CentreId = UserCentreId;
                eventViewModel.Event = NidanBusinessService.UpdateEvent(UserOrganisationId, eventViewModel.Event);
                return RedirectToAction("Index");
            }
            var viewModel = new EventViewModel
            {
                Event = eventViewModel.Event
            };
            return View(viewModel);
        }

        public ActionResult UpdateEventApproveState(int? id, bool approveState)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var eventData = NidanBusinessService.RetrieveEvent(organisationId, id.Value, e => true);
            if (eventData == null)
            {
                return HttpNotFound();
            }
            var result = approveState ? eventData.EventApproveStateId = (int)EventApproveState.Approved : eventData.EventApproveStateId = (int)EventApproveState.Declined;
            NidanBusinessService.UpdateEvent(organisationId, eventData);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(_nidanBusinessService.RetrieveEvents(UserOrganisationId, e => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult EventBrainStormingList(int eventId)
        {
            var organisationId = UserOrganisationId;
            var brainStormingList = _nidanBusinessService.RetrieveEventManagementGrids(organisationId, e => e.EventId == eventId && e.EventFunctionTypeId == (int)EventFunctionType.BrainStorming);
            if (brainStormingList.Items.Any())
                return this.JsonNet(brainStormingList);
            //var data = _nidanBusinessService.RetrieveBrainstormings(UserOrganisationId, e => true);
            return this.JsonNet(brainStormingList);
        }

        [HttpPost]
        public ActionResult BrainStormingQuestion()
        {
            var organisationId = UserOrganisationId;
            //var data = _nidanBusinessService.RetrieveBrainstormings(organisationId, e => true);
            return this.JsonNet(null);
        }


        //public ActionResult BrainStorm()
        //{
        //    return null;
        //}

        public PartialViewResult BrainStorm()
        {
            return PartialView("_BrainStorming");
        }
    }
}