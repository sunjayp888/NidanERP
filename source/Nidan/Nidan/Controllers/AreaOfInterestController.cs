using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Entity;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class AreaOfInterestController : BaseController
    {
        private static string AreaOfInterestName { get; set; }
        public AreaOfInterestController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: AreaOfInterest
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: AreaOfInterest/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var viewModel = new AreaOfInterestViewModel
            {
                AreaOfInterest = new AreaOfInterest()
                {
                    OrganisationId = UserOrganisationId,
                },
            };
            return View(viewModel);
        }

        // POST: AreaOfInterest/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaOfInterestViewModel areaOfInterestViewModel)
        {
            if (ModelState.IsValid)
            {
                areaOfInterestViewModel.AreaOfInterest.OrganisationId = UserOrganisationId;
                var result = NidanBusinessService.CreateAreaOfInterest(UserOrganisationId, areaOfInterestViewModel.AreaOfInterest);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return RedirectToAction("Index");
            }
            return View(areaOfInterestViewModel);
        }

        // GET: AreaOfInterest/Edit/5
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var areaOfInterest = NidanBusinessService.RetrieveAreaOfInterest(UserOrganisationId, id.Value);
            AreaOfInterestName = areaOfInterest.Name;
            var viewmodel = new AreaOfInterestViewModel()
            {
                AreaOfInterest = areaOfInterest,
            };

            return View(viewmodel);
        }

        // POST: AreaOfInterest/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaOfInterestViewModel areaOfInterestViewModel)
        {
            if (ModelState.IsValid)
            {
                areaOfInterestViewModel.AreaOfInterest.OrganisationId = UserOrganisationId;
                var result = NidanBusinessService.UpdateAreaOfInterest(UserOrganisationId, areaOfInterestViewModel.AreaOfInterest);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return RedirectToAction("Index");
            }
            return View(areaOfInterestViewModel);
        }

        // POST: AreaOfInterest/Delete/5
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //   NidanBusinessService.DeleteAbsenceType(UserOrganisationId, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveAreaOfInterests(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult CanDeleteAreaOfInterest(int id)
        {
            //  return this.JsonNet(NidanBusinessService.CanDeleteAbsenceType(UserOrganisationId, id));
            return null;
        }
    }
}