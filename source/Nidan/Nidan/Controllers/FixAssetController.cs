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
            //var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == centreId);
            var products = NidanBusinessService.RetrieveProducts(organisationId, e => true);
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = new FixAsset(),
                //Rooms = new SelectList(rooms, "RoomId", "Description"),
                Products = new SelectList(products, "ProductId", "Name")
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
            //fixAssetViewModel.Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => true).ToList());
            fixAssetViewModel.Products = new SelectList(NidanBusinessService.RetrieveProducts(organisationId, e => true).ToList());
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
            var fixAsset = NidanBusinessService.RetrieveFixAsset(organisationId, id.Value, e => true);
            if (fixAsset == null)
            {
                return HttpNotFound();
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = fixAsset,
                Rooms = new SelectList(NidanBusinessService.RetrieveRooms(UserOrganisationId, e => e.CentreId == UserCentreId).ToList(), "RoomId", "Description"),
                Products = new SelectList(NidanBusinessService.RetrieveProducts(UserOrganisationId, e => true).ToList(), "ProductId", "Name"),
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

        // GET: StockPurchase
        public ActionResult CentreFixAsset(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAssetId = id.Value
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CentreFixAssetList(int fixAssetId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCentreFixAssets(organisationId, fixAssetId, e => (isSuperAdmin || e.CentreId == centreId) && e.FixAssetId == fixAssetId, orderBy, paging);
            return this.JsonNet(data);

        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
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

        [HttpPost]
        public ActionResult Room()
        {
            var centreId = UserCentreId;
            return this.JsonNet(NidanBusinessService.RetrieveRooms(UserOrganisationId, c => c.CentreId==centreId));
        }

        [HttpPost]  
        public ActionResult UpdateCentreFixAsset(int roomId, DateTime dateofuse, List<CentreFixAsset> centreFixAssets)
        {
            var result = NidanBusinessService.MarkAsset(UserOrganisationId, centreFixAssets, roomId, dateofuse);
            return this.JsonNet(result);
        }
    }
}