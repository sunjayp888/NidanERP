﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using PaymentMode = Nidan.Business.Enum.PaymentMode;

namespace Nidan.Controllers
{
    public class ExpenseController : BaseController
    {
        private readonly IDocumentService _documentService;
        public ExpenseController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _documentService = documentService;
        }

        // GET: OtherFee
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        public ActionResult TermsAndCondition()
        {
            return View(new BaseViewModel());
        }

        // GET: OtherFee/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var currentMonth = DateTime.UtcNow.Month;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => isSuperAdmin || e.ExpenseHeaderId != 8).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == centreId).Items.ToList();
            var totalPettyCash = NidanBusinessService.RetrieveCentrePettyCashs(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.Amount);
            var totalDebitAmount = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.DebitAmount);
            var expenseData = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseGeneratedDate.Month == currentMonth);
            //   var eligibleExpenseHeader=expenseData.
            var viewModel = new ExpenseViewModel()
            {
                CentreId = centreId,
                Expense = new Expense(),
                AvailablePettyCash = totalPettyCash - totalDebitAmount,
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name"),
                Projects = new SelectList(project, "ProjectId", "Name"),
                SelectedProjectIds = new List<int>()
            };

            return View(viewModel);
        }

        // POST: OtherFee/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseViewModel expenseViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;
            var isCashAvailable = expenseViewModel.AvailablePettyCash > expenseViewModel.Expense.DebitAmount;
            var expenseHeadLimit = NidanBusinessService.RetrieveExpenseHeadLimits(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseMonth == currentMonth && e.ExpenseYear == currentYear && e.ExpenseHeaderId == expenseViewModel.Expense.ExpenseHeaderId).Items.FirstOrDefault();
            expenseViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            expenseViewModel.Projects = new SelectList(NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == centreId).Items.ToList());
            if (expenseHeadLimit != null)
            {
                var limitAmount = expenseHeadLimit.LimitAmount;
                var monthlyExpenseByExpenseHeader = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseGeneratedDate.Month == currentMonth && e.ExpenseHeaderId == expenseViewModel.Expense.ExpenseHeaderId);
                var totalMonthlyExpenseByExpenseHeader = monthlyExpenseByExpenseHeader.Items.Sum(e => e.DebitAmount);
                var balanceLimit = limitAmount - totalMonthlyExpenseByExpenseHeader;
                var isExpenseLimitExceed = expenseViewModel.Expense.DebitAmount > balanceLimit;   
                expenseViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
                if (isExpenseLimitExceed)
                {
                    ModelState.AddModelError("", String.Format("Limit is exceeded for {0} Expense Head", monthlyExpenseByExpenseHeader.Items.FirstOrDefault()?.ExpenseHeader.Name));
                    return View(expenseViewModel);
                }
            }
            if (!isCashAvailable)
            {
                ModelState.AddModelError("", String.Format("Insufficient cash, available petty cash is {0}", expenseViewModel.AvailablePettyCash));
                return View(expenseViewModel);
            }
            if (ModelState.IsValid)
            {
                expenseViewModel.Expense.OrganisationId = organisationId;
                expenseViewModel.Expense.CentreId = centreId;
                expenseViewModel.Expense.PersonnelId = personnelId;
                expenseViewModel.Expense.CreatedBy = personnelId;
                expenseViewModel.Expense.PaymentModeId = (int)PaymentMode.Cash;
                expenseViewModel.Expense.CreatedBy = personnelId;  
                expenseViewModel.Expense = NidanBusinessService.CreateExpense(organisationId, centreId, expenseViewModel.Expense, expenseViewModel.SelectedProjectIds);
                return RedirectToAction("Index");
            }
            expenseViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            expenseViewModel.Projects = new SelectList(NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList());
            return View(expenseViewModel);
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
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => isSuperAdmin || e.ExpenseHeaderId != 8).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => e.CentreId == centreId).Items.ToList();
            var expense = NidanBusinessService.RetrieveExpense(organisationId, centreId, id.Value, e => e.CentreId == centreId);
            var totalPettyCash = NidanBusinessService.RetrieveCentrePettyCashs(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.Amount);
            var totalDebitAmount = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.DebitAmount);

            if (expense == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ExpenseViewModel
            {
                CentreId = centreId,
                Expense = expense,
                AvailablePettyCash = totalPettyCash - totalDebitAmount,
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
        public ActionResult Edit(ExpenseViewModel expenseViewModel)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;
            var isCashAvailable = expenseViewModel.AvailablePettyCash > expenseViewModel.Expense.DebitAmount;
            var expenseHeadLimit = NidanBusinessService.RetrieveExpenseHeadLimits(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseMonth == currentMonth && e.ExpenseYear == currentYear && e.ExpenseHeaderId == expenseViewModel.Expense.ExpenseHeaderId).Items.FirstOrDefault();
            if (expenseHeadLimit != null)
            {
                var limitAmount = expenseHeadLimit.LimitAmount;
                var monthlyExpenseByExpenseHeader = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseGeneratedDate.Month == currentMonth && e.ExpenseHeaderId == expenseViewModel.Expense.ExpenseHeaderId);
                var totalMonthlyExpenseByExpenseHeader = monthlyExpenseByExpenseHeader.Items.Sum(e => e.DebitAmount);
                var balanceLimit = limitAmount - totalMonthlyExpenseByExpenseHeader;
                var isExpenseLimitExceed = expenseViewModel.Expense.DebitAmount > balanceLimit;
                expenseViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => isSuperAdmin || e.ExpenseHeaderId != 8).Items.ToList());
                if (isExpenseLimitExceed)
                {
                    ModelState.AddModelError("", String.Format("Limit is exceeded for {0} Expense Head", monthlyExpenseByExpenseHeader.Items.FirstOrDefault()?.ExpenseHeader.Name));
                    return View(expenseViewModel);
                }
            }
            if (!isCashAvailable)
            {
                ModelState.AddModelError("", String.Format("Insufficient cash, available petty cash is {0}", expenseViewModel.AvailablePettyCash));
                return View(expenseViewModel);
            }
            if (ModelState.IsValid)
            {
                expenseViewModel.Expense.OrganisationId = organisationId;
                expenseViewModel.Expense.CentreId = isSuperAdmin ? expenseViewModel.Expense.CentreId : centreId;
                expenseViewModel.Expense.PersonnelId = personnelId;
                expenseViewModel.Expense.CreatedBy = personnelId;
                expenseViewModel.Expense.PaymentModeId = (int)PaymentMode.Cash;
                expenseViewModel.Expense = NidanBusinessService.UpdateExpense(organisationId, centreId, expenseViewModel.Expense, expenseViewModel.SelectedProjectIds);
                return RedirectToAction("Index");
            }
            var viewModel = new ExpenseViewModel
            {
                Expense = expenseViewModel.Expense
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            //var startOfWeekDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Sunday);
            //var endOfWeekDate = startOfWeekDate.AddDays(6);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveExpenses(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult ListByCashMemo(string cashMemo, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveExpenses(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CashMemoNumbers == cashMemo, orderBy, paging));

        }

        public ActionResult Download(int? id)
        {
            var centreId = UserCentreId;
            var expense = NidanBusinessService.RetrieveExpense(UserOrganisationId, centreId, id.Value, e => true);
            var data = NidanBusinessService.CreateExpenseBytes(UserOrganisationId, centreId, expense);
            var voucherNumber = expense.VoucherNumber;
            return File(data, ".pdf", string.Format("{0} Other Fee.pdf", voucherNumber));
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveExpenseDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var expenseData = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId && e.CashMemoNumbers == documentViewModel.StudentCode).Items.ToList().FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                expenseData?.CashMemoNumbers, "Expense Document", documentViewModel.Attachment.FileName,
                documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }

        [HttpPost]
        public ActionResult IsDateInCurrentWeek(int expenseId)
        {
            var startOfWeekDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
            var endOfWeekDate = startOfWeekDate.AddDays(5);
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.ExpenseId == expenseId && e.ExpenseGeneratedDate >= startOfWeekDate && e.ExpenseGeneratedDate <= endOfWeekDate);
            var result = data.Items.Any();
            return this.JsonNet(result);
        }

        [HttpPost]
        public bool ExpenseLimitCheck(int expenseHeaderId)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;
            var totalMonthlyExpenseByExpenseHeader = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseGeneratedDate.Month == currentMonth && e.ExpenseHeaderId == expenseHeaderId).Items.Sum(e => e.DebitAmount);
            var firstOrDefault = NidanBusinessService.RetrieveExpenseHeadLimits(organisationId, centreId, e => e.CentreId == centreId && e.ExpenseMonth == currentMonth && e.ExpenseYear == currentYear && e.ExpenseHeaderId == expenseHeaderId).Items.FirstOrDefault();
            if (firstOrDefault != null)
            {
                var expenseHeadLimit = firstOrDefault.LimitAmount;
                var result = totalMonthlyExpenseByExpenseHeader >= expenseHeadLimit;
                return result;
            }
            return true;
        }

        [HttpPost]
        public ActionResult SearchByDateCentreId(DateTime fromDate, DateTime toDate, int centreId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveExpenses(UserOrganisationId, centreId, e => (isSuperAdmin || e.CentreId == centreId) && e.ExpenseGeneratedDate >= fromDate && e.ExpenseGeneratedDate <= toDate && e.CentreId == centreId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveExpenses(UserOrganisationId, centreId, e => (isSuperAdmin || e.CentreId == centreId) && e.ExpenseGeneratedDate >= fromDate && e.ExpenseGeneratedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCentres()
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCentres(organisationId);
            return this.JsonNet(data);
        }
        
        [HttpPost]
        public ActionResult SearchExpenseHeaderGridByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            //var data = NidanBusinessService.RetrieveExpenseHeaderGrid(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.ExpenseGeneratedDate >= fromDate && e.ExpenseGeneratedDate <= toDate, orderBy, paging);
            var data = NidanBusinessService.RetriveExpenseHeaderSummaryReportByDate(organisationId,centreId, fromDate,toDate);
            return this.JsonNet(data);
        }
    }
}