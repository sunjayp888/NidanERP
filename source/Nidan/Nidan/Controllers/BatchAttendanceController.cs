using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
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
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var subjects = NidanBusinessService.RetrieveSubjects(organisationId, e => true).ToList();
            var sessions = NidanBusinessService.RetrieveSessions(organisationId, e => true).Items.ToList();
            var viewModel = new BatchAttendanceViewModel
            {
                BatchAttendance = new BatchAttendance(),
                Batches = new SelectList(batches, "BatchId", "Name"),
                Subjects = new SelectList(subjects, "SubjectId", "Name"),
                Sessions = new SelectList(sessions, "SessionId", "Name")
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
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
            batchAttendanceViewModel.Attendance.AttendanceDate = _todayUTC;
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

        [HttpPost]
        public ActionResult AttendanceList(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var admissiondata = NidanBusinessService.RetrieveAdmissions(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.BatchId == batchId, orderBy, paging);
            return this.JsonNet(admissiondata);
        }

        [HttpPost]
        public ActionResult GetSubject(int batchId)
        {
            var organisationId = UserOrganisationId;
            var batchData = NidanBusinessService.RetrieveBatch(organisationId,batchId);
            var subjectIds = NidanBusinessService.RetrieveSubjectCourses(UserOrganisationId, e => e.CourseId == batchData.CourseId).Select(e=>e.SubjectId).ToList();
            var subjectdata = NidanBusinessService.RetrieveSubjects(UserOrganisationId,e=>subjectIds.Contains(e.SubjectId)).ToList();
            return this.JsonNet(subjectdata);
        }

        [HttpPost]
        public ActionResult GetSession(int subjectId)
        {
            var organisationId = UserOrganisationId;
            var subjectData = NidanBusinessService.RetrieveSubject(organisationId, subjectId);
            var sessiondata = NidanBusinessService.RetrieveSessions(UserOrganisationId,e=>e.SubjectId==subjectData.SubjectId).Items.ToList();
            return this.JsonNet(sessiondata);
        }

        [HttpPost]
        public ActionResult SearchByDate(int batchId, DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAttendanceGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.AttendanceDate >= fromDate && e.AttendanceDate <= toDate && e.BatchId==batchId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}

