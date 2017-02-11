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
    public class FollowUpController : BaseController
    {
        // GET: FollowUp
        public FollowUpController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: FollowUp/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var followUp = NidanBusinessService.RetrieveFollowUp(UserOrganisationId, id.Value);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            var viewModel = new FollowUpViewModel
            {
                FollowUp = followUp,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name")
            };
            return View(viewModel);
        }

        // POST: FollowUp/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FollowUpViewModel followUpViewModel)
        {
            //var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                followUpViewModel.FollowUp.OrganisationId = UserOrganisationId;
                followUpViewModel.FollowUp.CentreId = 1;
                followUpViewModel.FollowUp = NidanBusinessService.UpdateFollowUp(UserOrganisationId, followUpViewModel.FollowUp);
                return RedirectToAction("Index");
            }
            var viewModel = new FollowUpViewModel
            {
                FollowUp = followUpViewModel.FollowUp
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveFollowUps(UserOrganisationId, f => true, orderBy, paging);
            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId, f => true, orderBy, paging));
        }

        public void Read(int id)
        {
            var data = NidanBusinessService.RetrieveFollowUp(UserOrganisationId, id);
            data.ReadDateTime = DateTime.Now;
            NidanBusinessService.UpdateFollowUp(UserOrganisationId, data);
        }

        public ActionResult Count()
        {
            var count = NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
                e => e.CreatedDateTime == DateTime.Now && e.ReadDateTime.Value.Date != DateTime.Now.Date);
            return this.JsonNet(count);
        }

    [HttpPost]
    public void MarkAsRead(int id)
        {
            var data = NidanBusinessService.RetrieveFollowUp(UserOrganisationId, id);
            data.ReadDateTime = DateTime.Now;
            NidanBusinessService.UpdateFollowUp(UserOrganisationId, data);
            //return RedirectToAction("Index");
        }

    }
}