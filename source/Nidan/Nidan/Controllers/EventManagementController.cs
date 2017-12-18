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
        public ActionResult UpdateEventBrainStorming(int eventId, List<EventManagement> eventBrainstormings)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var result = NidanBusinessService.UpdateEventManagement(organisationId, centreId, eventId, eventBrainstormings);
            return this.JsonNet(result);
        }
    }
}