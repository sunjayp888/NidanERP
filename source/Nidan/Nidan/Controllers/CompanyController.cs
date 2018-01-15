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
    public class CompanyController :  BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public CompanyController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Company
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:Company/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centres = _nidanBusinessService.RetrieveCentres(organisationId, e=>true);
            var viewModel = new CompanyViewModel
            {
                Company = new Company(),
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            return View(viewModel);
        }

        // POST: Company/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyViewModel companyViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                companyViewModel.Company.OrganisationId = organisationId;
                companyViewModel.Company.CentreId = centreId;
                companyViewModel.Company.CreatedBy = personnelId;
                companyViewModel.Company = _nidanBusinessService.CreateCompany(organisationId, companyViewModel.Company);
                return RedirectToAction("Index");
            }
            companyViewModel.Centres = new SelectList(_nidanBusinessService.RetrieveCentres(organisationId, e => true));
            return View(companyViewModel);
        }

        // GET: Company/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e=>true);
            var company = _nidanBusinessService.RetrieveCompany(organisationId, id.Value);
            if (company == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CompanyViewModel()
            {
                Company = company,
                Centres = new SelectList(centres, "CentreId", "Name"),
            };
            return View(viewModel);
        }

        // POST: Company/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyViewModel companyViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                companyViewModel.Company.OrganisationId = organisationId;
                companyViewModel.Company.CentreId = centreId;
                companyViewModel.Company.CreatedBy = personnelId;
                companyViewModel.Company = NidanBusinessService.UpdateCompany(organisationId, companyViewModel.Company);
                return RedirectToAction("Index");
            }
            var viewModel = new CompanyViewModel
            {
                Company = companyViewModel.Company
            };
            return View(viewModel);
        }

        // GET: Company/Edit/{id}
        public ActionResult AddBranch(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var company = _nidanBusinessService.RetrieveCompany(organisationId, id.Value);
            if (company == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CompanyViewModel()
            {
                Company = company,
                Centres = new SelectList(centres, "CentreId", "Name"),
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveCompanies(UserOrganisationId,
                p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}