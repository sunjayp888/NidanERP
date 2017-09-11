using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class StockIssueController : BaseController
    {
        public StockIssueController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: StockIssue
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: StockIssue/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            id = id ?? 0;

            var stockPurchase = NidanBusinessService.RetrieveStockPurchase(organisationId, centreId, id.Value, e => true);
            var stockIssueData = NidanBusinessService.RetrieveStockIssues(organisationId, centreId, e => e.StockPurchaseId == id.Value);
            var totalIssuedQuantity = stockIssueData.Items.Sum(e => e.IssuedQuantity);
            var viewModel = new StockIssueViewModel()
            {
                StockPurchase = stockPurchase,
                StockPurchaseId = id.Value,
                BalanceQuantity = stockPurchase.Quantity - totalIssuedQuantity
            };
            return View(viewModel);

        }

        // POST: StockPurchase/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockIssueViewModel stockIssueViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var isbalanceItem = stockIssueViewModel.BalanceQuantity >= stockIssueViewModel.StockIssue.IssuedQuantity;
            if (!isbalanceItem)
            {
                ModelState.AddModelError("", String.Format("Insufficient item, available item is {0}", stockIssueViewModel.BalanceQuantity));
                return View(stockIssueViewModel);
            }
            if (ModelState.IsValid)
            {
                stockIssueViewModel.StockIssue.StockPurchaseId = stockIssueViewModel.StockPurchaseId;
                stockIssueViewModel.StockIssue.OrganisationId = organisationId;
                stockIssueViewModel.StockIssue.CentreId = centreId;
                stockIssueViewModel.StockIssue.BalanceQuantity = stockIssueViewModel.BalanceQuantity - stockIssueViewModel.StockIssue.IssuedQuantity;
                stockIssueViewModel.StockIssue = NidanBusinessService.CreateStockIssue(organisationId, stockIssueViewModel.StockIssue);
                return RedirectToAction("Index", "StockPurchase");
            }

            return View(stockIssueViewModel);
        }
    }
}