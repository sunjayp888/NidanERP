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
        [Authorize(Roles = "Admin")]
        public ActionResult Create(string id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var project = NidanBusinessService.RetrieveProjects(organisationId, e => true).Items.ToList();
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1);
            var otherFee = NidanBusinessService.RetrieveOtherFees(organisationId, centreId, e => e.CentreId == centreId && e.CashMemo == id).Items.FirstOrDefault();
            if (otherFee != null)
            {
                otherFee.Project = null;
                otherFee.ExpenseHeader = null;
                otherFee.ProjectId = 0;
                otherFee.ExpenseHeaderId = 0;
            }
            var totalPettyCash = NidanBusinessService.RetrieveCentrePettyCashs(organisationId, centreId, e => e.CentreId == centreId).Items.FirstOrDefault()?.Amount ?? 0;
            var totalDebitAmount = NidanBusinessService.RetrieveOtherFees(organisationId, centreId, e => e.CentreId == centreId).Items.Sum(e => e.DebitAmount);
            var viewModel = new OtherFeeViewModel()
            {
                OtherFee = otherFee ?? new OtherFee(),
                AvailablePettyCash = totalPettyCash - totalDebitAmount,
                CashMemo = otherFee?.CashMemo,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name"),
                Projects = new SelectList(project, "ProjectId", "Name")
            };

            return View(viewModel);
        }

        // POST: OtherFee/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtherFeeViewModel otherFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var isCashAvailable = otherFeeViewModel.AvailablePettyCash > otherFeeViewModel.OtherFee.DebitAmount;
            otherFeeViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            otherFeeViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1).ToList(), "PaymentModeId", "Name");
            if (!isCashAvailable)
            {
                ModelState.AddModelError("", String.Format("Insufficient cash, available petty cash is {0}", otherFeeViewModel.AvailablePettyCash));
                return View(otherFeeViewModel);
            }
            if (ModelState.IsValid)
            {
                otherFeeViewModel.OtherFee.OrganisationId = organisationId;
                otherFeeViewModel.OtherFee.CentreId = centreId;
                otherFeeViewModel.OtherFee.PersonnelId = personnelId;
                otherFeeViewModel.OtherFee.CreatedDate = DateTime.UtcNow;
                otherFeeViewModel.OtherFee.PaymentModeId = (int)PaymentMode.Cash;
                otherFeeViewModel.OtherFee = NidanBusinessService.CreateOtherFee(organisationId, centreId, otherFeeViewModel.OtherFee);
                return RedirectToAction("Create", new { id = otherFeeViewModel.OtherFee.CashMemo });
            }
            otherFeeViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            otherFeeViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1).ToList(), "PaymentModeId", "Name");
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
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1);
            var otherFee = NidanBusinessService.RetrieveOtherFee(organisationId, centreId, id.Value, e => e.CentreId == centreId);

            if (otherFee == null)
            {
                return HttpNotFound();
            }
            var viewModel = new OtherFeeViewModel
            {
                OtherFee = otherFee,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name"),
                Projects = new SelectList(project, "ProjectId", "Name")
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
                otherFeeViewModel.OtherFee.OrganisationId = organisationId;
                otherFeeViewModel.OtherFee.CentreId = centreId;
                otherFeeViewModel.OtherFee.PersonnelId = personnelId;
                otherFeeViewModel.OtherFee.PaymentModeId = (int)PaymentMode.Cash;
                otherFeeViewModel.OtherFee = NidanBusinessService.UpdateOtherFee(organisationId, centreId, otherFeeViewModel.OtherFee);
                return RedirectToAction("Create", new { id = otherFeeViewModel.OtherFee.CashMemo });
            }
            var viewModel = new OtherFeeViewModel
            {
                OtherFee = otherFeeViewModel.OtherFee
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveOtherFees(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult ListByCashMemo(string cashMemo, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveOtherFees(UserOrganisationId, UserCentreId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CashMemo == cashMemo, orderBy, paging));

        }

        [HttpPost]
        public void Delete(int centreId, int otherFeeId)
        {
            NidanBusinessService.DeleteOtherFee(UserOrganisationId, centreId, otherFeeId);
        }

        public ActionResult Download(string id)
        {
            var otherFee = NidanBusinessService.RetrieveOtherFees(UserOrganisationId, UserCentreId, e=>e.CashMemo==id).Items.ToList();
            var data = NidanBusinessService.CreateOtherFeeBytes(UserOrganisationId, UserCentreId, otherFee);
            var cashMemoNumber = otherFee.FirstOrDefault()?.CashMemo;
            return File(data, ".pdf", string.Format("{0} Other Fee.pdf", cashMemoNumber));
        }
    }
}