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
            var viewModel = new MobilizationViewModel
            {
                Mobilization = new Mobilization
                {
                    OrganisationId = UserOrganisationId,
                    EventId = 3,
                    CentreId = 1,
                    Name = "Shraddha",
                    Mobile = 9870754355,
                    InterestedCourse = ".net",
                    Qualification = "BSCIT",
                    CreatedDate=DateTime.Now,
                    FollowUpDate=DateTime.Now.AddDays(2),
                    Remark="",
                },
            };
            return View(viewModel);
        }

        // POST: Mobilization/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MobilizationViewModel mobilizationViewModel)
        {
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.EventId = 3;
                mobilizationViewModel.Mobilization = NidanBusinessService.CreateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
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
                Mobilization = mobilization
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
    
    }
}