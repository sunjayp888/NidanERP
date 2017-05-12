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

            var enquiryCount = NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
               e => (isSuperAdmin || e.CentreId == UserCentreId) && e.FollowUpDateTime == _today && e.FollowUpType.ToLower() == "enquiry").Items.Count();

            var totalEnquiryCount =
                NidanBusinessService.RetrieveEnquiries(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.Close != "Yes" && e.IsRegistrationDone == false).Count();

            var counsellingCount = NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
               e => (isSuperAdmin || e.CentreId == UserCentreId) && e.FollowUpDateTime == _today && e.FollowUpType.ToLower() == "counselling").Items.Count();

            var totalMobilizationCount =
                NidanBusinessService.RetrieveMobilizations(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.Close != "Yes")
                    .Items.Count();


            if (User.IsInRole("User") && !permissions.IsManager)
                return RedirectToAction("Profile", "Personnel", new { id = personnelId });

            var viewModel = new HomeViewModel
            {
                Permissions = permissions,
                EnquiryCount = enquiryCount,
                CounsellingCount = counsellingCount,
                TotalEnquiryCount = totalEnquiryCount,
                TotalMobilizationCount = totalMobilizationCount
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
        public ActionResult StatisticsByCentre(int id)
        {
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId).Where(e => e.CentreId == id);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult StatisticsBarGraph()
        {
            var data = NidanBusinessService.RetrieveBarGraphStatistics(UserOrganisationId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult StatisticsBarGraphByCentre(int id)
        {
            var data = NidanBusinessService.RetrievePieGraphStatistics(UserOrganisationId).Where(e => e.CentreId == id);
            return this.JsonNet(data);
        }
    }
}