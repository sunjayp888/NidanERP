using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Interfaces;
using Nidan.Data;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;
using Nidan.Business;
using Nidan.Document;
using Nidan.Document.Interfaces;

namespace Nidan.Controllers
{
    [Authorize]
    public class TrainerController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly IDocumentService _documentService;
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

        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public TrainerController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
            _documentService = documentService;
        }

        // GET: Trainer
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:trainer/create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var viewModel = new TrainerViewModel
            {
                Trainer = new Trainer(),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name")
            };
            return View(viewModel);
        }

        // POST: trainer/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerViewModel trainerViewModel)
        {
            var organisationId = UserOrganisationId;
            trainerViewModel.Trainer.CreatedDate = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                trainerViewModel.Trainer.OrganisationId = UserOrganisationId;
                trainerViewModel.Trainer.CentreId = UserCentreId;
                trainerViewModel.Trainer = NidanBusinessService.CreatetTrainer(UserOrganisationId, trainerViewModel.Trainer);
                var personnel = new Personnel()
                {
                    OrganisationId = organisationId,
                    DOB = DateTime.Today,
                    Title = "Mr",
                    Forenames = trainerViewModel.Trainer.Name,
                    Surname = "Surname",
                    Email = trainerViewModel.Trainer.EmailId,
                    Address1 = "Address1",
                    Postcode = "POST CODE",
                    Telephone = "12345678",
                    NINumber = "NZ1234567",
                    CentreId = trainerViewModel.Trainer.CentreId
                   
                };
                NidanBusinessService.CreatePersonnel(organisationId, personnel);
                trainerViewModel.Trainer.PersonnelId = personnel.PersonnelId;
                NidanBusinessService.UpdateTrainer(organisationId, trainerViewModel.Trainer);
                CreateTrainerUserAndRole(personnel);
                return RedirectToAction("Edit", new { id = trainerViewModel.Trainer.TrainerId });
            }
            trainerViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            trainerViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            return View(trainerViewModel);
        }

        private IdentityResult CreateTrainerUserAndRole(Personnel personnel)
        {
            var createUser = new ApplicationUser
            {
                UserName = personnel.Email,
                Email = personnel.Email,
                OrganisationId = UserOrganisationId,
                PersonnelId = personnel.PersonnelId,
                CentreId = personnel.CentreId
            };
            var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
            createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

            var result = UserManager.Create(createUser, "Password1!");
            return result;
        }

        // GET: trainer/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["TrainerId"] = id;
            var trainer = NidanBusinessService.RetrieveTrainer(UserOrganisationId, id.Value);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TrainerViewModel
            {
                Trainer = trainer,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name")
            };
            return View(viewModel);
        }

        // POST: trainer/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                trainerViewModel.Trainer.OrganisationId = UserOrganisationId;
                trainerViewModel.Trainer.CentreId = UserCentreId;
                trainerViewModel.Trainer = NidanBusinessService.UpdateTrainer(UserOrganisationId, trainerViewModel.Trainer);
                return RedirectToAction("Index");
            }
            var viewModel = new TrainerViewModel
            {
                Trainer = trainerViewModel.Trainer
            };
            return View(viewModel);
        }

        public ActionResult Upload(int id)
        {

            var viewModel = new TrainerViewModel
            {
                TrainerId = Convert.ToInt32(TempData["TrainerId"]),
                Files = new List<HttpPostedFileBase>(),
                DocumentTypes = new SelectList(NidanBusinessService.RetrieveDocumentTypes(UserOrganisationId), "DocumentTypeId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(TrainerViewModel trainerViewModel)
        {
            trainerViewModel.DocumentTypes = new SelectList(NidanBusinessService.RetrieveDocumentTypes(UserOrganisationId),
                "DocumentTypeId", "Name");

            if (trainerViewModel.Files != null)
            {
                if (trainerViewModel.Files != null && trainerViewModel.Files[0].ContentLength > 0)
                {
                    var trainerData = _nidanBusinessService.RetrieveTrainer(UserOrganisationId, trainerViewModel.TrainerId);

                    if (trainerViewModel.Files[0].FileName.EndsWith(".pdf"))
                    {
                        _documentService.Create(UserOrganisationId, UserCentreId,
                            trainerViewModel.Document.DocumentTypeId, trainerData.TrainerId.ToString(),
                            trainerData.Name, "Trainer Document", trainerViewModel.Files[0].FileName,
                            trainerViewModel.Files[0].InputStream.ToBytes());
                    }
                    else
                    {
                        ModelState.AddModelError("FileFormat", "This file format is not supported");
                        return View(trainerViewModel);
                    }
                    return RedirectToAction("Edit", new { id = trainerData.TrainerId });
                }
                ModelState.AddModelError("", "Please Upload Your file");
            }
            return View(trainerViewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveTrainers(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, e => e.Sector.SectorId == sectorId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveTrainerBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }

    }
}