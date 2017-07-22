using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using PaymentMode = Nidan.Business.Enum.PaymentMode;

namespace Nidan.Controllers
{
    public class OtherFeeController : BaseController
    {
        public OtherFeeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: OtherFee
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: OtherFee/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList();
            var totalPettyCash = NidanBusinessService.RetrieveCentrePettyCashs(organisationId, centreId, e => e.CentreId == centreId).Items.FirstOrDefault()?.Amount ?? 0;
            var totalDebitAmount = NidanBusinessService.RetrieveOtherFees(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.DebitAmount);
            var viewModel = new OtherFeeViewModel()
            {
                Expense = new Expense(),
                AvailablePettyCash = totalPettyCash - totalDebitAmount,
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name"),
                Projects = new SelectList(project, "ProjectId", "Name")
            };

            return View(viewModel);
        }

        // POST: OtherFee/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtherFeeViewModel otherFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var isCashAvailable = otherFeeViewModel.AvailablePettyCash > otherFeeViewModel.Expense.DebitAmount;
            otherFeeViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            if (!isCashAvailable)
            {
                ModelState.AddModelError("", String.Format("Insufficient cash, available petty cash is {0}", otherFeeViewModel.AvailablePettyCash));
                return View(otherFeeViewModel);
            }
            if (ModelState.IsValid)
            {
                otherFeeViewModel.Expense.OrganisationId = organisationId;
                otherFeeViewModel.Expense.CentreId = centreId;
                otherFeeViewModel.Expense.PersonnelId = personnelId;
                otherFeeViewModel.Expense.PaymentModeId = (int)PaymentMode.Cash;
                otherFeeViewModel.Expense = NidanBusinessService.CreateExpense(organisationId, centreId, otherFeeViewModel.Expense, otherFeeViewModel.SelectedProjectIds);
                return RedirectToAction("Index");
            }
            otherFeeViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            otherFeeViewModel.Projects = new SelectList(NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList());
            return View(otherFeeViewModel);
        }

        // GET: OtherFee/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList();
            var expense = NidanBusinessService.RetrieveExpense(organisationId, centreId, id.Value, e => e.CentreId == centreId);

            if (expense == null)
            {
                return HttpNotFound();
            }
            var viewModel = new OtherFeeViewModel
            {
                Expense = expense,
                CashMemo = expense.CashMemoNumbers,
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name"),
                Projects = new SelectList(project, "ProjectId", "Name"),
                SelectedProjectIds = expense?.ExpenseProjects.Select(e => e.ProjectId).ToList()
            };
            return View(viewModel);
        }

        // POST: OtherFee/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OtherFeeViewModel otherFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                otherFeeViewModel.Expense.OrganisationId = organisationId;
                otherFeeViewModel.Expense.CentreId = centreId;
                otherFeeViewModel.Expense.PersonnelId = personnelId;
                otherFeeViewModel.Expense.PaymentModeId = (int)PaymentMode.Cash;
                otherFeeViewModel.Expense = NidanBusinessService.UpdateExpense(organisationId, centreId, otherFeeViewModel.Expense, otherFeeViewModel.SelectedProjectIds);
                return RedirectToAction("Index");
            }
            var viewModel = new OtherFeeViewModel
            {
                Expense = otherFeeViewModel.Expense
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var startOfWeekDate = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
            var endOfWeekDate = startOfWeekDate.AddDays(6);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveExpenses(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= startOfWeekDate && p.CreatedDate <= endOfWeekDate, orderBy, paging));
        }

        [HttpPost]
        public ActionResult ListByCashMemo(string cashMemo, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveExpenses(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CashMemoNumbers == cashMemo, orderBy, paging));

        }

        public ActionResult Download(int? id)
        {
            var expense = NidanBusinessService.RetrieveExpense(UserOrganisationId, UserCentreId, id.Value, e => true);
            var data = NidanBusinessService.CreateExpenseBytes(UserOrganisationId, UserCentreId, expense);
            var voucherNumber = expense.VoucherNumber;
            return File(data, ".pdf", string.Format("{0} Other Fee.pdf", voucherNumber));
        }
    }
}