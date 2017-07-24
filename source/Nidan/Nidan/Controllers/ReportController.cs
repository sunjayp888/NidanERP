using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nidan.Business;
using Nidan.Business.Dto;
using Nidan.Business.Helper;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System.IO;

namespace Nidan.Controllers
{
    public class ReportController : BaseController
    {

        public ReportController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: Report
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Report
        public ActionResult Enquiry()
        {
            return View(new BaseViewModel());
        }

        // GET: Report
        public ActionResult Mobilization()
        {
            return View(new BaseViewModel());
        }

        // GET: Report
        public ActionResult FollowUp()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Admission()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Registration()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Counselling()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Expense()
        {
            return View(new BaseViewModel());
        }

        public ActionResult MobilizationStatistics()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new ReportViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            viewModel.MonthList = new SelectList(viewModel.MonthType, "Id", "Name");
            viewModel.YearList = new SelectList(viewModel.YearType, "Id", "Name");
            return View(viewModel);
        }

        public ActionResult MobilizationProcessReportByMonth()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new ReportViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            viewModel.MonthList = new SelectList(viewModel.MonthType, "Id", "Name");
            viewModel.YearList = new SelectList(viewModel.YearType, "Id", "Name");
            return View(viewModel);
        }

        public ActionResult MobilizationProcessReportByDate()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new ReportViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            viewModel.MonthList = new SelectList(viewModel.MonthType, "Id", "Name");
            viewModel.YearList = new SelectList(viewModel.YearType, "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SearchEnquiryByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveEnquiryDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == centreId) && p.EnquiryDate >= fromDate && p.EnquiryDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchMobilizationByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveMobilizationDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate && p.CreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchFollowUpByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveFollowUpDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate && p.CreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchAdmissionByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveAdmissionGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.AdmissionDate >= fromDate && p.AdmissionDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchRegistrationByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveRegistrationGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.RegistrationDate >= fromDate && p.RegistrationDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchCounsellingByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveCounsellingGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate && p.CreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult ExpenseReportByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrievePettyCashExpenseReports(UserOrganisationId, p => (isSuperAdmin) && p.ExpenseCreatedDate >= fromDate && p.ExpenseCreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MobilizationCountReportByMonthAndYear(int centreId, int fromMonth, int toMonth, int fromYear,int toYear, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetriveMobilizationCountReportByMonthAndYear(UserOrganisationId, centreId, p => p.CentreId == centreId && p.Month >= fromMonth && p.Month <= toMonth && p.Year >= fromYear && p.Year <= toYear, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MobilizationCountReportBydate(int centreId, DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetriveMobilizationCountReportByDate(UserOrganisationId, centreId, p => p.CentreId == centreId && p.Date >= fromDate && p.Date <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult DownloadEnquiryCSVByDate(DateTime fromDate, DateTime toDate)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveEnquiryDataGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.EnquiryDate >= fromDate &&
                        p.EnquiryDate <= toDate).Items.ToList();
            string csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_EnquiryReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadMobilizationCSVByDate(DateTime fromDate, DateTime toDate)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveMobilizationDataGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate &&
                        p.CreatedDate <= toDate).Items.ToList();
            string csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_MobilizationReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadFollowUpCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveFollowUpDataGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.FollowUpDate >= fromDate &&
                        p.FollowUpDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_FollowUpReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadAdmissionCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveAdmissionGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.AdmissionDate >= fromDate &&
                        p.AdmissionDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_AdmissionReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadExpenseCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveAdmissionGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.AdmissionDate >= fromDate &&
                        p.AdmissionDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_ExpenseReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadCounsellingCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveCounsellingGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate &&
                        p.CreatedDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_CounsellingReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult DownloadRegistrationCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data =
                NidanBusinessService.RetrieveRegistrationGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.RegistrationDate >= fromDate &&
                        p.RegistrationDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_RegistrationReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }
    }
}