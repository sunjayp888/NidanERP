using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Enum;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class CandidateFeeController : BaseController
    {
        public CandidateFeeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: CandidateFee
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var candidateFee = NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value);
            // var paymentmodes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);

            if (candidateFee == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CandidateFeeViewModel
            {
                CandidateFeeId = candidateFee.CandidateFeeId,
                CandidateFee = candidateFee,
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, e => true).ToList(), "PaymentModeId", "Name"),
            };
            return View(viewModel);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetFeeCandidate(int id)
        {
            var userOrganisationId = UserOrganisationId;
            return this.JsonNet(NidanBusinessService.RetrieveCandidateFee(userOrganisationId, id));
        }

        [HttpPost]
        public ActionResult SaveFee(CandidateFee candidateFee)
        {
            var organisationId = UserOrganisationId;
            try
            {
                var candidateFeeData = NidanBusinessService.RetrieveCandidateFee(organisationId, candidateFee.CandidateFeeId);
                candidateFee.OrganisationId = organisationId;
                candidateFee.CentreId = UserCentreId;
                candidateFeeData.PaymentDate = DateTime.UtcNow;
                candidateFeeData.FeeTypeId = (int)FeeType.Installment;
                candidateFeeData.FiscalYear = "2017-18";
                candidateFeeData.BankName = candidateFee.BankName;
                candidateFeeData.ChequeDate = candidateFee.ChequeDate;
                candidateFeeData.PaidAmount = candidateFee.PaidAmount;
                candidateFeeData.PaymentModeId = candidateFee.PaymentModeId;
                candidateFeeData.ChequeNumber = candidateFee.ChequeNumber;
                candidateFee = NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeData);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveRegistrations(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Detail(int? id)
        {
            var candidateFeeModel = new CandidateFeeViewModel { CandidateInstallmentId = id.Value };
            return View(candidateFeeModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CandidateFeeList(int? id)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var candidateFee = NidanBusinessService.RetrieveCandidateFees(UserOrganisationId, c => c.CandidateInstallmentId == id.Value).Items;
            var data = NidanBusinessService.RetrieveCandidateFees(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CandidateInstallmentId == id).Items;
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            return this.JsonNet(NidanBusinessService.RetrieveCandidateFeeBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult PaymentMode()
        {
            return this.JsonNet(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, c => true));
        }
    }
}