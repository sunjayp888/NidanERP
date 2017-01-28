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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Nidan.Controllers
{
    [Authorize]
    public class MobilizationController : BaseController
    {

        public MobilizationController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }
        // GET: Mobilization

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var qualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var events = NidanBusinessService.RetrieveEvents(organisationId, e => true).Items.ToList();
            var viewModel = new MobilizationViewModel
            {
                Mobilization = new Mobilization
                {
                    OrganisationId = UserOrganisationId,
                    CentreId = 1,
                    Name = "Shraddha",
                    Mobile = 9870754355,
                    //Qualification = "BSCIT",
                    GeneratedDate = DateTime.Now,
                    FollowUpDate = DateTime.Now.AddDays(2),
                    Remark = "",
                },
                Courses = new SelectList(courses, "CourseId", "Name"),
                Events = new SelectList(events, "EventId", "Name"),
                Qualifications = new SelectList(qualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Mobilization/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MobilizationViewModel mobilizationViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.EventId = 3;
                mobilizationViewModel.Mobilization.CreatedDate = DateTime.Now;
                mobilizationViewModel.Mobilization.MobilizerStatus = "Open";
                mobilizationViewModel.Mobilization = NidanBusinessService.CreateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            mobilizationViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            //mobilizationViewModel.Events = new SelectList(NidanBusinessService.RetrieveEvents(organisationId, e => true).ToList());
            mobilizationViewModel.Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            return View(mobilizationViewModel);
        }

        // GET: Mobilization/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mobilization = NidanBusinessService.RetrieveMobilization(UserOrganisationId, id.Value);
            if (mobilization == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilization,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Events = new SelectList(NidanBusinessService.RetrieveEvents(UserOrganisationId, e => true).Items.ToList(), "EventId", "Name"),
                Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(UserOrganisationId, e => true).ToList(), "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Mobilizations/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MobilizationViewModel mobilizationViewModel)
        {
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.EventId = 3;
                mobilizationViewModel.Mobilization.CreatedDate = DateTime.Now;
                mobilizationViewModel.Mobilization.MobilizerStatus = "Open";
                mobilizationViewModel.Mobilization = NidanBusinessService.UpdateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilizationViewModel.Mobilization
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveMobilizations(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveMobilizationBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }

    }
}