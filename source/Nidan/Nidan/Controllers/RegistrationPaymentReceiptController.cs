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
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
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

        [Authorize(Roles = "Admin")]
        public ActionResult ViewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var registrationPaymentReceipt = NidanBusinessService.RetrieveRegistrationPaymentReceipt(UserOrganisationId, id.Value);
            if (registrationPaymentReceipt == null)
            {
                return HttpNotFound();
            }
            var viewModel = new RegistrationPaymentReceiptViewModel
            {
                RegistrationPaymentReceipt = registrationPaymentReceipt,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, e => true).ToList(), "PaymentModeId", "Name"),
                Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(UserOrganisationId, e => true).ToList(), "SchemeId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name"),
                BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(UserOrganisationId, e => true).ToList(), "BatchTimePreferId", "Name"),
            };
            return View(viewModel);
        }

        // GET: RegistrationPaymentReceipt/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
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
                    CourseId = enquiry.IntrestedCourseId,
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
            
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.EnquiryId = registrationPaymentReceiptViewModel.EnquiryId;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.RegistrationDate = _todayUTC;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate == null ? _todayUTC : registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.ChequeDate;
            var course = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.CourseId;
            var fees = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.Fees;
            registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.FollowUpDate = _todayUTC.AddDays(2);
            if (ModelState.IsValid)
            {
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.OrganisationId = UserOrganisationId;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.CentreId = UserCentreId;
                registrationPaymentReceiptViewModel.RegistrationPaymentReceipt = NidanBusinessService.CreateRegistrationPaymentReceipt(UserOrganisationId, registrationPaymentReceiptViewModel.RegistrationPaymentReceipt);
                return RedirectToAction("Index");
            }
            registrationPaymentReceiptViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            registrationPaymentReceiptViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList());
            return View(registrationPaymentReceiptViewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveRegistrationPaymentReceipts(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }
    }
}