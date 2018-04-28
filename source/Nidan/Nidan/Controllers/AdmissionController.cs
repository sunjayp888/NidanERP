using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.Provider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Enum;
using Nidan.Business.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;

namespace Nidan.Controllers
{
    public class AdmissionController : BaseController
    {
        private readonly IDocumentService _documentService;
        private readonly INidanBusinessService _nidanBusinessService;
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month, DateTime.UtcNow.Date.Day, 0, 0, 0);
        public AdmissionController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _documentService = documentService;
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: Admission
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        public ActionResult TodaysAdmission()
        {
            return View(new BaseViewModel());
        }

        // GET: Admission/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == UserCentreId);
            var courseInstallments = NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true);
            var registration = NidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId == UserCentreId).Where(e => e.CourseId == registration.CourseId);
            var candidateFee = _nidanBusinessService.RetrieveCandidateFees(organisationId, e => e.StudentCode == registration.StudentCode);
            var totalRegistrationAmount = candidateFee.Items.Where(e => e.FeeTypeId == 1 || e.FeeTypeId == 6).Sum(e => e.PaidAmount);
            registration.CandidateInstallment.DownPayment = registration.CandidateInstallment.DownPayment <= totalRegistrationAmount
                                                            ? 0 : (registration.CandidateInstallment.DownPayment - totalRegistrationAmount);
            var viewModel = new AdmissionViewModel
            {
                LumpsumAfterRegistration = registration.CandidateInstallment.LumpsumAmount - totalRegistrationAmount,
                Course = new Course { Name = "Test" },
                CourseInstallment = new CourseInstallment { Name = "Test" },
                Batch = new Batch { Name = "Test" },
                BatchDay = new BatchDay { BatchDayId = 0 },
                Registration = registration,
                RegistrationId = id.Value,
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name"),
                Rooms = new SelectList(rooms, "RoomId", "Description"),
                CourseInstallments = new SelectList(courseInstallments, "CourseInstallmentId", "Name"),
                TotalRegistrationAmount = totalRegistrationAmount.Value,
                Admission = new Admission()
                {
                    Registration = registration,
                    RegistrationId = id.Value
                }
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.DiscountList = new SelectList(viewModel.DiscountType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Admission/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var registrationData = NidanBusinessService.RetrieveRegistration(organisationId, admissionViewModel.Admission.RegistrationId);
            var enquiryId = registrationData.EnquiryId;
            var enquiryData = NidanBusinessService.RetrieveEnquiry(organisationId, enquiryId);
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission = NidanBusinessService.CreateAdmission(organisationId, centreId, personnelId, admissionViewModel.Admission, admissionViewModel.CandidateFee);
                // Create Personnel
                var personnel = Personnel(organisationId, enquiryData);
                admissionViewModel.Admission.PersonnelId = personnel.PersonnelId;
                admissionViewModel.Admission.Batch = null;
                admissionViewModel.Admission.Registration = null;
                admissionViewModel.Admission.CreatedBy = personnelId;
                NidanBusinessService.UpdateAdmission(organisationId, centreId, personnelId, admissionViewModel.Admission);
                CreateCandidateUserAndRole(personnel);
                return RedirectToAction("Index");
            }
            admissionViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name");
            admissionViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList(), "PaymentModeId", "Name");
            admissionViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList(), "SchemeId", "Name");
            admissionViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            admissionViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList(), "BatchTimePreferId", "Name");
            admissionViewModel.Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => e.CourseId == registrationData.CourseId).ToList(), "BatchId", "Name");
            admissionViewModel.Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == UserCentreId).ToList(), "RoomId", "Description");
            admissionViewModel.CourseInstallments = new SelectList(NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true).ToList());
            return View(admissionViewModel);
        }

        private Personnel Personnel(int organisationId, Enquiry enquiryData)
        {
            var personnel = new Personnel()
            {
                OrganisationId = organisationId,
                Title = enquiryData.Title,
                Forenames = enquiryData.FirstName,
                Surname = enquiryData.LastName,
                DOB = enquiryData.DateOfBirth ?? DateTime.UtcNow,
                Address1 = enquiryData.Address1,
                Address2 = enquiryData.Address2,
                Address3 = enquiryData.Address3,
                Address4 = enquiryData.Address4,
                Postcode = enquiryData.PinCode.ToString(),
                Mobile = enquiryData.Mobile.ToString(),
                Email = enquiryData.EmailId,
                Telephone = "12345678",
                NINumber = "NZ1234567",
                CentreId = enquiryData.CentreId
            };
            NidanBusinessService.CreatePersonnel(organisationId, personnel);
            return personnel;
        }

        private IdentityResult CreateCandidateUserAndRole(Personnel personnel)
        {
            var createUser = new ApplicationUser
            {
                UserName = personnel.Email,
                Email = personnel.Email,
                OrganisationId = UserOrganisationId,
                PersonnelId = personnel.PersonnelId,
                CentreId = personnel.CentreId
            };

            var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "User").Id;
            createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

            var result = UserManager.Create(createUser, "Password1!");
            return result;
        }

        // GET: Admission/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, centreId, id.Value);


            if (admission == null)
            {
                return HttpNotFound();
            }
            var candidateFeeByAdmission = NidanBusinessService.RetrieveCandidateFees(organisationId, e => e.StudentCode == admission.Registration.StudentCode && e.FeeTypeId == (int)Business.Enum.FeeType.Admission).Items.FirstOrDefault();

            var viewModel = new AdmissionViewModel
            {
                Course = new Course()
                {
                    Name = "Test"
                },
                CourseInstallment = new CourseInstallment()
                {
                    Name = "Test"
                },
                Batch = new Batch()
                {
                    Name = "Test"
                },
                Registration = admission.Registration,
                RegistrationId = admission.RegistrationId,
                Admission = admission,
                CandidateFee = candidateFeeByAdmission ?? admission.Registration.CandidateFee,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name"),
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList(), "PaymentModeId", "Name"),
                Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList(), "SchemeId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name"),
                BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList(), "BatchTimePreferId", "Name"),
                Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList(), "BatchId", "Name"),
                Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == UserCentreId).ToList(), "RoomId", "Description"),
                CourseInstallments = new SelectList(NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true).ToList(), "CourseInstallmentId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.DiscountList = new SelectList(viewModel.DiscountType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Admission/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission.OrganisationId = organisationId;
                admissionViewModel.Admission.CentreId = centreId;
                admissionViewModel.Admission.PersonnelId = personnelId;
                admissionViewModel.Admission.CreatedBy = personnelId;
                admissionViewModel.Admission = NidanBusinessService.UpdateAdmission(organisationId, centreId, personnelId, admissionViewModel.Admission);
                return RedirectToAction("Index");
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admissionViewModel.Admission
            };
            return View(viewModel);
        }

        // GET: Admission/AssignBatch/{id}
        public ActionResult AssignBatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centreId = UserCentreId;
            var organisationId = UserOrganisationId;
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, centreId, id.Value);


            if (admission == null)
            {
                return HttpNotFound();
            }
            var candidateFeeByAdmission = NidanBusinessService.RetrieveCandidateFees(organisationId, e => e.StudentCode == admission.Registration.StudentCode && e.FeeTypeId == (int)Business.Enum.FeeType.Admission).Items.FirstOrDefault();
            var viewModel = new AdmissionViewModel
            {
                Course = new Course()
                {
                    Name = "Test"
                },
                CourseInstallment = new CourseInstallment()
                {
                    Name = "Test"
                },
                Batch = new Batch()
                {
                    Name = "Test"
                },
                Registration = admission.Registration,
                RegistrationId = admission.RegistrationId,
                Admission = admission,
                CandidateFee = candidateFeeByAdmission ?? admission.Registration.CandidateFee,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name"),
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList(), "PaymentModeId", "Name"),
                Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList(), "SchemeId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name"),
                BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList(), "BatchTimePreferId", "Name"),
                Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => e.CourseId == admission.Registration.CourseId).ToList(), "BatchId", "Name"),
                Rooms = new SelectList(NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == UserCentreId).ToList(), "RoomId", "Description"),
                CourseInstallments = new SelectList(NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true).ToList(), "CourseInstallmentId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.DiscountList = new SelectList(viewModel.DiscountType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Admission/AssignBatch/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignBatch(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission.OrganisationId = organisationId;
                admissionViewModel.Admission.CentreId = centreId;
                admissionViewModel.Admission.PersonnelId = personnelId;
                admissionViewModel.Admission.CreatedBy = personnelId;
                NidanBusinessService.AssignBatch(organisationId, centreId, personnelId, admissionViewModel.Admission);
                return RedirectToAction("Index");
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admissionViewModel.Admission
            };
            return View(viewModel);
        }

        // GET: Admission/View/{id}
        public ActionResult View(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var admission = !isSuperAdmin ? NidanBusinessService.RetrieveAdmission(organisationId, centreId, id.Value) :
                             NidanBusinessService.RetrieveAdmission(organisationId, id.Value, e => true);
            var admissionGrid = _nidanBusinessService.RetrieveAdmissionGrid(organisationId, id.Value, e => true);
            var candidateFee = _nidanBusinessService.RetrieveCandidateFees(organisationId, e => e.StudentCode == admissionGrid.StudentCode && (e.FeeTypeId == 1 || e.FeeTypeId == 6));
            var totalRegistrationAmount = candidateFee.Items.Sum(e => e.PaidAmount);
            if (admission == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admission,
                PaidAmount = admissionGrid.PaidAmount.Value,
                TotalRegistrationAmount = totalRegistrationAmount.Value
            };
            return View(viewModel);
        }

        // GET: Admission/OtherFee/{id}
        public ActionResult OtherFee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var admission = _nidanBusinessService.RetrieveAdmission(organisationId, centreId, id.Value);
            var registration = _nidanBusinessService.RetrieveRegistration(organisationId, centreId, admission.RegistrationId);
            var paymentModes = _nidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var feeTypes = _nidanBusinessService.RetrieveFeeTypes(organisationId, e => e.FeeTypeId ==5);
            var admissionGrid = _nidanBusinessService.RetrieveAdmissionGrid(organisationId, e => e.AdmissionId == id.Value).Items.FirstOrDefault();
            if (registration == null)
            {
                return HttpNotFound();
            }
            var interestedCourseIds = registration.Enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCourses(organisationId, p => true).Where(e => interestedCourseIds.Contains(e.CourseId));
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, registration.EnquiryId);
            var counsellingData = NidanBusinessService.RetrieveCounsellings(organisationId, e => e.EnquiryId == enquiry.EnquiryId).Items.FirstOrDefault();
            var counsellingCourse = NidanBusinessService.RetrieveCourses(organisationId, e => true).Where(e => e.CourseId == counsellingData?.CourseOfferedId);
            var viewModel = new AdmissionViewModel()
            {
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                EnquiryId = enquiry.EnquiryId,
                Registration = registration,
                CounsellingCourse = new SelectList(counsellingCourse, "CourseId", "Name"),
                FeeTypes = new SelectList(feeTypes, "FeeTypeId", "Name"),
                Enquiry = enquiry,
                Course = registration.Course,
                CandidateInstallment = registration.CandidateInstallment,
                CourseInstallment = registration.CourseInstallment,
                PaidAmount = admissionGrid.PaidAmount.Value,
                CandidateInstallmentId=registration.CandidateInstallmentId
            };
            return View(viewModel);
        }

        // POST: Admission/OtherFee
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherFee(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                admissionViewModel.Registration.EnquiryId = admissionViewModel.EnquiryId;
                admissionViewModel.CandidateFee.CentreId = centreId;
                admissionViewModel.CandidateFee.PersonnelId = personnelId;
                admissionViewModel.CandidateFee.CandidateInstallmentId = admissionViewModel.Registration.CandidateInstallmentId;
                admissionViewModel.CandidateFee.StudentCode = admissionViewModel.Registration.StudentCode;
                admissionViewModel.CandidateFee = _nidanBusinessService.CreateCandidateFee(organisationId, admissionViewModel.CandidateFee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAdmissionGrid(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult TodaysAdmissionList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAdmissionGrid(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.AdmissionDate == _today, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveAdmissionDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBatchDetails(int batchId)
        {
            var data = NidanBusinessService.RetrieveBatch(UserOrganisationId, batchId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAdmissionGrid(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.AdmissionDate >= fromDate && p.AdmissionDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAdmissionBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        public ActionResult Download(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var admission = !isSuperAdmin ? NidanBusinessService.RetrieveAdmission(organisationId, centreId, id.Value) :
                NidanBusinessService.RetrieveAdmission(organisationId, id.Value, e => true);
            var data = NidanBusinessService.CreateEnrollmentBytes(organisationId, centreId, admission);
            return File(data, ".pdf", string.Format("{0} {1} Enrollment.pdf", admission.Registration.Enquiry.FirstName, admission.Registration.Enquiry.LastName));
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var studentData = NidanBusinessService.RetrieveEnquiries(organisationId, e => e.CentreId == centreId && e.StudentCode == documentViewModel.StudentCode).ToList().FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           studentData?.FirstName, "Admission Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }

        [HttpPost]
        public ActionResult RetrieveCandidateFees(int candidateInstallmentId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateFees(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CandidateInstallmentId == candidateInstallmentId && p.FeeTypeId!=3, orderBy, paging);
            return this.JsonNet(data);
        }

        //  [HttpPost]
        public ActionResult DownloadOtherFee(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var admission = new Admission();
            var candidateFee = NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value);
            var feeTypeId = candidateFee.FeeTypeId;
            string firstName = "";
            string lastName = "";
            Business.Enum.FeeType feeType = (Business.Enum.FeeType)feeTypeId;
            if (feeTypeId == 2)
            {
                var registration = NidanBusinessService.RetrieveRegistrations(organisationId, e => e.StudentCode == candidateFee.StudentCode).Items.FirstOrDefault();
                var admissionData = NidanBusinessService.RetrieveAdmissions(organisationId, e => e.RegistrationId == registration.RegistrationId).Items.FirstOrDefault();
                firstName = admissionData?.Registration.Enquiry.FirstName;
                lastName = admissionData?.Registration.Enquiry.LastName;
                admission = admissionData;
            }
            var data = feeTypeId == 1 || feeTypeId == 3 || feeTypeId == 4 || feeTypeId == 5 || feeTypeId == 6 ? NidanBusinessService.CreateRegistrationRecieptBytes(organisationId, centreId, id.Value)
                : NidanBusinessService.CreateEnrollmentBytes(organisationId, centreId, admission);
            return File(data, ".pdf", string.Format("{0} {1} {2}.pdf", firstName, lastName, feeType.ToString()));
        }
    }
}