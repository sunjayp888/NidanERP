using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        private readonly DateTime _today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public ActionResult Index()
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var permissions = NidanBusinessService.RetrievePersonnelPermissions(User.IsInRole("Admin"), organisationId, personnelId);
            var count = NidanBusinessService.RetrieveFollowUps(UserOrganisationId,
               e => e.FollowUpDateTime.Value == _today).Items.Count();
            if (User.IsInRole("User") && !permissions.IsManager)
                return RedirectToAction("Profile", "Personnel", new { id = personnelId });

            var viewModel = new HomeViewModel
            {
                Permissions = permissions,
                FollowUpCount = count,
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
    }
}