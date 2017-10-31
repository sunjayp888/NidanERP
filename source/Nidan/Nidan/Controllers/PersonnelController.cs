using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Attributes;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Entity.Extensions;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;
using SharedTypes.DataContracts;

namespace Nidan.Controllers
{
    public class PersonnelController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        private readonly IEmailService _emailService;
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

        public PersonnelController(INidanBusinessService hrBusinessService, IEmailService emailService) : base(hrBusinessService)
        {
            _emailService = emailService;
        }

        // GET: Personnel
        [AuthorizePersonnel(Roles = "Admin,User,SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Personnel/Profile/{id}
        [AuthorizePersonnel(Roles = "Admin,User,SuperAdmin")]
        public new ActionResult Profile(int id)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var personnel = NidanBusinessService.RetrievePersonnel(UserOrganisationId, id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            var isAdmin = User.IsInAnyRoles("Admin");
            List<int> selectedCompanyIds = null;
            List<int> selectedDepartmentIds = null;
            List<int> selectedDivisionIds = null;
            if (!isAdmin)
            {
            }
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnel,
                Permissions = NidanBusinessService.RetrievePersonnelPermissions(isAdmin, UserOrganisationId, UserPersonnelId, id),
                //PhotoBytes = NidanBusinessService.RetrievePhoto(organisationId, id)
            };
            return View(viewModel);
        }


        // GET: Personnel/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var roles = NidanBusinessService.RetrieveAspNetRoles(organisationId, e => true);
            var viewModel = new PersonnelProfileViewModel
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                Roles= new SelectList(roles,"Id","Name")
            };
            return View(viewModel);
        }

        // POST: Personnel/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelProfileViewModel personnelViewModel)
        {
            // check if user with this email already exists for the current organisation
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var userExists = UserManager.FindByEmail(personnelViewModel.Personnel.Email);
            var roleId = NidanBusinessService.RetrieveAspNetRoles(organisationId, e => e.Id == personnelViewModel.Role).FirstOrDefault().Name;

            personnelViewModel.Centres = new SelectList(centres, "CentreId", "Name");
            if (userExists != null)
                ModelState.AddModelError("", string.Format("An account already exists for the email address {0}", personnelViewModel.Personnel.Email));

            if (ModelState.IsValid)
            {
                //Create Personnel
                personnelViewModel.Personnel.CentreId = personnelViewModel.Personnel.CentreId == 0 ? UserCentreId : personnelViewModel.Personnel.CentreId;
                personnelViewModel.Personnel = NidanBusinessService.CreatePersonnel(UserOrganisationId, personnelViewModel.Personnel);
                //var trainer =  NidanBusinessService.CreateTrainer
                //create personnel
                //var personnel = new Personnel(){set all mandatory field like forename }
                //CreateTrainerUserAndRole(personnel)
                var result = CreateUserAndRole(personnelViewModel.Personnel, roleId);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                //delete the orphaned personnel & employment records
                NidanBusinessService.DeletePersonnel(UserOrganisationId, personnelViewModel.Personnel.PersonnelId);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(personnelViewModel);
        }

        private IdentityResult CreateUserAndRole(Personnel personnel, string role)
        {
            var createUser = new ApplicationUser
            {
                UserName = personnel.Email,
                Email = personnel.Email,
                OrganisationId = UserOrganisationId,
                PersonnelId = personnel.PersonnelId,
                CentreId = personnel.CentreId
            };

            var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == role).Id;
            createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });
            var password = string.Format("{0}{1}@", personnel.Forenames.ToUpper().First(), Guid.NewGuid().ToString().Substring(30));
            var result = UserManager.Create(createUser, password);
            if (result.Succeeded)
            {
                //send email
                var emailData = new EmailData()
                {
                    BCCAddressList = new List<string> { "developer@nidantech.com" },
                    Body = String.Format("Dear {0}.{1} {2}, Your Login Details For Nidan ERP Portal : User Id = {3} and Password = {4}",personnel.Title,personnel.Forenames,personnel.Surname,personnel.Email,password),
                    Subject = "Login Details For NidanERP",
                    IsHtml = true,
                    ToAddressList = new List<string> { personnel.Email }

                };
                _emailService.SendEmail(emailData);
            }

            return result;
        }

        //private IdentityResult CreateTrainerUserAndRole(Personnel personnel)
        //{
        //    var createUser = new ApplicationUser
        //    {
        //        UserName = personnel.Email,
        //        Email = personnel.Email,
        //        OrganisationId = UserOrganisationId,
        //        PersonnelId = personnel.PersonnelId,
        //        CentreId = personnel.CentreId
        //    };

        //    var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
        //    createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

        //    var result = UserManager.Create(createUser, "Password1!");
        //    return result;
        //}

        // GET: Personnel/Edit/{id}
        [AuthorizePersonnel(Roles = "Admin,User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personnel = NidanBusinessService.RetrievePersonnel(UserOrganisationId, id.Value);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            var centres = NidanBusinessService.RetrieveCentres(UserOrganisationId, e => true);

            personnel.Email = UserManager.FindByPersonnelId(personnel.PersonnelId)?.Email;
            var viewModel = new PersonnelProfileViewModel
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                Personnel = personnel
            };
            return View(viewModel);
        }

        // POST: Personnels/Edit/{id}
        [AuthorizePersonnel(Roles = "Admin,User,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelProfileViewModel personnelViewModel)
        {
            if (ModelState.IsValid)
            {

                personnelViewModel.Personnel = NidanBusinessService.UpdatePersonnel(UserOrganisationId, personnelViewModel.Personnel);

                var editUser = UserManager.FindByPersonnelId(personnelViewModel.Personnel.PersonnelId);
                editUser.Email = personnelViewModel.Personnel.Email;

                var result = UserManager.Update(editUser);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnelViewModel.Personnel
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadPhoto(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {

                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(file.ContentLength);
                        }
                        //NidanBusinessService.UploadPhoto(UserOrganisationId, id.Value,  fileData);
                    }
                }
                return this.JsonNet("");
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }

        }

        [HttpPost]
        public ActionResult DeletePhoto(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //NidanBusinessService.DeletePhoto(UserOrganisationId, id.Value);
                return this.JsonNet("");
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }

        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrievePersonnels(UserOrganisationId, e => true, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult AdmissionList(int personnelId)
        {
            var organisationId = UserOrganisationId;
            var personnel = NidanBusinessService.RetrievePersonnel(organisationId, personnelId);
            var admissionId = personnel.Admissions.FirstOrDefault()?.AdmissionId ?? 0;
            var admissionData = NidanBusinessService.RetrieveAdmissionGrid(organisationId, e => e.AdmissionId == admissionId);
            return this.JsonNet(admissionData);
        }

        [HttpPost]
        public ActionResult BatchList(int personnelId)
        {
            var organisationId = UserOrganisationId;
            var personnel = NidanBusinessService.RetrievePersonnel(organisationId, personnelId);
            var batchId = personnel.Admissions.FirstOrDefault()?.BatchId ?? 0;
            var batchData = NidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId == UserCentreId && e.BatchId == batchId, null, null);
            return this.JsonNet(batchData);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrievePersonnelBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }


        //private List<string> RetrieveRoles()
        //{
        //    return Enum.GetValues(typeof(Role))
        //   .Cast<Role>()
        //   .Select(v => v.ToString())
        //   .ToList();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}