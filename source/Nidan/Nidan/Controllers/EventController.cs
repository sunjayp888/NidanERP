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

namespace Nidan.Controllers
{
    public class EventController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public EventController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: Event
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Event/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var viewModel = new EventViewModel
            {
                Event = new Event()
                {
                    OrganisationId = UserOrganisationId,
                },
            };
            return View(viewModel);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                eventViewModel.Event.OrganisationId = UserOrganisationId;
                eventViewModel.Event.CentreId = UserCentreId;
                eventViewModel.Event= NidanBusinessService.CreateEvent(UserOrganisationId, eventViewModel.Event);
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        // GET: Event/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventData = NidanBusinessService.RetrieveEvent(UserOrganisationId, id.Value, e => true);
            var viewModel = new EventViewModel()
            {
                Event = eventData
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

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(_nidanBusinessService.RetrieveEvents(UserOrganisationId, e => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult QuestionList(int eventFunctionId,Paging paging, List<OrderBy> orderBy )
        {
            return this.JsonNet(_nidanBusinessService.RetrieveQuestions(UserOrganisationId, e => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult BrainStorm()
        {
            return null;
        }
    }
}