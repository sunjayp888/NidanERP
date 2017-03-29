using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public FollowUpController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        public ActionResult Index(string followUpType)
        {
            TempData["FollowUpType"] = followUpType;
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
                followUpViewModel.FollowUp.CentreId = UserCentreId;
                followUpViewModel.FollowUp = NidanBusinessService.UpdateFollowUp(UserOrganisationId, followUpViewModel.FollowUp);
                return RedirectToAction("Index");
            }
            var viewModel = new FollowUpViewModel
            {
                FollowUp = followUpViewModel.FollowUp,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var followUpType = TempData["FollowUpType"] as string;
            if (followUpType != null && followUpType == "Enquiry")
                return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
                   p => (isSuperAdmin || p.CentreId == UserCentreId)
                   && p.FollowUpDateTime == _today && p.FollowUpType == "Enquiry", orderBy, paging));

            if (followUpType != null && followUpType == "Counselling")
                return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
                   p => (isSuperAdmin || p.CentreId == UserCentreId)
                   && p.FollowUpDateTime == _today && p.FollowUpType == "Counselling", orderBy, paging));

            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
              p => (isSuperAdmin || p.CentreId == UserCentreId)
              && p.Close != "Yes", orderBy, paging));
        }

        [HttpPost]
        public ActionResult EnquiryList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
               p => (isSuperAdmin || p.CentreId == UserCentreId)
               && p.FollowUpType == "Enquiry"
               && p.FollowUpDateTime == _today, orderBy, paging));
        }

        [HttpPost]
        public ActionResult CounsellingList(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
               p => (isSuperAdmin || p.CentreId == UserCentreId)
               && p.FollowUpDateTime == _today
               && p.FollowUpType == "Counselling"
               , orderBy, paging));
        }


        public void Read(int id)
        {
            var data = NidanBusinessService.RetrieveFollowUp(UserOrganisationId, id);
            data.ReadDateTime = DateTime.UtcNow;
            NidanBusinessService.UpdateFollowUp(UserOrganisationId, data);
        }

        public ActionResult Count()
        {
            var count = NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
                e => e.FollowUpDateTime.Date == DateTime.UtcNow.Date && e.ReadDateTime.Date != DateTime.UtcNow.Date);
            return this.JsonNet(count);
        }

        [HttpPost]
        public void MarkAsRead(int id)
        {
            NidanBusinessService.MarkAsReadFollowUp(UserOrganisationId, id);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveFollowUps(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.FollowUpDateTime >= fromDate && e.FollowUpDateTime <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}