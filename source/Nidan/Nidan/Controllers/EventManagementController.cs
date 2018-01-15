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
            var personnelId = UserPersonnelId;
            var result = NidanBusinessService.UpdateEventManagement(organisationId, centreId, personnelId, eventId, eventBrainstormings);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult UpdateEventPlanning(int eventId, List<EventManagement> eventPlannings)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var result = NidanBusinessService.UpdateEventManagement(organisationId, centreId, personnelId, eventId, eventPlannings);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult UpdateEventDay(int eventId, List<EventManagement> eventDays)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var result = NidanBusinessService.UpdateEventManagement(organisationId, centreId, personnelId, eventId, eventDays);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult UpdatePostEvent(int eventId, List<EventManagement> postEvents)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var result = NidanBusinessService.UpdateEventManagement(organisationId, centreId, personnelId, eventId, postEvents);
            return this.JsonNet(result);
        }
    }
}