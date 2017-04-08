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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CourseInstallment/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var courseFeeBreakUp = NidanBusinessService.RetrieveCourseFeeBreakUp(organisationId, id.Value, e => true);
            var viewModel = new CourseInstallmentViewModel
            {
                CourseFeeBreakUpId = id.Value,
                Courses = new SelectList(courses, "CourseId", "Name"),
                CourseInstallment = new CourseInstallment
                {
                    CourseFeeBreakUp = courseFeeBreakUp
                }
            };
            viewModel.NoOfInstallmentList = new SelectList(viewModel.NoOfInstallmentType, "Id", "Name");
            return View(viewModel);
        }

        // POST: CourseInstallment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseInstallmentViewModel courseInstallmentViewModel)
        {
            courseInstallmentViewModel.CourseInstallment.OrganisationId = UserOrganisationId;
            //var coursefeebreakup=NidanBusinessService.RetrieveCourseFeeBreakUp(UserOrganisationId,)
            courseInstallmentViewModel.CourseInstallment.CourseFeeBreakUpId = courseInstallmentViewModel.CourseFeeBreakUpId;
            if (ModelState.IsValid)
            {
                courseInstallmentViewModel.CourseInstallment = NidanBusinessService.CreateCourseInstallment(UserOrganisationId, courseInstallmentViewModel.CourseInstallment);
                return RedirectToAction("Index", "CourseFeeBreakUp");
            }
            courseInstallmentViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList());
            courseInstallmentViewModel.NoOfInstallmentList = new SelectList(courseInstallmentViewModel.NoOfInstallmentType, "Id", "Name");
            return View(courseInstallmentViewModel);
        }

        // GET: CourseInstallment/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var courseInstallment = NidanBusinessService.RetrieveCourseInstallment(UserOrganisationId, id.Value);
            if (courseInstallment == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CourseInstallmentViewModel
            {
                CourseInstallment = courseInstallment,
                CourseFeeBreakUpId = courseInstallment.CourseFeeBreakUpId,
                Courses = new SelectList(courses, "CourseId", "Name")
            };
            viewModel.NoOfInstallmentList = new SelectList(viewModel.NoOfInstallmentType, "Id", "Name");
            return View(viewModel);
        }

        // POST: CourseInstallment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseInstallmentViewModel courseInstallmentViewModel)
        {
            if (ModelState.IsValid)
            {
                courseInstallmentViewModel.CourseInstallment.OrganisationId = UserOrganisationId;
                courseInstallmentViewModel.CourseInstallment = NidanBusinessService.UpdateCourseInstallment(UserOrganisationId, courseInstallmentViewModel.CourseInstallment);
                //return RedirectToAction("Edit", new { id = registrationPaymentReceiptViewModel.RegistrationPaymentReceipt.RegistrationPaymentReceiptId });
                return RedirectToAction("Index","CourseFeeBreakUp");
            }
            var viewModel = new CourseInstallmentViewModel
            {
                CourseInstallment = courseInstallmentViewModel.CourseInstallment
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("Admin");
            var courseId = Convert.ToInt32(TempData["CourseId"]);
            TempData["CourseId"] = courseId;
            if (courseId != 0)
            {
                return
                    this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isSuperAdmin ) && p.CourseId == courseId,
                        orderBy, paging));
            }
            else
            {
                return
                    this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isSuperAdmin),
                        orderBy, paging));
            }
        }
    }
}