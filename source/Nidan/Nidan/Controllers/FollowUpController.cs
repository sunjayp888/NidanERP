using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Extensions;
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
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        private readonly DateTime _tomorrow = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(1);
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
            TempData["FollowUpId"] = id;
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var followUp = NidanBusinessService.RetrieveFollowUp(organisationId, id.Value);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            var interestedCourses = followUp.Enquiry?.EnquiryCourses.Select(e => e.CourseId).ToList();
            var interestedCourseIds = followUp.FollowUpType == "Enquiry" ? interestedCourses : followUp.IntrestedCourseId.ToIEnumarable();

            var courses = NidanBusinessService.RetrieveCourses(organisationId, p => true)
                          .Where(e => interestedCourseIds?.Contains(e.CourseId) ?? true);

            var viewModel = new FollowUpViewModel
            {
                FollowUp = followUp,
                Courses = new SelectList(courses, "CourseId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            return View(viewModel);
        }

        // POST: FollowUp/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FollowUpViewModel followUpViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                followUpViewModel.FollowUp.OrganisationId = organisationId;
                followUpViewModel.FollowUp.CentreId = centreId;
                followUpViewModel.FollowUp = NidanBusinessService.UpdateFollowUp(organisationId, followUpViewModel.FollowUp);
                return RedirectToAction("Index");
            }
            var viewModel = new FollowUpViewModel
            {
                FollowUp = followUpViewModel.FollowUp,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name")
            };
            return View(viewModel);
        }

        // GET: Report/Pending
        public ActionResult Pending()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Todays
        public ActionResult Todays()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Tomorrows
        public ActionResult Tomorrows()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Upcoming
        public ActionResult Upcoming()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var followUpType = TempData["FollowUpType"] as string;
            if (followUpType != null && followUpType == "Enquiry")
                return this.JsonNet(NidanBusinessService.RetrieveFollowUps(organisationId,
                   p => (isSuperAdmin || p.CentreId == UserCentreId)
                   && p.FollowUpDateTime == _today && p.FollowUpType == "Enquiry", orderBy, paging));

            if (followUpType != null && followUpType == "Counselling")
                return this.JsonNet(NidanBusinessService.RetrieveFollowUps(organisationId,
                   p => (isSuperAdmin || p.CentreId == UserCentreId)
                   && p.FollowUpDateTime == _today && p.FollowUpType == "Counselling", orderBy, paging));

            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(organisationId,
              p => (isSuperAdmin || p.CentreId == UserCentreId)
              && p.Close != "Yes", orderBy, paging));
        }

        [HttpPost]
        public ActionResult PendingList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var pendingFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime < _today, orderBy, paging);
            return this.JsonNet(pendingFollowUpCount);
        }

        [HttpPost]
        public ActionResult TodaysList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var todaysFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime == _today, orderBy, paging);
            return this.JsonNet(todaysFollowUpCount);
        }

        [HttpPost]
        public ActionResult TomorrowsList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var tomorrowFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime == _tomorrow, orderBy, paging);
            return this.JsonNet(tomorrowFollowUpCount);
        }

        [HttpPost]
        public ActionResult UpcomingList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var upcomingFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime > _tomorrow, orderBy, paging);
            return this.JsonNet(upcomingFollowUpCount);
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
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFollowUps(organisationId,
               p => (isSuperAdmin || p.CentreId == UserCentreId)
               && p.FollowUpDateTime == _today
               && p.FollowUpType == "Counselling"
               , orderBy, paging));
        }

        [HttpPost]
        public ActionResult FollowUpHistoryList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var followUpId = Convert.ToInt32(TempData["FollowUpId"]);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFollowUpHistories(organisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.FollowUpId == followUpId, orderBy, paging));
        }

        public void Read(int id)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveFollowUp(organisationId, id);
            data.ReadDateTime = DateTime.UtcNow;
            NidanBusinessService.UpdateFollowUp(organisationId, data);
        }

        public ActionResult Count()
        {
            var organisationId = UserOrganisationId;
            var count = NidanBusinessService.RetrieveFollowUps(organisationId,
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
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.FollowUpDateTime >= fromDate && e.FollowUpDateTime <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveFollowUpBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Close == "No", orderBy, paging));
        }
    }
}