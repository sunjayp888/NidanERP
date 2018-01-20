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

        // GET: Report/Mobilization
        public ActionResult Mobilization()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/FollowUp
        public ActionResult FollowUp()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Admission
        public ActionResult Admission()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Registration
        public ActionResult Registration()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Counselling
        public ActionResult Counselling()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Expense
        public ActionResult Expense()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/BankDeposite
        public ActionResult BankDepositeDetailByDate()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Stock
        public ActionResult CentrePettyCashByCentre()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/BankDepositeReports
        public ActionResult BankDepositeReport()
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

        // GET: Report/FixAsset
        public ActionResult FixAsset()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var assetClasses = NidanBusinessService.RetrieveAssetClasses(organisationId, e => true);
            var viewModel = new ReportViewModel()
            {
                Centres = new SelectList(centres, "CentreId", "Name"),
                AssetClasses = new SelectList(assetClasses, "AssetClassId", "Name")
            };
            return View(viewModel);
        }

        // GET: Report/Stock
        public ActionResult Stock()
        {
            return View(new BaseViewModel());
        }

        // GET: Report/Stock
        public ActionResult FeeSummaryByDate()
        {
            return View(new BaseViewModel());
        }


        // GET: Report/MobilizationStatistics
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

        // GET: Report/MobilizationProcessReportByMonth
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

        // GET: Report/BankDepositeProcessReportByMonth
        public ActionResult BankDepositeProcessReportByMonth()
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

        // GET: Report/MobilizationProcessReportByDate
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

        // GET: Report/BankDepositeReportByDate
        public ActionResult BankDepositeReportByDate()
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
        public ActionResult AvailablePettyCashReport(Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetriveAvailablePettyCashReport(UserOrganisationId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MobilizationCountReportByMonthAndYear(int centreId, int year, Paging paging, List<OrderBy> orderBy)
        {
            var month = DateTime.Now.Month;
            var fiscalYear = month == 1 || month == 2 || month == 3 ? year - 1 : year;
            var data = NidanBusinessService.RetriveMobilizationCountReportByMonthAndYear(UserOrganisationId, centreId, fiscalYear, orderBy);
            return this.JsonNet(data);
        }


        [HttpPost]
        public ActionResult BankDepositeReportByMonthAndYear(int centreId, int year, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetriveBankDepositeReportByMonthAndYear(UserOrganisationId, centreId, year, orderBy);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MobilizationCountReportBydate(int centreId, int month, int year)
        {
            var data = NidanBusinessService.RetriveMobilizationCountReportByDate(UserOrganisationId, centreId, year, month);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult BankDepositeReportBydate(int centreId, int month, int year)
        {
            var data = NidanBusinessService.RetriveBankDepositeReportByDate(UserOrganisationId, centreId, year, month);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult TotalBankDepositeReportByCurrentMonth()
        {
            var data = NidanBusinessService.RetriveBankDepositeCountReportByMonthWise(UserOrganisationId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CentreBankDepositeReportByDate(int centreId, DateTime date)
        {
            var data = NidanBusinessService.RetriveCentreBankDepositeByDate(UserOrganisationId, centreId, date);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CentreCandidateFeeSummaryReportByDate(int centreId, DateTime date)
        {
            var data = NidanBusinessService.RetriveCentreCandidateFeeByDate(UserOrganisationId, centreId, date);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchStockByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveStockReportDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.StockPurchaseDate >= fromDate && p.StockPurchaseDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult TotalSumOfCountReportByMonthAndYear(int centreId, int fromMonth, int toMonth, int fromYear, int toYear)
        {
            var data = NidanBusinessService.RetriveMobilizationCountReportByMonthAndYear(UserOrganisationId, centreId, fromYear).ToList();
            var totalSumOfCount = new MobilizationCentreReportMonthWise()
            {
                MobilizationCount = data.Sum(e => e.MobilizationCount),
                EnquiryCount = data.Sum(e => e.EnquiryCount),
                CounsellingCount = data.Sum(e => e.CounsellingCount),
                RegistrationCount = data.Sum(e => e.RegistrationCount),
                AdmissionCount = data.Sum(e => e.AdmissionCount),
                CourseBooking = data.Sum(e => e.CourseBooking),
                FeeCollected = data.Sum(e => e.FeeCollected)
            };
            return this.JsonNet(totalSumOfCount);
        }

        [HttpPost]
        public ActionResult TotalMobilizationCountReportBydate(int centreId, int month, int year, Paging paging, List<OrderBy> orderBy)
        {
            //var data = NidanBusinessService.RetriveMobilizationCountReportByDate(UserOrganisationId, centreId, TODO, TODO, orderBy);
            //var totalSumOfCount = new MobilizationCentreReport()
            //{
            //    MobilizationCount = data.Items.Sum(e => e.MobilizationCount),
            //    EnquiryCount = data.Items.Sum(e => e.EnquiryCount),
            //    CounsellingCount = data.Items.Sum(e => e.CounsellingCount),
            //    RegistrationCount = data.Items.Sum(e => e.RegistrationCount),
            //    AdmissionCount = data.Items.Sum(e => e.AdmissionCount),
            //    CourseBooking = data.Items.Sum(e => e.CourseBooking),
            //    FeeCollected = data.Items.Sum(e => e.FeeCollected)
            //};
            return this.JsonNet(null);
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
            var data = NidanBusinessService.RetrieveFollowUpDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.FollowUpDate >= fromDate && p.FollowUpDate <= toDate).Items.ToList();
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

        [HttpPost]
        public ActionResult DownloadMobilizationCountReportCSVByMonthAndYear(int centreId, int fromYear)
        {
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, centreId);
            var data = NidanBusinessService.RetriveMobilizationCountReportByMonthAndYear(UserOrganisationId, centreId, fromYear).ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_StatisticsReportByMonthWise-({1}).csv", centre.Name, fromYear));
        }

        [HttpPost]
        public ActionResult DownloadBankDepositeReportCSVByMonthAndYear(int centreId, int fromYear)
        {
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, centreId);
            var data = NidanBusinessService.RetriveBankDepositeReportByMonthAndYear(UserOrganisationId, centreId, fromYear).ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_BankDepositeReportByMonthWise-({1}).csv", centre.Name, fromYear));
        }

        [HttpPost]
        public ActionResult DownloadBankDepositeReportCSVByDate(int centreId, int fromMonth, int fromYear)
        {
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, centreId);
            var data = NidanBusinessService.RetriveBankDepositeReportByDate(UserOrganisationId, centreId, fromYear, fromMonth).ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_BankDepositeByDate-({1}-{2}).csv", centre.Name, fromMonth, fromYear));
        }

        [HttpPost]
        public ActionResult DownloadMobilizationCountReportCSVByDate(int centreId, int fromMonth, int fromYear)
        {
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, centreId);
            var data = NidanBusinessService.RetriveMobilizationCountReportByDate(UserOrganisationId, centreId, fromYear, fromMonth).ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_StatisticsReportByDate-({1}-{2}).csv", centre.Name, fromMonth, fromYear));
        }

        [HttpPost]
        public ActionResult FixAssetByCentreIdAssetClassId(int assetClassId, int centreId)
        {
            if (centreId == 6 && assetClassId != 5)
            {
                var allCentredata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, e => e.AssetClassId == assetClassId);
                return this.JsonNet(allCentredata);
            }
            if (assetClassId == 5 && centreId != 6)
            {
                var allAssetdata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, e => e.CentreId == centreId);
                return this.JsonNet(allAssetdata);
            }
            if (centreId == 6 && assetClassId == 5)
            {
                var alldata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, e => true);
                return this.JsonNet(alldata);
            }
            var data = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, e => e.AssetClassId == assetClassId && e.CentreId == centreId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchFixAssetByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, p => (isSuperAdmin || p.CentreId == centreId) && p.DateofPurchase >= fromDate && p.DateofPurchase <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }
        [HttpPost]
        public ActionResult DownloadFixAssetByCentreIdAssetClassId(int assetClassId, int centreId)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, centreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            if (centreId == 6 && assetClassId != 5)
            {
                var alldata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, p => (isSuperAdmin) && p.AssetClassId == assetClassId).Items.ToList();
                var allcsv = alldata.GetCSV();
                return File(new System.Text.UTF8Encoding().GetBytes(allcsv), "text/csv", string.Format("{0}_FixAssetReport.csv", centre.Name));
            }
            if (assetClassId == 5 && centreId != 6)
            {
                var alldata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, p => (isSuperAdmin) && p.CentreId == centreId).Items.ToList();
                var allcsv = alldata.GetCSV();
                return File(new System.Text.UTF8Encoding().GetBytes(allcsv), "text/csv", string.Format("{0}_FixAssetReport.csv", centre.Name));
            }
            if (centreId == 6 && assetClassId == 5)
            {
                var alldata = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, e => true)
                    .Items.ToList();
                var allcsv = alldata.GetCSV();
                return File(new System.Text.UTF8Encoding().GetBytes(allcsv), "text/csv",
                    string.Format("{0}_FixAssetReport.csv", centre.Name));
            }
            var data = NidanBusinessService.RetrieveFixAssetDetailGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == centreId) && p.AssetClassId == assetClassId).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_FixAssetReport.csv", centre.Name));
        }

        [HttpPost]
        public ActionResult DownloadStockCSVByDate(DateTime fromDate, DateTime toDate)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var centreName = isSuperAdmin ? string.Empty : centre.Name;
            var data = NidanBusinessService.RetrieveStockReportDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.StockPurchaseDate >= fromDate && p.StockPurchaseDate <= toDate).Items.ToList();
            var csv = data.GetCSV();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", string.Format("{0}_StockReport-({1} To {2}).csv", centreName, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public ActionResult CentrePettyCashList(int centreId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCentrePettyCashs(UserOrganisationId, UserCentreId, p => (isSuperAdmin && p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult DownPaymentSummaryByDate(int centreId, DateTime? date)
        {
            var data = NidanBusinessService.RetrieveDownpaymentSummaryByDate(UserOrganisationId, centreId, date.Value, e => true);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult InstallmentSummaryByDate(int centreId, DateTime? date)
        {
            var data = NidanBusinessService.RetrieveInstallmentSummaryByDate(UserOrganisationId, centreId, date.Value, e => true);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult MobilizationCountReportByCurrentMonth()
        {
            var data = NidanBusinessService.RetriveMobilizationCountReportByMonthWise(UserOrganisationId);
            return this.JsonNet(data);
        }
    }
}