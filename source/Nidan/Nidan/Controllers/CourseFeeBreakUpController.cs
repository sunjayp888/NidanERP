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
    [Authorize]
    public class CourseFeeBreakUpController : BaseController
    {
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public CourseFeeBreakUpController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }

        // GET: CourseFeeBreakUp
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:CourseFeeBreakUp/create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
             var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new CourseFeeBreakUpViewModel
            {
                CourseFeeBreakUp = new CourseFeeBreakUp(),
                Centres = new SelectList(centres, "CentreId", "Name"),
            };
            return View(viewModel);
        }

        // POST: CourseFeeBreakUp/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseFeeBreakUpViewModel courseFeeBreakUpViewModel)
        {
            courseFeeBreakUpViewModel.CourseFeeBreakUp.OrganisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                courseFeeBreakUpViewModel.CourseFeeBreakUp.OrganisationId = UserOrganisationId;
                courseFeeBreakUpViewModel.CourseFeeBreakUp = NidanBusinessService.CreateCourseFeeBreakUp(UserOrganisationId, courseFeeBreakUpViewModel.CourseFeeBreakUp);
                var courseFeeBreakUp = NidanBusinessService.RetrieveCourseFeeBreakUp(UserOrganisationId, courseFeeBreakUpViewModel.CourseFeeBreakUp.CourseFeeBreakUpId);
                var courseInstallment = new CourseInstallment
                {
                    CourseFeeBreakUpId = courseFeeBreakUp.CourseFeeBreakUpId
                };
                return RedirectToAction("Create","CourseInstallment", new { id = courseInstallment.CourseFeeBreakUpId });
            }
            courseFeeBreakUpViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(UserOrganisationId, e => true).ToList());
            return View(courseFeeBreakUpViewModel);
        }

        // GET: CourseFeeBreakUp/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseFeeBreakUp = NidanBusinessService.RetrieveCourseFeeBreakUp(UserOrganisationId, id.Value);
            if (courseFeeBreakUp == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CourseFeeBreakUpViewModel
            {
                CourseFeeBreakUp = courseFeeBreakUp,
            };
            return View(viewModel);
        }

        // POST: trainer/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseFeeBreakUpViewModel courseFeeBreakUpViewModel)
        {
            if (ModelState.IsValid)
            {
                courseFeeBreakUpViewModel.CourseFeeBreakUp.OrganisationId = UserOrganisationId;
                //courseFeeBreakUpViewModel.CourseFeeBreakUp = NidanBusinessService.UpdateCourseInstallment(UserOrganisationId, CourseFeeBreakUpViewModel.);
                return RedirectToAction("Index");
            }
            var viewModel = new CourseFeeBreakUpViewModel
            {
                CourseFeeBreakUp = courseFeeBreakUpViewModel.CourseFeeBreakUp
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isAdmin = User.IsInAnyRoles("Admin");
            return this.JsonNet(NidanBusinessService.RetrieveCourseInstallments(UserOrganisationId, p => (isAdmin), orderBy, paging));
        }
    }
}