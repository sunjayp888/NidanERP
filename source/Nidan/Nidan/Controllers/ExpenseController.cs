using System;
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

        // GET: OtherFee/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList();
            var totalPettyCash = NidanBusinessService.RetrieveCentrePettyCashs(organisationId, centreId, e => e.CentreId == centreId).Items.FirstOrDefault()?.Amount ?? 0;
            var totalDebitAmount = NidanBusinessService.RetrieveExpenses(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.DebitAmount);
            var viewModel = new ExpenseViewModel()
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
        public ActionResult Create(ExpenseViewModel expenseViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var isCashAvailable = expenseViewModel.AvailablePettyCash > expenseViewModel.Expense.DebitAmount;
            expenseViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
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
                expenseViewModel.Expense.PaymentModeId = (int)PaymentMode.Cash;
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
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList();
            var expense = NidanBusinessService.RetrieveExpense(organisationId, centreId, id.Value, e => e.CentreId == centreId);

            if (expense == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ExpenseViewModel
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
        public ActionResult Edit(ExpenseViewModel expenseViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                expenseViewModel.Expense.OrganisationId = organisationId;
                expenseViewModel.Expense.CentreId = centreId;
                expenseViewModel.Expense.PersonnelId = personnelId;
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
            var startOfWeekDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Sunday);
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
    }
}