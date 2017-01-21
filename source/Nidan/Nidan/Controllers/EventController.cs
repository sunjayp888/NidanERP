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

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            //return this.JsonNet(_businessService.RetrieveAbsenceTypes(UserOrganisationId, orderBy, paging));
            return this.JsonNet(_nidanBusinessService.RetrieveEvents(UserOrganisationId,e => true, orderBy, paging));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EventViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _businessService.UpdateEvent(model.Event);

        //    }
        //    return RedirectToAction("Index");
        //}
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

        //[HttpPost]
        //public ActionResult Create(EventViewModel events)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var data = _businessService.CreateEvent(events.Event);
        //        return RedirectToAction("Index");
        //    }
        //    var model = new EventViewModel();
        //    return View(model);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var eventData = _businessService.RetrieveEvent(id.Value);
        //    if (eventData == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var viewModel = new EventViewModel()
        //    {
        //        Event = eventData
        //    };
        //    return View(viewModel);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    _businessService.DeleteEvent(id);
        //    return RedirectToAction("Index");
        //}
    }
}