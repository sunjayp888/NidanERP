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
    public class FixAssetController : BaseController
    {
        public FixAssetController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: FixAsset
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Batch/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == centreId);
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = new FixAsset(),
                Rooms = new SelectList(rooms, "RoomId", "Description")
            };

            return View(viewModel);
        }

        // POST: Batch/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FixAssetViewModel fixAssetViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                fixAssetViewModel.FixAsset.OrganisationId = organisationId;
                fixAssetViewModel.FixAsset.CentreId = UserCentreId;
                fixAssetViewModel.FixAsset = NidanBusinessService.CreateFixAsset(organisationId, fixAssetViewModel.FixAsset);
                return RedirectToAction("Index");
            }
            fixAssetViewModel.Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => true).ToList());
            return View(fixAssetViewModel);
        }

        // GET: FixAsset/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fixAsset = NidanBusinessService.RetrieveFixAsset(organisationId, e=>e.FixAssetId==id.Value);
            if (fixAsset == null)
            {
                return HttpNotFound();
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = fixAsset,
                Rooms = new SelectList(NidanBusinessService.RetrieveRooms(UserOrganisationId, e => e.CentreId == UserCentreId).ToList(), "RoomId", "Description"),
            };
            return View(viewModel);
        }

        // POST: FixAsset/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FixAssetViewModel fixAssetViewModel)
        {
            if (ModelState.IsValid)
            {
                fixAssetViewModel.FixAsset.OrganisationId = UserOrganisationId;
                fixAssetViewModel.FixAsset.CentreId = UserCentreId;
                fixAssetViewModel.FixAsset = NidanBusinessService.UpdateFixAsset(UserOrganisationId, fixAssetViewModel.FixAsset);
                return RedirectToAction("Index");
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = fixAssetViewModel.FixAsset
            };
            return View(viewModel);
        }

        // GET: FixAsset/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var fixAssetDataGrid = NidanBusinessService.RetrieveFixAsset(organisationId,e=>e.FixAssetId==id.Value);
            if (fixAssetDataGrid == null)
            {
                return HttpNotFound();
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = fixAssetDataGrid
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId,p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFixAssets(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.DateofPurchase >= fromDate && e.DateofPurchase <= toDate, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}