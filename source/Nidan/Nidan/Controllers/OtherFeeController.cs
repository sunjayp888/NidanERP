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
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var expenseHeader = NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList();
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1);
            var viewModel = new OtherFeeViewModel()
            {
                OtherFee = new OtherFee(),
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name")
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
            if (ModelState.IsValid)
            {
                otherFeeViewModel.OtherFee.OrganisationId = organisationId;
                otherFeeViewModel.OtherFee.CentreId = centreId;
                otherFeeViewModel.OtherFee.PersonnelId = personnelId;
                otherFeeViewModel.OtherFee.CreatedDate = DateTime.UtcNow;
                otherFeeViewModel.OtherFee.PaymentModeId = (int) PaymentMode.Cash;
                otherFeeViewModel.OtherFee = NidanBusinessService.CreateOtherFee(organisationId, centreId, otherFeeViewModel.OtherFee);
                return RedirectToAction("Index");
            }
            otherFeeViewModel.ExpenseHeaders = new SelectList(NidanBusinessService.RetrieveExpenseHeaders(organisationId, e => true).Items.ToList());
            otherFeeViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1).ToList(), "PaymentModeId", "Name");
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
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => e.PaymentModeId == 1);
            var otherFee = NidanBusinessService.RetrieveOtherFee(organisationId, centreId, id.Value, e => true);

            if (otherFee == null)
            {
                return HttpNotFound();
            }
            var viewModel = new OtherFeeViewModel
            {
                OtherFee = otherFee,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                ExpenseHeaders = new SelectList(expenseHeader, "ExpenseHeaderId", "Name")
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
                return RedirectToAction("Index");
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
    }
}