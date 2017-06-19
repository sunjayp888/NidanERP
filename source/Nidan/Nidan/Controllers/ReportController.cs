using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Extensions;

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
            return View();
        }

        [HttpPost]
        public ActionResult Enquiry(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveEnquiryDataGrid(UserOrganisationId, p => (isSuperAdmin|| p.CentreId==UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}