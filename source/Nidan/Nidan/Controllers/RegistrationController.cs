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
        public RegistrationController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: RegistrationPaymentReceipt
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        public ActionResult TodaysRegistration()
        {
            return View(new BaseViewModel());
        }

        // GET: RegistrationPaymentReceipt/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            id = id ?? 0;
            var enquiry = _nidanBusinessService.RetrieveEnquiry(organisationId, id.Value, e => true);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var interestedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = _nidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var batchTimePrefers = _nidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var courseInstallments = _nidanBusinessService.RetrieveCentreCourseInstallments(organisationId, centreId).Items.Select(e => e.CourseInstallment).ToList();
            var counsellingData = _nidanBusinessService.RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            var counsellingCourse = _nidanBusinessService.RetrieveCourses(organisationId, e => true).Where(e => e.CourseId == counsellingData?.CourseOfferedId);
            var viewModel = new RegistrationViewModel
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                Enquiry = enquiry,
                StudentCode = enquiry.StudentCode,
                EnquiryId = enquiry.EnquiryId,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                CounsellingCourse = new SelectList(counsellingCourse, "CourseId", "Name")
            };
            return View(viewModel);
        }

        // POST: Registration/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationViewModel registrationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                registrationViewModel.Registration.EnquiryId = registrationViewModel.EnquiryId;
                registrationViewModel.Registration = _nidanBusinessService.CreateCandidateRegistration(organisationId, centreId, personnelId, registrationViewModel.StudentCode, registrationViewModel.Registration);
                // return RedirectToAction("Edit", new { id = registration.RegistrationId });
                return RedirectToAction("Index");
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
            var registration = _nidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var batchTimePrefers = _nidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            if (registration == null)
            {
                return HttpNotFound();
            }
            var courseInstallments = _nidanBusinessService.RetrieveCentreCourseInstallments(organisationId, centreId).Items.Select(e => e.CourseInstallment).ToList();
            var interestedCourseIds = registration.Enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = _nidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var enquiry = _nidanBusinessService.RetrieveEnquiry(organisationId, registration.EnquiryId);
            var counsellingData = _nidanBusinessService.RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            var counsellingCourse = _nidanBusinessService.RetrieveCourses(organisationId, e => true).Where(e => e.CourseId == counsellingData?.CourseOfferedId);
            var candidateFeeData = _nidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateInstallmentId == registration.CandidateInstallmentId);
            var paidAmount = candidateFeeData.Items.Where(e => e.FeeTypeId == 1 || e.FeeTypeId == 6).Sum(e => e.PaidAmount);
            var viewModel = new RegistrationViewModel()
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                EnquiryId = enquiry.EnquiryId,
                Registration = registration,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                CounsellingCourse = new SelectList(counsellingCourse, "CourseId", "Name"),
                Enquiry = enquiry,
                PaidAmount = paidAmount.Value
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

        // GET: Registration/Edit/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var registration = _nidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var batchTimePrefers = _nidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var courseInstallments = _nidanBusinessService.RetrieveCourseInstallments(organisationId, centreId);
            var candidateFeeData = _nidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateInstallmentId == registration.CandidateInstallmentId);
            var paidAmount = candidateFeeData.Items.Where(e => e.FeeTypeId == 1 || e.FeeTypeId == 6).Sum(e => e.PaidAmount);
            if (registration == null)
            {
                return HttpNotFound();
            }
            var interestedCourseIds = registration.Enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = _nidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var enquiry = _nidanBusinessService.RetrieveEnquiry(organisationId, registration.EnquiryId);
            var viewModel = new RegistrationViewModel()
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                EnquiryId = enquiry.EnquiryId,
                Registration = registration,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                PaidAmount = paidAmount.Value
            };
            return View(viewModel);
        }

        // GET: Registration/OtherFee/{id}
        public ActionResult OtherFee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var registration = _nidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var feeTypes = _nidanBusinessService.RetrieveFeeTypes(organisationId, e => e.FeeTypeId !=1 && e.FeeTypeId != 2 && e.FeeTypeId != 3 && e.FeeTypeId != 5);
            if (registration == null)
            {
                return HttpNotFound();
            }
            var courseInstallments = NidanBusinessService.RetrieveCentreCourseInstallments(organisationId, centreId).Items.Select(e => e.CourseInstallment).ToList();
            var interestedCourseIds = registration.Enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, registration.EnquiryId);
            var counsellingData = NidanBusinessService.RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            var counsellingCourse = NidanBusinessService.RetrieveCourses(organisationId, e => true).Where(e => e.CourseId == counsellingData?.CourseOfferedId);
            var candidateFeeData = _nidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateInstallmentId == registration.CandidateInstallmentId);
            var paidAmount = candidateFeeData.Items.Where(e => e.FeeTypeId == 1 || e.FeeTypeId == 6).Sum(e => e.PaidAmount);
            var viewModel = new RegistrationViewModel()
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                EnquiryId = enquiry.EnquiryId,
                Registration = registration,
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                CounsellingCourse = new SelectList(counsellingCourse, "CourseId", "Name"),
                FeeTypes = new SelectList(feeTypes, "FeeTypeId", "Name"),
                Enquiry = enquiry,
                PaidAmount = paidAmount.Value
            };
            return View(viewModel);
        }

        // POST: Registration/OtherFee
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherFee(RegistrationViewModel registrationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                registrationViewModel.Registration.EnquiryId = registrationViewModel.EnquiryId;
                registrationViewModel.CandidateFee.CentreId = centreId;
                registrationViewModel.CandidateFee.PersonnelId = personnelId;
                registrationViewModel.CandidateFee.CandidateInstallmentId = registrationViewModel.Registration.CandidateInstallmentId;
                registrationViewModel.CandidateFee.StudentCode = registrationViewModel.Registration.StudentCode;
                registrationViewModel.CandidateFee = _nidanBusinessService.CreateCandidateFee(organisationId, registrationViewModel.CandidateFee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveRegistrationGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsAdmissionDone == false, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult TodaysRegistrationList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveRegistrationGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsAdmissionDone == false && p.RegistrationDate == _today, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidateFeeList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveRegistrations(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            foreach (var item in data.Items)
            {
                var candidateInstallmentId = item.CandidateInstallmentId;
                var courseFee = NidanBusinessService.RetrieveCandidateInstallment(organisationId, candidateInstallmentId, e => true).CourseFee;
                var totalPaidAmount = NidanBusinessService.RetrieveCandidateFees(organisationId, e => e.CandidateInstallmentId == candidateInstallmentId).Items.Sum(e => e.PaidAmount);
                item.CandidateFee.PaidAmount = totalPaidAmount;
                item.CandidateFee.Particulars = (courseFee - totalPaidAmount).ToString();
            }
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult EnquiryList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsRegistrationDone == "NO", orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveRegistrationBySearchKeyword(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsAdmissionDone == false && p.SearchField.Trim().ToLower().Contains(searchKeyword.Trim().ToLower()), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveRegistrationGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.RegistrationDate >= fromDate && e.RegistrationDate <= toDate && e.IsAdmissionDone == false, orderBy, paging);
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

        //  [HttpPost]
        public ActionResult Download(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.CreateRegistrationRecieptBytes(UserOrganisationId, UserCentreId, id.Value);
            return File(data, ".pdf", "Registration Reciept.pdf");
        }
    }
}