using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Enum;
using Nidan.Business.Extensions;
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
                candidateFeeData.FiscalYear = DateTime.UtcNow.FiscalYear();
                candidateFeeData.IsPaymentDone = true;
                candidateFeeData.BankName = candidateFee.BankName;
                candidateFeeData.ChequeDate = candidateFee.ChequeDate;
                candidateFeeData.PaidAmount = candidateFee.PaidAmount;
                candidateFeeData.PaymentModeId = candidateFee.PaymentModeId;
                candidateFeeData.ChequeNumber = candidateFee.ChequeNumber;
                candidateFeeData.PersonnelId = UserPersonnelId;
                candidateFee = NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeData);
                // RedirectToAction("Detail", new { id = candidateFeeData.CandidateInstallmentId });
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
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateInstallment(organisationId, id.Value, e => true);
            var enquiry = NidanBusinessService.RetrieveEnquiries(organisationId, e => e.StudentCode == data.StudentCode).ToList().FirstOrDefault();
            var candidateFeeModel = new CandidateFeeViewModel
            {
                CandidateName = String.Format("{0} {1} {2} {3}", enquiry?.Title, enquiry?.FirstName, enquiry?.MiddleName, enquiry?.LastName),
                CandidateInstallmentId = id.Value
            };
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
            //var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsSuperAdmin();
            //var registrations = NidanBusinessService.RetrieveRegistrations(organisationId, e => true);
            var data = NidanBusinessService.RetrieveCandidateInstallmentBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult PaymentMode()
        {
            return this.JsonNet(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, c => true));
        }

        public ActionResult Download(int? id)
        {
            var organisationId = UserOrganisationId;
            var feeTypeId = NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value).FeeTypeId;
            FeeType feeType = (FeeType)feeTypeId;
            var data = NidanBusinessService.CreateRegistrationRecieptBytes(organisationId, UserCentreId, id.Value);
            return File(data, ".pdf", feeType.ToString() + ".pdf");
        }
    }
}