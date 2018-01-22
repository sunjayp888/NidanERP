using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CompanyFollowUpController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        private readonly DateTime _tomorrow = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(1);
        public CompanyFollowUpController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CompanyFollowUp
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CompanyFollowUp/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            TempData["CompanyFollowUpId"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var companyFollowUp = _nidanBusinessService.RetrieveCompanyFollowUp(organisationId, id.Value);
            if (companyFollowUp == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CompanyFollowUpViewModel
            {
                CompanyFollowUp = companyFollowUp,
                HRContact1=companyFollowUp.CompanyBranch.HRContact1,
                HREmail1=companyFollowUp.CompanyBranch.HREmail1,
                HRName1= companyFollowUp.CompanyBranch.HRName1,
                HRContact2 = companyFollowUp.CompanyBranch.HRContact2 ?? 0,
                HREmail2 = companyFollowUp.CompanyBranch.HREmail2,
                HRName2 = companyFollowUp.CompanyBranch.HRName2

            };
            return View(viewModel);
        }

        // POST: CompanyFollowUp/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyFollowUpViewModel companyFollowUpViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                companyFollowUpViewModel.CompanyFollowUp.OrganisationId = organisationId;
                companyFollowUpViewModel.CompanyFollowUp.CentreId = centreId;
                companyFollowUpViewModel.CompanyFollowUp.CreatedBy = personnelId;
                companyFollowUpViewModel.CompanyFollowUp = _nidanBusinessService.UpdateCompanyFollowUp(organisationId, companyFollowUpViewModel.CompanyFollowUp);
                return RedirectToAction("Index");
            }
            var viewModel = new CompanyFollowUpViewModel
            {
                CompanyFollowUp = companyFollowUpViewModel.CompanyFollowUp,
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveCompanyFollowUpGrid(organisationId,p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        //CompanyFollowUpHistoryList
        [HttpPost]
        public ActionResult CompanyFollowUpHistoryList(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var companyFollowUpId = Convert.ToInt32(TempData["CompanyFollowUpId"]);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveCompanyFollowUpHistoryGrid(organisationId,e => (isSuperAdmin || e.CentreId == centreId) && e.CompanyFollowUpId == companyFollowUpId, orderBy,paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveCompanyFollowUpBySearchKeyword(organisationId, searchKeyword, p => isSuperAdmin, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}