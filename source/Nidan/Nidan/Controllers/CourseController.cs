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
    public class CourseController : BaseController
    {
        private ApplicationRoleManager _roleManager;

        public CourseController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Course
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Course/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var courseTypes = NidanBusinessService.RetrieveCourseTypes(organisationId, e => true);
            var viewModel = new CourseViewModel
            {
                Course = new Course(),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                CourseTypes = new SelectList(courseTypes, "CourseTypeId", "Name")
            };
            return View(viewModel);
        }

        // POST: Course/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel courseViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                courseViewModel.Course.OrganisationId = UserOrganisationId;
                courseViewModel.Course = NidanBusinessService.CreateCourse(UserOrganisationId, courseViewModel.Course);
                return RedirectToAction("Edit", new { id = courseViewModel.Course.CourseId });
            }
            courseViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            courseViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            courseViewModel.CourseTypes = new SelectList(NidanBusinessService.RetrieveCourseTypes(organisationId, e => true).ToList());
            return View(courseViewModel);
        }

        // GET: Course/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var schemes = NidanBusinessService.RetrieveSchemes(organisationId, e => true);
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var courseTypes = NidanBusinessService.RetrieveCourseTypes(organisationId, e => true);
            var course = NidanBusinessService.RetrieveCourse(organisationId, id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            TempData["CourseId"] = id;

            var viewModel = new CourseViewModel
            {
                Course = course,
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                CourseTypes = new SelectList(courseTypes, "CourseTypeId", "Name")
            };
            return View(viewModel);
        }

        // POST: Course/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                courseViewModel.Course.OrganisationId = UserOrganisationId;
                courseViewModel.Course = NidanBusinessService.UpdateCourse(UserOrganisationId, courseViewModel.Course);
            }
            var viewModel = new CourseViewModel
            {
                Course = courseViewModel.Course
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var centreCourseIds = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId).Items.Select(e=>e.CourseId);
            var data = NidanBusinessService.RetrieveCourses(organisationId, p => isSuperAdmin || centreCourseIds.Contains(p.CourseId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SubjectCourseList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var centreCourseIds = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId).Items.Select(e => e.CourseId);
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, p => isSuperAdmin || centreCourseIds.Contains(p.CourseId));
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetSector(int schemeId)
        {
            var data = NidanBusinessService.RetrieveSectors(UserOrganisationId, e => e.Scheme.SchemeId == schemeId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CourseInstallmentList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isSuperAdmin), orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCourseBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin, orderBy, paging));
        }
    }
}