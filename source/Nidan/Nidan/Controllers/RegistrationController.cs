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
    public class RegistrationController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        private readonly DateTime _todayUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public RegistrationController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
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
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, id.Value, e => true);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var interestedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, UserCentreId).Where(e => interestedCourseIds.Contains(e.CourseId));
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var viewModel = new RegistrationViewModel
            {
                EnquiryId = id.Value,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                Enquiry = enquiry
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
            var a = registrationPaymentReceiptViewModel;
            if (a.SectorId != 0 || a.IntrestedCourseId != 0 || a.BatchTimePreferId != 0)
            {
                var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId,
               registrationPaymentReceiptViewModel.EnquiryId);
                enquiry.SectorId = registrationPaymentReceiptViewModel.SectorId;
                enquiry.IntrestedCourseId = registrationPaymentReceiptViewModel.IntrestedCourseId;
                enquiry.BatchTimePreferId = registrationPaymentReceiptViewModel.BatchTimePreferId;
                NidanBusinessService.UpdateEnquiry(organisationId, enquiry, null);
            }
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
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
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
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Remarks = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Remarks;
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
            var data = NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}