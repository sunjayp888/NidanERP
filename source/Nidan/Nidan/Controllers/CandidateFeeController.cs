using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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

        [Authorize(Roles = "Admin , SuperAdmin")]
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

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult GetFeeCandidate(int id)
        {
            var userOrganisationId = UserOrganisationId;
            return this.JsonNet(NidanBusinessService.RetrieveCandidateFee(userOrganisationId, id));
        }

        [HttpPost]
        public ActionResult SaveFee(CandidateFeeViewModel candidateFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            try
            {
                var candidateFeeData = NidanBusinessService.RetrieveCandidateFee(organisationId, candidateFeeViewModel.CandidateFee.CandidateFeeId);
                var candidateInstallmentData = NidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateFeeId > candidateFeeData.CandidateFeeId && e.CandidateInstallmentId == candidateFeeData.CandidateInstallmentId).Items.FirstOrDefault();
                candidateFeeViewModel.CandidateFee.OrganisationId = organisationId;
                candidateFeeViewModel.CandidateFee.CentreId = UserCentreId;
                candidateFeeData.FeeTypeId = (int)FeeType.Installment;
                candidateFeeData.FiscalYear = DateTime.UtcNow.FiscalYear();
                candidateFeeData.IsPaymentDone = true;
                candidateFeeData.BankName = candidateFeeViewModel.CandidateFee.BankName;
                candidateFeeData.ChequeDate = candidateFeeViewModel.CandidateFee.ChequeDate;
                candidateFeeData.IsPaidAmountOverride = candidateFeeViewModel.CandidateFee.IsPaidAmountOverride;
                candidateFeeData.PaymentModeId = candidateFeeViewModel.CandidateFee.PaymentModeId;
                candidateFeeData.ChequeNumber = candidateFeeViewModel.CandidateFee.ChequeNumber;
                candidateFeeData.HaveReceipt = candidateFeeViewModel.CandidateFee.HaveReceipt;
                candidateFeeData.ReferenceReceiptNumber = candidateFeeViewModel.CandidateFee.ReferenceReceiptNumber;
                candidateFeeData.PersonnelId = UserPersonnelId;
                if (candidateFeeViewModel.CandidateFee.IsPaidAmountOverride && candidateFeeViewModel.CandidateFee.PaidAmount != null)
                {
                    if (candidateFeeViewModel.CandidateFee.PaidAmount < candidateFeeData.InstallmentAmount)
                    {
                        candidateFeeData.BalanceInstallmentAmount = candidateFeeData.InstallmentAmount - candidateFeeViewModel.CandidateFee.PaidAmount;
                        if (candidateInstallmentData != null)
                        {
                            candidateFeeData.PaidAmount = candidateFeeViewModel.CandidateFee.PaidAmount;
                            NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeData);
                            AdjustInstallment(candidateFeeData.CandidateInstallmentId, 0, candidateFeeData.BalanceInstallmentAmount);
                            return this.JsonNet(true);
                        }
                    }
                    if (candidateFeeViewModel.CandidateFee.PaidAmount > candidateFeeData.InstallmentAmount)
                    {
                        candidateFeeData.AdvancedAmount = candidateFeeViewModel.CandidateFee.PaidAmount - candidateFeeData.InstallmentAmount;
                        if (candidateInstallmentData != null)
                        {
                            candidateFeeData.PaidAmount = candidateFeeViewModel.CandidateFee.PaidAmount;
                            NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeData);
                            AdjustInstallment(candidateFeeData.CandidateInstallmentId, candidateFeeData.AdvancedAmount, 0);
                            return this.JsonNet(true);
                        }

                    }
                    if (candidateInstallmentData != null)
                    {
                        NidanBusinessService.UpdateCandidateFee(organisationId, candidateInstallmentData);
                    }
                    candidateFeeData.PaidAmount = candidateFeeViewModel.CandidateFee.PaidAmount;
                }
                else
                {
                    candidateFeeData.PaidAmount = candidateFeeData.InstallmentAmount;
                }
                candidateFeeViewModel.CandidateFee = NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeData);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }
        }

        public void AdjustInstallment(int? candidateInstallmentId, decimal? advancedAmount, decimal? balanceAmount)
        {
            var candidateFees = NidanBusinessService.RetrieveCandidateFees(UserOrganisationId, e => e.CandidateInstallmentId == candidateInstallmentId && e.FeeTypeId == (int)FeeType.Installment && !e.IsPaymentDone);
            //for advancedAmount => we are passing 0 to balanceAmount & for balanceAmount => passing 0 to advancedAmount
            var amount = advancedAmount != 0 ?
                (candidateFees.Items.Sum(e => e.InstallmentAmount) - advancedAmount) / candidateFees.Items.Count()
                : (candidateFees.Items.Sum(e => e.InstallmentAmount) + balanceAmount) / candidateFees.Items.Count();
            foreach (var candidate in candidateFees.Items)
            {
                candidate.InstallmentAmount = amount;
                NidanBusinessService.UpdateCandidateFee(UserOrganisationId, candidate);
            }

        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveCandidateInstallmentGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Detail(int? id)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateInstallment(organisationId, id.Value, e => true);
            var enquiry = NidanBusinessService.RetrieveEnquiries(organisationId, e => e.StudentCode == data.StudentCode).ToList().FirstOrDefault();
            //var candidateFeeData = NidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateInstallmentId == id.Value);
            var candidateFeeData = NidanBusinessService.RetrieveCandidateFeeGrid(organisationId, e => e.CandidateInstallmentId == id.Value);
            var totalPaid = candidateFeeData.Items.Sum(e => e.PaidAmount);
            var courseFee = data.PaymentMethod == "MonthlyInstallment" ? data.CourseFee : data.LumpsumAmount;
            var balanceAmount = data.PaymentMethod == "MonthlyInstallment" ? data.CourseFee - totalPaid : data.LumpsumAmount - totalPaid;
            var candidateFeeModel = new CandidateFeeViewModel
            {
                CandidateName = String.Format("{0} {1} {2} {3}", enquiry?.Title, enquiry?.FirstName, enquiry?.MiddleName, enquiry?.LastName),
                CandidateInstallmentId = id.Value,
                TotalPaidFee = totalPaid,
                BalanceFee = balanceAmount,
                CourseFee = courseFee,
            };
            return View(candidateFeeModel);
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        public ActionResult CandidateFeeList(int? id)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveCandidateFeeGrid(UserOrganisationId,
                p => p.CandidateInstallmentId == id);
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
            var centreId = UserCentreId;
            var admission = new Admission();
            var candidateFee = NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value);
            var feeTypeId = candidateFee.FeeTypeId;
            string firstName = "";
            string lastName = "";
            FeeType feeType = (FeeType)feeTypeId;
            if (feeTypeId == 2)
            {
                var registration = NidanBusinessService.RetrieveRegistrations(organisationId, e => e.StudentCode == candidateFee.StudentCode).Items.FirstOrDefault();
                var admissionData = NidanBusinessService.RetrieveAdmissions(organisationId, e => e.RegistrationId == registration.RegistrationId).Items.FirstOrDefault();
                firstName = admissionData?.Registration.Enquiry.FirstName;
                lastName = admissionData?.Registration.Enquiry.LastName;
                admission = admissionData;
            }
            var data = feeTypeId == 1 || feeTypeId == 3 ? NidanBusinessService.CreateRegistrationRecieptBytes(organisationId, centreId, id.Value)
                : NidanBusinessService.CreateEnrollmentBytes(organisationId, centreId, admission);
            return File(data, ".pdf", string.Format("{0} {1} {2}.pdf", firstName, lastName, feeType.ToString()));
        }

        [HttpPost]
        public ActionResult TotalFee(int? id)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateInstallment(organisationId, id.Value, e => true);
            var candidateFeeData = NidanBusinessService.RetrieveCandidateFeeGrid(organisationId, e => e.CandidateInstallmentId == id.Value);
            var totalPaid = candidateFeeData.Items.Sum(e => e.PaidAmount);
            var courseFee = data.PaymentMethod == "MonthlyInstallment" ? data.CourseFee : data.LumpsumAmount;
            var balanceAmount = data.PaymentMethod == "MonthlyInstallment" ? data.CourseFee - totalPaid : data.LumpsumAmount - totalPaid;
            var candidateFeeModel = new CandidateFeeViewModel
            {
                TotalPaidFee = totalPaid,
                BalanceFee = balanceAmount,
                CourseFee = courseFee
            };
            return this.JsonNet(candidateFeeModel);
        }

    }
}