using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
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

        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public ActionResult Index()

        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrievePieGraphStatistics(organisationId);
            var permissions = NidanBusinessService.RetrievePersonnelPermissions(User.IsInRole("Admin"), organisationId, personnelId);

            var enquiryCount = NidanBusinessService.RetrieveEnquiries(UserOrganisationId,
               e => (isSuperAdmin || e.CentreId == centreId) && e.EnquiryDate == _today && e.EnquiryStatus=="Enquiry").Count();

            var mobilizationCount =
                NidanBusinessService.RetrieveMobilizations(UserOrganisationId,
               e => (isSuperAdmin || e.CentreId == centreId) && e.CreatedDate == _today && e.Close == "No").Items.Count();

            var registraionCount = NidanBusinessService.RetrieveRegistrations(UserOrganisationId,
               e => (isSuperAdmin || e.CentreId == centreId) && e.RegistrationDate == _today && e.IsAdmissionDone == false).Items.Count();

            var admissionCount =
                NidanBusinessService.RetrieveAdmissions(UserOrganisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.AdmissionDate== _today)
                    .Items.Count();


            if (User.IsInRole("User") && !permissions.IsManager)
                return RedirectToAction("Profile", "Personnel", new { id = personnelId });

            var viewModel = new HomeViewModel
            {
                Permissions = permissions,
                EnquiryCount = enquiryCount,
                RegistraionCount = registraionCount,
                MobilizationCount = mobilizationCount,
                AdmissionCount = admissionCount
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
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId);
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
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId).Where(e => e.CentreId == id).ToList();
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
            var data = NidanBusinessService.RetrieveBarGraphStatistics(UserOrganisationId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult StatisticsBarGraphByCentre(int? id)
        {
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId).Where(e => e.CentreId == id);
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