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
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            //var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var viewModel = new TrainerViewModel
            {
                Trainer = new Trainer(),
                //Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            return View(viewModel);
        }

        // POST: trainer/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainerViewModel trainerViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                trainerViewModel.Trainer.OrganisationId = UserOrganisationId;
                trainerViewModel.Trainer.CentreId = UserCentreId;
                trainerViewModel.Trainer.EmailId = trainerViewModel.Trainer.EmailId.ToLower();
                trainerViewModel.Trainer = NidanBusinessService.CreatetTrainer(UserOrganisationId, trainerViewModel.Trainer);
                var personnel = new Personnel()
                {
                    OrganisationId = organisationId,
                    DOB = trainerViewModel.Trainer.DateOfBirth,
                    Title = trainerViewModel.Trainer.Title,
                    Forenames = trainerViewModel.Trainer.FirstName,
                    Surname = trainerViewModel.Trainer.LastName,
                    Email = trainerViewModel.Trainer.EmailId,
                    Address1 = trainerViewModel.Trainer.Address1,
                    Address2 = trainerViewModel.Trainer.Address2,
                    Address3 = trainerViewModel.Trainer.Address3,
                    Address4 = trainerViewModel.Trainer.Address4,
                    Postcode = trainerViewModel.Trainer.PinCode,
                    Mobile = trainerViewModel.Trainer.Mobile.ToString(),
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
            //trainerViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            trainerViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            trainerViewModel.Talukas =new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            trainerViewModel.Districts =new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            trainerViewModel.States =new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
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
            var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Trainer").Id;
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
                //Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name"),
                Talukas = new SelectList(NidanBusinessService.RetrieveTalukas(UserOrganisationId, e => true).ToList(), "TalukaId", "Name"),
                Districts = new SelectList(NidanBusinessService.RetrieveDistricts(UserOrganisationId, e => true).ToList(), "DistrictId", "Name"),
                States = new SelectList(NidanBusinessService.RetrieveStates(UserOrganisationId, e => true).ToList(), "StateId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
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
                trainerViewModel.Trainer.EmailId = trainerViewModel.Trainer.EmailId.ToLower();
                trainerViewModel.Trainer = NidanBusinessService.UpdateTrainer(UserOrganisationId, trainerViewModel.Trainer);
                return RedirectToAction("Index");
            }
            var viewModel = new TrainerViewModel
            {
                Trainer = trainerViewModel.Trainer
            };
            return View(viewModel);
        }

        // GET: Trainer/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var trainer = NidanBusinessService.RetrieveTrainer(organisationId, id.Value);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TrainerViewModel
            {
                Trainer = trainer
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveTrainers(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetTaluka(int districtId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveTalukas(UserOrganisationId, e => e.District.DistrictId == districtId).ToList());
        }

        [HttpPost]
        public ActionResult GetDistrict(int stateId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveDistricts(UserOrganisationId, e => e.State.StateId == stateId).ToList());
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveTrainerBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveTrainerDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var trainerData = NidanBusinessService.RetrieveTrainers(organisationId, e => e.CentreId == centreId && e.TrainerId.ToString() == documentViewModel.StudentCode).ToList().FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                trainerData?.FirstName, "Trainer Document", documentViewModel.Attachment.FileName,
                documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }
    }
}