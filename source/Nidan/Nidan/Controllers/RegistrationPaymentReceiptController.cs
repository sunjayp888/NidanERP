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
    public class RegistrationPaymentReceiptController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        private readonly DateTime _todayUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public RegistrationPaymentReceiptController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: RegistrationPaymentReceipt
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: RegistrationPaymentReceipt/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId,id.Value,e=>true);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var viewModel = new RegistrationPaymentReceiptViewModel
            {
               
                EnquiryId = id.Value,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),

                RegistrationPaymentReceipt = new RegistrationPaymentReceipt()
                {
                    CourseId = enquiry?.IntrestedCourseId ?? 0,
                    //ChequeNo = "000000",
                    //BankName = "Nidan",
                    Particulars = "Fees Against Registration",
                    Enquiry = enquiry
                }
            };
            return View(viewModel);
        }

        // POST: RegistrationPaymentReceipt/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationPaymentReceiptViewModel registrationPaymentReceiptViewModel)
        {
            var organisationId = UserOrganisationId;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.OrganisationId = UserOrganisationId;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.CentreId = UserCentreId;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.EnquiryId = registrationPaymentReceiptViewModel.EnquiryId;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.RegistrationDate = _todayUtc;
            if (ModelState.IsValid)
            {
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt = NidanBusinessService.CreateRegistrationPaymentReceipt(UserOrganisationId, registrationPaymentReceiptViewModel.RegistrationPaymentReceipt);
                return RedirectToAction("Edit", new { id = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.RegistrationPaymentReceiptId });
            }
            registrationPaymentReceiptViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList());
            return View(registrationPaymentReceiptViewModel);
        }

        // GET: RegistrationPaymentReceipt/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, id.Value, e => true);
            var registrationPaymentReceipt = NidanBusinessService.RetrieveRegistrationPaymentReceipt(UserOrganisationId, id.Value);
            if (registrationPaymentReceipt == null)
            {
                return HttpNotFound();
            }
            var viewModel = new RegistrationPaymentReceiptViewModel
            {
                EnquiryId = id.Value,
                RegistrationPaymentReceipt = registrationPaymentReceipt,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, e => true).ToList(), "PaymentModeId", "Name"),
                Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(UserOrganisationId, e => true).ToList(), "SchemeId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name"),
                BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(UserOrganisationId, e => true).ToList(), "BatchTimePreferId", "Name")
            };
            return View(viewModel);
        }

        // POST: RegistrationPaymentReceipt/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationPaymentReceiptViewModel registrationPaymentReceiptViewModel)
        {
            if (ModelState.IsValid)
            {
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.OrganisationId = UserOrganisationId;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.CentreId = UserCentreId;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate == null ? _todayUtc : registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate;
                var fees = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Fees;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.FollowUpDate = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.FollowUpDate;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Remarks =registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Remarks;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt = NidanBusinessService.UpdateRegistrationPaymentReceipt(UserOrganisationId, registrationPaymentReceiptViewModel.RegistrationPaymentReceipt);
                return RedirectToAction("Edit", new { id = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.RegistrationPaymentReceiptId });
                //return RedirectToAction("Index");
            }
            var viewModel = new RegistrationPaymentReceiptViewModel
            {
                RegistrationPaymentReceipt = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveRegistrationPaymentReceipts(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult EnquiryList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Registered == false, orderBy, paging));
        }

        [HttpPost]
        public ActionResult EnquirySearch(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            //var enquiry= NidanBusinessService.RetrieveEnquiries(UserOrganisationId,
            //    p => (isSuperAdmin || p.CentreId == UserCentreId) && p.EnquiryStatus != "Registration" && p.CandidateName==searchKeyword
            //    , orderBy,paging);
            var data =NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.EnquiryStatus != "Registration", orderBy, paging);
            return this.JsonNet(data);
            //this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId,
            //    p => (isSuperAdmin || p.CentreId == UserCentreId) && p.EnquiryStatus != "Registration" && p.CandidateName==searchKeyword
            //    , orderBy,paging));
        }
    }
}