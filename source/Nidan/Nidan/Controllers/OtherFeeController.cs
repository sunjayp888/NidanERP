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
    public class OtherFeeController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public OtherFeeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: OtherFee
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: OtherFee/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            id = id ?? 0;
            var enquiry = _nidanBusinessService.RetrieveEnquiry(organisationId, id.Value);
            var onlineExams = _nidanBusinessService.RetrieveOnlineExams(organisationId, e => true);
            var feeTypes = _nidanBusinessService.RetrieveFeeTypes(organisationId, e => e.FeeTypeId != 1 && e.FeeTypeId != 2 && e.FeeTypeId != 3 && e.FeeTypeId != 5 && e.FeeTypeId != 6);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var viewModel = new OtherFeeViewModel
            {
                EnquiryId = enquiry.EnquiryId,
                Enquiry = enquiry,
                OnlineExams = new SelectList(onlineExams, "OnlineExamId", "Name"),
                FeeTypes = new SelectList(feeTypes, "FeeTypeId", "Name"),
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                StudentCode = enquiry.StudentCode,
                CandidateName = string.Format("{0} {1} {2}", enquiry.Title, enquiry.FirstName, enquiry.LastName),
                OtherFee = new OtherFee
                {
                    StudentCode = enquiry.StudentCode,
                    EnquiryId = enquiry.EnquiryId
                }
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
            otherFeeViewModel.OtherFee.StudentCode = otherFeeViewModel.StudentCode;
            if (ModelState.IsValid)
            {
                otherFeeViewModel.OtherFee.OrganisationId = organisationId;
                otherFeeViewModel.OtherFee.CentreId = centreId;
                otherFeeViewModel.OtherFee.CreatedBy = personnelId;
                otherFeeViewModel.OtherFee = _nidanBusinessService.CreateOtherFee(organisationId, otherFeeViewModel.OtherFee);
                return RedirectToAction("Index","Enquiry");
            }
            otherFeeViewModel.OnlineExams = new SelectList(_nidanBusinessService.RetrieveOnlineExams(organisationId, e => true));
            otherFeeViewModel.FeeTypes = new SelectList(_nidanBusinessService.RetrieveFeeTypes(organisationId, e => true));
            otherFeeViewModel.PaymentModes = new SelectList(_nidanBusinessService.RetrievePaymentModes(organisationId, e => true));
            return View(otherFeeViewModel);
        }

        [HttpPost]
        public ActionResult CandidateOtherFeeByEnquiryId(int enquiryId, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveOtherFees(UserOrganisationId, e => e.EnquiryId == enquiryId, orderBy, paging);
            return this.JsonNet(data);
        }

        public ActionResult DownloadOtherFee(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.CreateOtherFeeRecieptBytes(organisationId, centreId, id.Value);
            return File(data, ".pdf", "Other Fee Reciept.pdf");
        }
    }
}