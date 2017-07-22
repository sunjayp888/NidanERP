using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class EventBrainstormingController : BaseController
    {
        public EventBrainstormingController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }

        // GET: EventBrainstorming
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult Create(EventBrainstormingViewModel eventBrainstormingViewModel, List<Brainstorming> brainstorming, int eventId)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var eventData = NidanBusinessService.RetrieveEvent(organisationId, eventId, e => true);
            foreach (var item in brainstorming)
            {
                var eventbrainstorming = new EventBrainstorming()
                {
                    EventId = eventData.EventId,
                    BrainstormingId = item.BrainstormingId,
                };
                var result = NidanBusinessService.CreateEventBrainstorming(organisationId, centreId, eventbrainstorming);
            }

            return null;

        }
    }
}