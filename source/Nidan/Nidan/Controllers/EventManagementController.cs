using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class EventManagementController : BaseController
    {
        public EventManagementController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: EventManagement
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult Create(int eventId, List<EventManagement> eventBrainstormings)
        {
            //var organisationId = UserOrganisationId;
            //var centreId = UserCentreId;
            //foreach (var item in eventBrainstormings)
            //{
            //    item.CentreId = centreId;
            //    item.OrganisationId = organisationId;
            //}
           // var result = NidanBusinessService.CreateEventBrainstorming(organisationId, centreId, eventId, eventBrainstormings);
            return this.JsonNet(null);
        }
    }
}