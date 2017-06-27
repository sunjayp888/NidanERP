using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class BatchAttendanceController : BaseController
    {
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public BatchAttendanceController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: BatchAttendance
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: BatchAttendance/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var batch = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var subject = NidanBusinessService.RetrieveSubjects(organisationId, e => true).ToList();
            var session = NidanBusinessService.RetrieveSessions(organisationId, e => true).Items.ToList();
            var viewModel = new BatchAttendanceViewModel
            {
                BatchAttendance = new BatchAttendance(),
            };
            return View(viewModel);
        }

        // POST: BatchAttendance/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchAttendanceViewModel batchAttendanceViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var batch = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var subject = NidanBusinessService.RetrieveSubjects(organisationId, e => true).ToList();
            var session = NidanBusinessService.RetrieveSessions(organisationId, e => true).Items.ToList();
            //attendanceViewModel.Attendance.AttendanceDate = _todayUTC;
            if (ModelState.IsValid)
            {
                batchAttendanceViewModel.BatchAttendance.OrganisationId = UserOrganisationId;
                batchAttendanceViewModel.BatchAttendance.CentreId = UserCentreId;
                batchAttendanceViewModel.BatchAttendance.PersonnelId = UserPersonnelId;
                //attendanceViewModel.Attendance = NidanBusinessService.CreateAttendance(organisationId, centreId, attendanceViewModel.Attendance.PersonnelId, attendanceViewModel.Attendance);
                return RedirectToAction("Index");
            }
            return View(batchAttendanceViewModel);
        }
    }
}