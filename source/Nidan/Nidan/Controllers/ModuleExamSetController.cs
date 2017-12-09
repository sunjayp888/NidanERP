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
    public class ModuleExamSetController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public ModuleExamSetController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: ModuleExamSet
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:ModuleExamSet/create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var subjects = NidanBusinessService.RetrieveSubjects(organisationId, e => true);
            var viewModel = new ModuleExamSetViewModel()
            {
                ModuleExamSet = new ModuleExamSet(),
                Subjects = new SelectList(subjects, "SubjectId", "Name")
            };
            return View(viewModel);
        }

        // POST: ModuleExamSet/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleExamSetViewModel moduleExamSetViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                moduleExamSetViewModel.ModuleExamSet.OrganisationId = organisationId;
                moduleExamSetViewModel.ModuleExamSet.CreatedBy = personnelId;
                moduleExamSetViewModel.ModuleExamSet = NidanBusinessService.CreateModuleExamSet(UserOrganisationId, moduleExamSetViewModel.ModuleExamSet);
                return RedirectToAction("Index");
            }
            moduleExamSetViewModel.Subjects = new SelectList(NidanBusinessService.RetrieveSubjects(organisationId, e => true).ToList());
            return View(moduleExamSetViewModel);
        }

        // GET: ModuleExamSet/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            //var personnelId = UserPersonnelId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var moduleExamSet = NidanBusinessService.RetrieveModuleExamSet(UserOrganisationId, id.Value, e => true);
            var questionTypes = NidanBusinessService.RetrieveQuestionTypes(organisationId, e => true);
            var subjects = NidanBusinessService.RetrieveSubjects(organisationId, e => true);
            if (moduleExamSet == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ModuleExamQuestionSetViewModel()
            {
                ModuleExamSet = moduleExamSet,
                ModuleExamSetId = moduleExamSet.ModuleExamSetId,
                QuestionTypes = new SelectList(questionTypes, "QuestionTypeId", "Name"),
                Subjects = new SelectList(subjects, "SubjectId", "Name"),
                ModuleExamQuestionSet = new ModuleExamQuestionSet()
                {
                    ModuleExamSetId = moduleExamSet.ModuleExamSetId
                }
            };
            return View(viewModel);
        }

        // POST: ModuleExamSet/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModuleExamQuestionSetViewModel moduleExamQuestionSetViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                moduleExamQuestionSetViewModel.ModuleExamQuestionSet.OrganisationId = organisationId;
                moduleExamQuestionSetViewModel.ModuleExamQuestionSet.CreatedBy = personnelId;
                moduleExamQuestionSetViewModel.ModuleExamQuestionSet = NidanBusinessService.CreateModuleExamQuestionSet(UserOrganisationId, moduleExamQuestionSetViewModel.ModuleExamQuestionSet);
                return RedirectToAction("Edit", "ModuleExamSet", new { id = moduleExamQuestionSetViewModel.ModuleExamSet.ModuleExamSetId });
            }
            var viewModel = new ModuleExamQuestionSetViewModel()
            {
                ModuleExamQuestionSet = moduleExamQuestionSetViewModel.ModuleExamQuestionSet
            };
            moduleExamQuestionSetViewModel.Subjects = new SelectList(NidanBusinessService.RetrieveSubjects(organisationId, e => true).ToList(), "SubjectId", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveModuleExamSets(UserOrganisationId, e => true, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult ModuleExamQuestion(int moduleExamSetId, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveModuleExamQuestionSetGrid(UserOrganisationId, e => e.ModuleExamSetId == moduleExamSetId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}