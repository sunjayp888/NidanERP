using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;

namespace Nidan.Controllers
{
    public class AdmissionController : BaseController
    {
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

        public AdmissionController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Admission
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Admission/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true);
            var rooms = NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId == UserCentreId);
            var courseInstallments = NidanBusinessService.RetrieveCourseInstallments(organisationId, e => true);
            var registration = NidanBusinessService.RetrieveRegistration(organisationId, id.Value);
            var viewModel = new AdmissionViewModel
            {
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdmissionViewModel admissionViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var registrationData = NidanBusinessService.RetrieveRegistration(organisationId,admissionViewModel.Admission.RegistrationId);
            var enquiryId = registrationData.EnquiryId;
            var enquiryData = NidanBusinessService.RetrieveEnquiry(organisationId, enquiryId);
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission = NidanBusinessService.CreateAdmission(organisationId, centreId, admissionViewModel.Admission);
                // Create Personnel
                var personnel = Personnel(organisationId, enquiryData);
                admissionViewModel.Admission.PersonnelId = personnel.PersonnelId;
                NidanBusinessService.UpdateAdmission(organisationId, admissionViewModel.Admission);
                CreateCandidateUserAndRole(personnel);
                return RedirectToAction("Index");
            }
            admissionViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name");
            admissionViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList(), "PaymentModeId", "Name");
            admissionViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList(), "SchemeId", "Name");
            admissionViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            admissionViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList(), "BatchTimePreferId", "Name");
            admissionViewModel.Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList(), "BatchId", "Name");
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
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, id.Value);

            if (admission == null)
            {
                return HttpNotFound();
            }
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
                RegistrationId = id.Value,
                Admission = admission,
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
            if (ModelState.IsValid)
            {
                admissionViewModel.Admission.OrganisationId = organisationId;
                admissionViewModel.Admission.CentreId = UserCentreId;
                admissionViewModel.Admission = NidanBusinessService.UpdateAdmission(organisationId, admissionViewModel.Admission);
                return RedirectToAction("Index");
            }
            var viewModel = new AdmissionViewModel
            {
                Admission = admissionViewModel.Admission
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveAdmissions(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetBatchDetails(int batchId)
        {
            var data = NidanBusinessService.RetrieveBatch(UserOrganisationId, batchId);
            return this.JsonNet(data);
        }
    }
}