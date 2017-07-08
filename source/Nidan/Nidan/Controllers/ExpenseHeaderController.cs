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
    public class ExpenseHeaderController : BaseController
    {
        public ExpenseHeaderController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: ExpenseHeader
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: ExpenseHeader/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var viewModel = new ExpenseHeaderViewModel
            {
                ExpenseHeader = new ExpenseHeader(),
            };

            return View(viewModel);
        }

        // POST: ExpenseHeader/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseHeaderViewModel expenseHeaderViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                expenseHeaderViewModel.ExpenseHeader.OrganisationId = organisationId;
                expenseHeaderViewModel.ExpenseHeader = NidanBusinessService.CreateExpenseHeader(organisationId, expenseHeaderViewModel.ExpenseHeader);
                return RedirectToAction("Index");
            }
            return View(expenseHeaderViewModel);
        }

        // GET: ExpenseHeader/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeader(organisationId, id.Value, e => true);

            if (expenseHeader == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ExpenseHeaderViewModel
            {
                ExpenseHeader = expenseHeader
            };
            return View(viewModel);
        }

        // POST: ExpenseHeader/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExpenseHeaderViewModel expenseHeaderViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                expenseHeaderViewModel.ExpenseHeader.OrganisationId = organisationId;
                expenseHeaderViewModel.ExpenseHeader = NidanBusinessService.UpdateExpenseHeader(organisationId, expenseHeaderViewModel.ExpenseHeader);
                return RedirectToAction("Index");
            }
            var viewModel = new ExpenseHeaderViewModel
            {
                ExpenseHeader = expenseHeaderViewModel.ExpenseHeader
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveExpenseHeaders(UserOrganisationId, p => true, orderBy, paging));
        }
    }
}