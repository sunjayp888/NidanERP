using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CompanyBranchController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public CompanyBranchController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CompanyBranch
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CompanyBranch/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var sectors = _nidanBusinessService.RetrieveSectors(organisationId, e => true);
            var company = _nidanBusinessService.RetrieveCompany(organisationId, id.Value);
            var viewModel = new CompanyBranchViewModel
            {
                Company = company,
                CompanyId = id.Value,
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                CompanyBranch = new CompanyBranch()
                {
                    CompanyId = company.CompanyId,
                    Company = company
                }
            };
            return View(viewModel);
        }

        // POST: Counselling/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyBranchViewModel companyBranchViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                companyBranchViewModel.CompanyBranch.OrganisationId = organisationId;
                companyBranchViewModel.CompanyBranch.CreatedBy = personnelId;
                companyBranchViewModel.CompanyBranch.CentreId = centreId;
                companyBranchViewModel.CompanyBranch = _nidanBusinessService.CreateCompanyBranch(organisationId, companyBranchViewModel.CompanyBranch);
                return RedirectToAction("Edit", "Company", new { id = companyBranchViewModel.CompanyBranch.CompanyId });
            }
            companyBranchViewModel.Sectors = new SelectList(_nidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            return View(companyBranchViewModel);
        }

        // GET: Company/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var companyBranch = _nidanBusinessService.RetrieveCompanyBranch(organisationId, id.Value);
            var sectors = _nidanBusinessService.RetrieveSectors(organisationId, e => true);
            var company = _nidanBusinessService.RetrieveCompany(organisationId, companyBranch.CompanyId);
            var viewModel = new CompanyBranchViewModel()
            {
                Company = company,
                CompanyId=companyBranch.CompanyId,
                CompanyBranch = companyBranch,
                Sectors = new SelectList(sectors, "SectorId", "Name")
            };
            return View(viewModel);
        }

        // POST: Company/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyBranchViewModel companyBranchViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                companyBranchViewModel.CompanyBranch.OrganisationId = organisationId;
                companyBranchViewModel.CompanyBranch.CentreId = centreId;
                companyBranchViewModel.CompanyBranch.CreatedBy = personnelId;
                companyBranchViewModel.CompanyBranch = NidanBusinessService.UpdateCompanyBranch(organisationId, companyBranchViewModel.CompanyBranch);
                return RedirectToAction("Edit", "Company", new { id = companyBranchViewModel.CompanyBranch.CompanyId });
            }
            var viewModel = new CompanyBranchViewModel
            {
                CompanyBranch = companyBranchViewModel.CompanyBranch
            };
            companyBranchViewModel.Sectors = new SelectList(_nidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CompanyBranchByCompanyId(int companyId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var organisationId = UserOrganisationId;
            var data = _nidanBusinessService.RetrieveCompanyBranches(organisationId,p => (isSuperAdmin || p.CentreId == centreId) && p.CompanyId == companyId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}