using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Business.Models;
using Nidan.Extensions;
using Nidan.Models;


namespace Nidan.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        private readonly DateTime _tomorrow = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(1);
        public ActionResult Index()

        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsSuperAdmin();
            // var data = NidanBusinessService.RetrievePieGraphStatistics(organisationId);
            var permissions = NidanBusinessService.RetrievePersonnelPermissions(User.IsInRole("Admin"), organisationId, personnelId);

            var enquiryCount = NidanBusinessService.RetrieveEnquiries(organisationId,
                e => (isSuperAdmin || e.CentreId == centreId) && e.EnquiryDate == _today && (e.EnquiryStatus == "Enquiry" || e.EnquiryStatus == "Counselling")).Count();

            var mobilizationCount =
                NidanBusinessService.RetrieveMobilizations(organisationId,
               e => (isSuperAdmin || e.CentreId == centreId) && e.CreatedDate == _today && e.Close == "No").Items.Count();

            var registraionCount = NidanBusinessService.RetrieveRegistrations(organisationId,
               e => (isSuperAdmin || e.CentreId == centreId) && e.RegistrationDate == _today && e.IsAdmissionDone == false).Items.Count();

            var admissionCount =
                NidanBusinessService.RetrieveAdmissions(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.AdmissionDate == _today)
                    .Items.Count();

            var pendingFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime < _today).Items.Count();
            var todaysFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime == _today).Items.Count();
            var tomorrowFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime == _tomorrow).Items.Count();
            var upcomingFollowUpCount = NidanBusinessService.RetrieveFollowUps(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.Close == "No" && e.FollowUpDateTime > _tomorrow).Items.Count();

            if (User.IsInRole("User") && !permissions.IsManager)
                return RedirectToAction("Profile", "Personnel", new { id = personnelId });

            var viewModel = new HomeViewModel
            {
                Permissions = permissions,
                EnquiryCount = enquiryCount,
                RegistraionCount = registraionCount,
                MobilizationCount = mobilizationCount,
                AdmissionCount = admissionCount,
                PendingFollowUpCount = pendingFollowUpCount,
                TodaysFollowUpCount = todaysFollowUpCount,
                TomorrowsFollowUpCount = tomorrowFollowUpCount,
                UpcomingFollowUpCount = upcomingFollowUpCount
                //Divisions = initialDivisions,
                //SelectedDivisionIds = permissions.IsAdmin ? null : initialDivisions.Select(c => c.DivisionId).ToList()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(new BaseViewModel());
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult Statistics()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId));
            var graphData = new List<PieGraph>()
            {
                new PieGraph() {Label = "Mobilization",Value = data.Sum(e => e.MobilizationCount).ToString()},
                new PieGraph() {Label = "Enquiry",Value = data.Sum(e => e.EnquiryCount).ToString()},
                new PieGraph() {Label = "Admission",Value = data.Sum(e => e.AdmissionCount).ToString()},
                new PieGraph() {Label = "Registration",Value = data.Sum(e => e.RegistrationCount).ToString()},
                new PieGraph() {Label = "Counselling",Value = data.Sum(e => e.CounsellingCount).ToString()}
            };
            return this.JsonNet(graphData);
        }

        [HttpPost]
        public ActionResult StatisticsByCentre(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId)).Where(e => e.CentreId == id).ToList();
            var graphData = new List<PieGraph>()
            {
                new PieGraph() {Label = "Mobilization",Value = data.Sum(e => e.MobilizationCount).ToString()},
                new PieGraph() {Label = "Enquiry",Value = data.Sum(e => e.EnquiryCount).ToString()},
                new PieGraph() {Label = "Admission",Value = data.Sum(e => e.AdmissionCount).ToString()},
                new PieGraph() {Label = "Registration",Value = data.Sum(e => e.RegistrationCount).ToString()},
                new PieGraph() {Label = "Counselling",Value = data.Sum(e => e.CounsellingCount).ToString()}
            };
            return this.JsonNet(graphData);
        }

        [HttpPost]
        public ActionResult StatisticsBarGraph()
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            // var centre = NidanBusinessService.RetrieveCentres(organisationId, e=>isSuperAdmin||e.CentreId==UserCentreId).ToList();
            var startOfWeekDate = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
            var endOfWeekDate = startOfWeekDate.AddDays(6);
            var graphData = new List<Graph>();
            var enquiries =
                NidanBusinessService.RetrieveEnquiries(organisationId,
                        e => (isSuperAdmin || e.CentreId == centreId) && e.EnquiryDate >= startOfWeekDate && e.EnquiryDate <= endOfWeekDate)
                    .ToList();
            var mobilizations =
                NidanBusinessService.RetrieveMobilizations(organisationId,
                        e => (isSuperAdmin || e.CentreId == centreId) && e.CreatedDate >= startOfWeekDate && e.CreatedDate <= endOfWeekDate)
                    .Items.ToList();
            var registrations =
                NidanBusinessService.RetrieveRegistrations(organisationId,
                        e => (isSuperAdmin || e.CentreId == centreId) && e.RegistrationDate >= startOfWeekDate && e.RegistrationDate <= endOfWeekDate)
                    .Items.ToList();
            var admissions =
                NidanBusinessService.RetrieveAdmissions(organisationId,
                        e => (isSuperAdmin || e.CentreId == centreId) && e.AdmissionDate >= startOfWeekDate && e.AdmissionDate <= endOfWeekDate)
                    .Items.ToList();

            foreach (var date in endOfWeekDate.RangeFrom(startOfWeekDate))
            {
                graphData.Add(new Graph
                {
                    MobilizationCount = mobilizations.Count(e => e.CreatedDate.Date == date.Date && e.Close == "No"),
                    AdmissionCount = admissions.Count(e => e.AdmissionDate.Date == date.Date),
                    EnquiryCount = enquiries.Count(e => e.EnquiryDate.Date == date.Date && e.IsRegistrationDone == false),
                    RegistrationCount = registrations.Count(e => e.RegistrationDate.Date == date && e.IsAdmissionDone == false),
                    Date = date
                });
            }

            //var data = NidanBusinessService.RetrieveBarGraphStatistics(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId));
            return this.JsonNet(graphData);
        }

        [HttpPost]
        public ActionResult StatisticsBarGraphByCentre(int? id)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBarGraphStatistics(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId)).Where(e => e.CentreId == id);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCentres()
        {
            var data = NidanBusinessService.RetrieveCentres(UserOrganisationId);
            return this.JsonNet(data);
        }
    }
}