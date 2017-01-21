using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Attributes;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;

namespace Nidan.Controllers
{
    [Authorize]
    public class PersonnelController : BaseController
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

        public PersonnelController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }

        // GET: Personnel
        [AuthorizePersonnel(Roles = "Admin,User")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Personnel/Profile/{id}
        [AuthorizePersonnel(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = new Personnel
                {
                    OrganisationId = UserOrganisationId,
                    DOB = DateTime.Today,
                    Title = "Mr",
                    Forenames = "A",
                    Surname = "B",
                    Email = string.Format("{0}@hr.com", Guid.NewGuid()),
                    Address1 = "Address1",
                    Postcode = "POST CODE",
                    Telephone = "12345678",
                    NINumber = "NZ1234567",
                },
            };
            return View(viewModel);
        }

        // POST: Personnel/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelProfileViewModel personnelViewModel)
        {
            // check if user with this email already exists for the current organisation
            var userExists = UserManager.FindByEmail(personnelViewModel.Personnel.Email);
            if (userExists != null)
                ModelState.AddModelError("", string.Format("An account already exists for the email address {0}", personnelViewModel.Personnel.Email));

            if (ModelState.IsValid)
            {
                //Create Personnel               
                personnelViewModel.Personnel = NidanBusinessService.CreatePersonnel(UserOrganisationId, personnelViewModel.Personnel);

                var result = CreateUserAndRole(personnelViewModel.Personnel);
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

        private IdentityResult CreateUserAndRole(Personnel personnel)
        {
            var createUser = new ApplicationUser
            {
                UserName = personnel.Email,
                Email = personnel.Email,
                OrganisationId = UserOrganisationId,
                PersonnelId = personnel.PersonnelId
            };

            var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "User").Id;
            createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

            var result = UserManager.Create(createUser, "Password1!");
            return result;
        }

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
            personnel.Email = UserManager.FindByPersonnelId(personnel.PersonnelId)?.Email;
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnel
            };
            return View(viewModel);
        }

        // POST: Personnels/Edit/{id}
        [AuthorizePersonnel(Roles = "Admin,User")]
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
            catch(Exception ex)
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
            return this.JsonNet(NidanBusinessService.RetrievePersonnel(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrievePersonnelBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }


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