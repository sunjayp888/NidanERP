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
    public class StockPurchaseController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public StockPurchaseController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: StockPurchase
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: StockPurchase/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var sectors = NidanBusinessService.RetrieveCentreSectors(organisationId, centreId, e => e.CentreId == centreId);
            var stockTypes = NidanBusinessService.RetrieveStockTypes(organisationId, e => true);
            var studentKits = NidanBusinessService.RetrieveStudentKits(organisationId, e => true);
            var viewModel = new StockPurchaseViewModel()
            {
                StockPurchase = new StockPurchase(),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                StockTypes = new SelectList(stockTypes, "StockTypeId", "Name"),
                StudentKits = new SelectList(studentKits, "StudentKitId", "Name")
            };
            return View(viewModel);
        }

        // POST: StockPurchase/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockPurchaseViewModel stockPurchaseViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                stockPurchaseViewModel.StockPurchase.OrganisationId = organisationId;
                stockPurchaseViewModel.StockPurchase.CentreId = centreId;
                stockPurchaseViewModel.StockPurchase = NidanBusinessService.CreateStockPurchase(UserOrganisationId, stockPurchaseViewModel.StockPurchase);
                return RedirectToAction("Index");
            }
            stockPurchaseViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveCentreSectors(organisationId,centreId, e => true).ToList());
            stockPurchaseViewModel.StockTypes = new SelectList(NidanBusinessService.RetrieveStockTypes(organisationId,e => true).ToList());
            stockPurchaseViewModel.StudentKits = new SelectList(NidanBusinessService.RetrieveStudentKits(organisationId, e => true).ToList());
            return View(stockPurchaseViewModel);
        }

        // GET: StockPurchase
        public ActionResult StockIssue(int? id)
        {
            TempData["StockPurchaseId"] = id;
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveStockDataGrid(organisationId, p => isSuperAdmin || p.CentreId == centreId && (p.StockPurchaseDate.Month == DateTime.UtcNow.Month || p.TotalBalanceQuantity != 0), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult StockIssueList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var stockPurchaseId = Convert.ToInt32(TempData["StockPurchaseId"]);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveStockIssues(organisationId, centreId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.StockPurchaseId == stockPurchaseId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveStockDataGrid(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId && p.StockPurchaseDate.Month == DateTime.UtcNow.Month || p.TotalBalanceQuantity != 0), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveStockDataGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.StockPurchaseDate >= fromDate && e.StockPurchaseDate <= toDate && e.TotalBalanceQuantity!=0, orderBy, paging));
        }

        [HttpPost]
        public ActionResult StockPurchaseByStationary(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveStockDataGrid(organisationId, p => isSuperAdmin || p.CentreId == centreId && p.StockTypeId==1 && (p.StockPurchaseDate.Month == DateTime.UtcNow.Month || p.TotalBalanceQuantity != 0), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult StockPurchaseBySector(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveStockDataGrid(organisationId, p => isSuperAdmin || p.CentreId == centreId && p.StockTypeId == 2 && (p.StockPurchaseDate.Month == DateTime.UtcNow.Month || p.TotalBalanceQuantity != 0), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}