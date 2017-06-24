using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business;
using Nidan.Business.Dto;
using Nidan.Business.Helper;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

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

        public ActionResult DownloadEnquiryCSVByDate(DateTime fromDate, DateTime toDate)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centre = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId);
            var data =
                NidanBusinessService.RetrieveFollowUpDataGrid(UserOrganisationId,
                    p =>
                        (isSuperAdmin || p.CentreId == UserCentreId) && p.CreatedDate >= fromDate &&
                        p.CreatedDate <= toDate).Items.ToList();

            //var output = DataHelper.ToDataTable(data);
            return File(data.ToList().ToCsvStream(), "text/csv", string.Format("{0}_EnquiryReport-{1:yyyy-MM-dd-hh-mm-ss}.csv", centre.Name, DateTime.Now));
        }
    }
}