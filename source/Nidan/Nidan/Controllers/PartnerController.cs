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
    public class PartnerController : BaseController
    {
        public PartnerController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Partner
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Partner/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var viewModel = new PartnerViewModel
            {
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                Partner = new Partner
                {
                    OrganisationId = organisationId
                }
            };
            return View(viewModel);
        }

        // POST: Partner/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PartnerViewModel partnerViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                partnerViewModel.Partner.OrganisationId = organisationId;
                partnerViewModel.Partner = NidanBusinessService.CreatePartner(organisationId, partnerViewModel.Partner);
                return RedirectToAction("Index");
            }
            partnerViewModel.Talukas = new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            partnerViewModel.Districts = new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            partnerViewModel.States = new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            return View(partnerViewModel);
        }

        // GET: Partner/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var partner = NidanBusinessService.RetrievePartner(organisationId, id.Value, e => true);
            if (partner == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PartnerViewModel
            {
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                Partner = partner

            };
            return View(viewModel);
        }

        // POST: Partner/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerViewModel partnerViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                partnerViewModel.Partner.OrganisationId = organisationId;
                partnerViewModel.Partner = NidanBusinessService.UpdatePartner(organisationId, partnerViewModel.Partner);
                return RedirectToAction("Index");
            }
            var viewModel = new PartnerViewModel
            {
                Partner = partnerViewModel.Partner
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrievePartners(UserOrganisationId, e => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetTaluka(int districtId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveTalukas(UserOrganisationId, e => e.District.DistrictId == districtId).ToList());
        }

        [HttpPost]
        public ActionResult GetDistrict(int stateId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveDistricts(UserOrganisationId, e => e.State.StateId == stateId).ToList());
        }
    }
}