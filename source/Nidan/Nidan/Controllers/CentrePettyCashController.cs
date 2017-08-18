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
    public class CentrePettyCashController : BaseController
    {
        public CentrePettyCashController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: CentrePettyCash
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CentrePettyCash/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new CentrePettyCashViewModel
            {
                CentrePettyCash = new CentrePettyCash(),
                Centres = new SelectList(centres, "CentreId", "Name")
            };

            return View(viewModel);
        }

        // POST: CentrePettyCash/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CentrePettyCashViewModel centrePettyCashViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = centrePettyCashViewModel.CentrePettyCash.CentreId;
            if (ModelState.IsValid)
            {
                centrePettyCashViewModel.CentrePettyCash = NidanBusinessService.CreateCentrePettyCash(organisationId, centreId, UserPersonnelId, centrePettyCashViewModel.CentrePettyCash);
                return RedirectToAction("Index");
            }
            centrePettyCashViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            return View(centrePettyCashViewModel);
        }

        // GET: CentrePettyCash/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var centrePettyCash = NidanBusinessService.RetrieveCentrePettyCash(organisationId, centreId, id.Value, e => true);

            if (centrePettyCash == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CentrePettyCashViewModel
            {
                CentrePettyCash = centrePettyCash,
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            return View(viewModel);
        }

        // POST: CentrePettyCash/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CentrePettyCashViewModel centrePettyCashViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                centrePettyCashViewModel.CentrePettyCash = NidanBusinessService.UpdateCentrePettyCash(organisationId, centreId, UserPersonnelId, centrePettyCashViewModel.CentrePettyCash);
                return RedirectToAction("Index");
            }
            var viewModel = new CentrePettyCashViewModel
            {
                CentrePettyCash = centrePettyCashViewModel.CentrePettyCash
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCentrePettyCashs(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }
    }
}