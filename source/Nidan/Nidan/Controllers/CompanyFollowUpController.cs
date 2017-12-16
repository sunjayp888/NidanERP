using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CompanyFollowUpController : BaseController
    {
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        private readonly DateTime _tomorrow = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(1);
        public CompanyFollowUpController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: CompanyFollowUp
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
    }
}