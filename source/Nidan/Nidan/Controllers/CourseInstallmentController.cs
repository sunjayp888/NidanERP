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
    public class CourseInstallmentController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _todayUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public CourseInstallmentController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CourseInstallment
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CourseInstallment/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var viewModel = new CourseInstallmentViewModel()
            {
                CourseInstallment = new CourseInstallment(),
                Courses = new SelectList(courses, "CourseId", "Name")
            };
            return View(viewModel);
        }

        // POST: CourseInstallment/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseInstallmentViewModel courseInstallmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            courseInstallmentViewModel.CourseInstallment.OrganisationId = organisationId;
            if (ModelState.IsValid)
            {
                courseInstallmentViewModel.CourseInstallment = NidanBusinessService.CreateCourseInstallment(organisationId, courseInstallmentViewModel.CourseInstallment);
                return RedirectToAction("Index", "CourseInstallment");
            }
            courseInstallmentViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            return View(courseInstallmentViewModel);
        }

        // GET: CourseInstallment/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var courseInstallment = NidanBusinessService.RetrieveCourseInstallment(organisationId, id.Value);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            if (courseInstallment == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CourseInstallmentViewModel
            {
                CourseInstallment = courseInstallment,
                Courses = new SelectList(courses, "CourseId", "Name"),
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            return View(viewModel);
        }

        //// POST: CourseInstallment/Edit/{id}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(CourseInstallmentViewModel courseInstallmentViewModel)
        //{
        //    var organisationId = UserOrganisationId;
        //    var centreId = UserCentreId;
        //    if (ModelState.IsValid)
        //    {
        //        courseInstallmentViewModel.CourseInstallment.OrganisationId = organisationId;
        //        courseInstallmentViewModel.CourseInstallment.CentreId = centreId;
        //        courseInstallmentViewModel.CourseInstallment = NidanBusinessService.UpdateCourseInstallment(organisationId, courseInstallmentViewModel.CourseInstallment);
        //        return RedirectToAction("Index","CourseInstallment");
        //    }
        //    var viewModel = new CourseInstallmentViewModel
        //    {
        //        CourseInstallment = courseInstallmentViewModel.CourseInstallment
        //    };
        //    return View(viewModel);
        //}

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var courseId = Convert.ToInt32(TempData["CourseId"]);
            TempData["CourseId"] = courseId;
            if (courseId != 0)
            {
                var data = NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CourseId == courseId, orderBy, paging);
                return this.JsonNet(data);
            }
            else
            {
                return
                    this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId),
                        orderBy, paging));
            }
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCourseInstallmentBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }
    }
}