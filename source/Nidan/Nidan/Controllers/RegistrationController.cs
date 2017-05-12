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
            var centreId = UserCentreId;
            id = id ?? 0;
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, id.Value, e => true);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var interestedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var courseInstallments = NidanBusinessService.RetrieveCourseInstallments(organisationId, centreId);
            var viewModel = new RegistrationViewModel
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                Enquiry = enquiry,
                StudentCode = enquiry.StudentCode,
                EnquiryId = enquiry.EnquiryId,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
            };
            return View(viewModel);
        }

        // POST: Registration/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationViewModel registrationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                registrationViewModel.Registration.EnquiryId = registrationViewModel.EnquiryId;
                registrationViewModel.Registration.FollowupDate=DateTime.UtcNow.AddDays(2);
                registrationViewModel.Registration.RegistrationDate=DateTime.UtcNow;
                var registration = _nidanBusinessService.CreateCandidateRegistration(organisationId, centreId, registrationViewModel.StudentCode, registrationViewModel.Registration);
                return RedirectToAction("Edit", new {id = registration.RegistrationId});
            }
            return View();
        }

        // GET: Registration/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var registration = NidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var courseInstallments = NidanBusinessService.RetrieveCourseInstallments(organisationId, centreId);

            if (registration == null)
            {
                return HttpNotFound();
            }
            var interestedCourseIds = registration.Enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, registration.EnquiryId);
            var viewModel = new RegistrationViewModel()
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                EnquiryId = enquiry.EnquiryId,
                Registration = registration,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
            };
            return View(viewModel);
        }

        // POST: RegistrationPaymentReceipt/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationViewModel registrationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                registrationViewModel.Registration.OrganisationId = organisationId;
                registrationViewModel.Registration.CentreId = centreId;
                registrationViewModel.Registration = _nidanBusinessService.UpdateRegistartion(organisationId, registrationViewModel.Registration);
                return RedirectToAction("Edit", new { id = registrationViewModel.Registration.RegistrationId });
            }
            var viewModel = new RegistrationViewModel
            {
                Registration = registrationViewModel.Registration
            };

                return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveRegistrations(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsAdmissionDone == false, orderBy, paging));
        }

        [HttpPost]
        public ActionResult CandidateFeeList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveRegistrations(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult EnquiryList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsRegistrationDone == false, orderBy, paging));
        }

        [HttpPost]
        public ActionResult EnquirySearch(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourseInstallmentByCourseId(int courseId)
        {
            var data = NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, c => c.CourseId == courseId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourseInstallment(int courseInstallmentId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, c => c.CourseInstallmentId == courseInstallmentId).FirstOrDefault());
        }

    }
}