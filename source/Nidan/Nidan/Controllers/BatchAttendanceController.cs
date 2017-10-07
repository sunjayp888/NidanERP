using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
            DateTime.UtcNow.Day, 0, 0, 0);

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
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var viewModel = new BatchAttendanceViewModel
            {
                BatchAttendance = new BatchAttendance(),
                Batches = new SelectList(batches, "BatchId", "Name"),
            };
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
        public ActionResult AttendanceList(int batchId, DateTime date, Paging paging, List<OrderBy> orderBy)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var admissiondata = NidanBusinessService.RetrieveAttendanceGrid(UserOrganisationId,
                p => (isSuperAdmin || p.CentreId == UserCentreId) && p.BatchId == batchId && (DbFunctions.TruncateTime(p.AttendanceDate) == date || !p.AttendanceDate.HasValue), orderBy, paging);
            return this.JsonNet(admissiondata);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, int batchId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveAttendanceGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.AttendanceDate >= fromDate && e.AttendanceDate <= toDate && e.BatchId == batchId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBatches()
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId == UserCentreId, null);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MarkAttendance(List<AttendanceGrid> attendances, int subjectId, int sessionId)
        {
            //Please make sure we are getting all data if not set in js file.
            var result = NidanBusinessService.MarkAttendance(UserOrganisationId, UserCentreId, UserPersonnelId, attendances, subjectId, sessionId);
            return null;
        }

        [HttpPost]
        public ActionResult GetBiometricDataList(DateTime attendanceDate, int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var admissionData = NidanBusinessService.RetrieveAdmissions(organisationId, e => e.BatchId == batchId);
            var studentCodes = admissionData.Items.Select(e => e.Registration.StudentCode);
            var data = NidanBusinessService.RetrieveBiometricAttendanceGrid(organisationId, e => studentCodes.Contains(e.StudentCode) && e.LogDateTime == attendanceDate && e.Direction=="I");
            return this.JsonNet(data);
        }
    }

}
