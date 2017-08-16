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
    public class AttendanceController : BaseController
    {
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public AttendanceController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: Attendance
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Attendance/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
           var viewModel = new AttendanceViewModel
            {
                Attendance = new Attendance(),
            };
            return View(viewModel);
        }

        // POST: Attendance/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceViewModel attendanceViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                attendanceViewModel.Attendance.OrganisationId = UserOrganisationId;
                attendanceViewModel.Attendance.CentreId = UserCentreId;
                attendanceViewModel.Attendance.PersonnelId = UserPersonnelId;
                attendanceViewModel.Attendance = NidanBusinessService.CreateAttendance(organisationId,centreId, attendanceViewModel.Attendance.PersonnelId.Value, attendanceViewModel.Attendance);
                return RedirectToAction("Index");
            }
            return View(attendanceViewModel);
        }

        // GET: Attendance/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var attendance = NidanBusinessService.RetrieveAttendance(organisationId, id.Value);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AttendanceViewModel
            {
                Attendance = attendance
            };
            return View(viewModel);
        }

        // POST: Attendance/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttendanceViewModel attendanceViewModel)
        {
            if (ModelState.IsValid)
            {
                attendanceViewModel.Attendance.OrganisationId = UserOrganisationId;
                attendanceViewModel.Attendance.CentreId = UserCentreId;
                attendanceViewModel.Attendance.PersonnelId = UserPersonnelId;
                attendanceViewModel.Attendance = NidanBusinessService.UpdateAttendance(UserOrganisationId, attendanceViewModel.Attendance);
                return RedirectToAction("Index");
            }
            var viewModel = new AttendanceViewModel
            {
                Attendance = attendanceViewModel.Attendance
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveAttendances(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging));
        }
    }
}