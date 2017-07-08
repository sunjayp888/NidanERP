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
    public class ModuleController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        public ModuleController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: Module
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Module/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var viewModel = new ModuleViewModel
            {
                Module = new Module(),
                Courses = new SelectList(courses, "CourseId", "Name")
            };

            return View(viewModel);
        }

        // POST: Module/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleViewModel moduleViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                moduleViewModel.Module.OrganisationId = organisationId;
                moduleViewModel.Module = NidanBusinessService.CreateModule(organisationId, moduleViewModel.Module);
                return RedirectToAction("Index");
            }
            moduleViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            return View(moduleViewModel);
        }

        // GET: Module/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var module = NidanBusinessService.RetrieveModule(UserOrganisationId, id.Value);

            if (module == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ModuleViewModel
            {
                Module = module,
                Courses = new SelectList(courses, "CourseId", "Name")
            };
            return View(viewModel);
        }

        // POST: Module/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModuleViewModel moduleViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                moduleViewModel.Module.OrganisationId = organisationId;
                moduleViewModel.Module = NidanBusinessService.UpdateModule(organisationId, moduleViewModel.Module);
                return RedirectToAction("Index");
            }
            var viewModel = new ModuleViewModel
            {
                Module = moduleViewModel.Module
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("Admin");
            return this.JsonNet(NidanBusinessService.RetrieveModules(UserOrganisationId, p => (isSuperAdmin), orderBy, paging));
        }
    }
}