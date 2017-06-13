using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Extensions;

namespace Nidan.Controllers
{
    public class OtherFeeProjectController : BaseController
    {
        // GET: OtherFeeProject
        public ActionResult Index()
        {
            return View();
        }

        public OtherFeeProjectController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult Create(OtherFeeProject otherFeeProject)
        {
            var result = NidanBusinessService.CreateOtherFeeProject(UserOrganisationId, otherFeeProject);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int otherFeeId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveOtherFeeProjects(UserOrganisationId, UserCentreId, otherFeeId));
        }
    }
}