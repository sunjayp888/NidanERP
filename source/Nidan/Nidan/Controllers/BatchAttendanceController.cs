using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Business.Models;
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
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsSuperAdmin();
            var batches = NidanBusinessService.RetrieveBatches(organisationId, p => isSuperAdmin || p.CentreId == centreId).ToList();
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
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchAttendanceViewModel batchAttendanceViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                batchAttendanceViewModel.BatchAttendance.OrganisationId = organisationId;
                batchAttendanceViewModel.BatchAttendance.CentreId = centreId;
                return RedirectToAction("Index");
            }
            return View(batchAttendanceViewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var attendanceList = NidanBusinessService.RetrieveBatchAttendanceDataGrid(UserOrganisationId, p => isSuperAdmin || p.CentreId == centreId, orderBy, paging);
            return this.JsonNet(attendanceList);
        }

        [HttpPost]
        public ActionResult AttendanceList(int batchId, DateTime date, Paging paging, List<OrderBy> orderBy)
        {
            var attendanceList = NidanBusinessService.RetrieveStudentAttendanceByBatchId(UserOrganisationId, UserPersonnelId, batchId, date);
            return this.JsonNet(attendanceList);
        }

        [HttpPost]
        public ActionResult GetSubject(int batchId)
        {
            var organisationId = UserOrganisationId;
            var batchData = NidanBusinessService.RetrieveBatch(organisationId, batchId);
            var subjectIds = NidanBusinessService.RetrieveSubjectCourses(UserOrganisationId, e => e.CourseId == batchData.CourseId).Select(e => e.SubjectId).ToList();
            var subjectdata = NidanBusinessService.RetrieveSubjects(UserOrganisationId, e => subjectIds.Contains(e.SubjectId)).ToList();
            return this.JsonNet(subjectdata);
        }

        [HttpPost]
        public ActionResult GetSession(int subjectId)
        {
            var organisationId = UserOrganisationId;
            var subjectData = NidanBusinessService.RetrieveSubject(organisationId, subjectId);
            var sessiondata = NidanBusinessService.RetrieveSessions(UserOrganisationId, e => e.SubjectId == subjectData.SubjectId).Items.ToList();
            return this.JsonNet(sessiondata);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, int batchId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBatchAttendanceDataGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.AttendanceDate >= fromDate && e.AttendanceDate <= toDate && e.BatchId == batchId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBatches()
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBatches(organisationId, e => isSuperAdmin || e.CentreId == UserCentreId, null);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MarkAttendance(List<StudentAttendance> attendances, int batchId, int subjectId, int sessionId)
        {
            //Please make sure we are getting all data if not set in js file.
            var result = NidanBusinessService.MarkAttendance(UserOrganisationId, UserCentreId, UserPersonnelId, attendances, batchId, subjectId, sessionId);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult GetBiometricDataList(DateTime attendanceDate, int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var admissionData = NidanBusinessService.RetrieveAdmissions(organisationId, e => e.BatchId == batchId);
            var studentCodes = admissionData.Items.Select(e => e.Registration.StudentCode);
            var data = NidanBusinessService.RetrieveBiometricAttendanceGrid(organisationId, e => studentCodes.Contains(e.StudentCode) && e.LogDateTime == attendanceDate && e.Direction == "I");
            return this.JsonNet(data);
        }
    }

}
