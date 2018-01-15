using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CandidatePostPlacementController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public CandidatePostPlacementController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CandidatePostPlacement
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CandidatePostPlacement/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var admission = _nidanBusinessService.RetrieveAdmissionGrid(organisationId, id.Value, e => true);
            var companies = _nidanBusinessService.RetrieveCompanies(organisationId, e => true);
            var companyBranches = _nidanBusinessService.RetrieveCompanyBranches(organisationId, e => true);
            var viewModel = new CandidatePostPlacementViewModel
            {
                AdmissionId = id.Value,
                BatchId = admission.BatchId ?? 0,
                CandidateName = admission.CandidateName,
                Mobile = admission.Mobile,
                EmailId = admission.EmailId,
                Course = admission.CourseName,
                Companies = new SelectList(companies, "CompanyId", "Name"),
                CompanyBranches = new SelectList(companyBranches, "CompanyBranchId", "CompanyBranchName"),
                CandidatePostPlacement = new CandidatePostPlacement()
                {
                    AdmissionId = id.Value,
                    BatchId = admission.BatchId ?? 0
                }
            };
            return View(viewModel);
        }

        // POST: CandidatePostPlacement/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidatePostPlacementViewModel candidatePostPlacementViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                candidatePostPlacementViewModel.CandidatePostPlacement.OrganisationId = organisationId;
                candidatePostPlacementViewModel.CandidatePostPlacement.CreatedBy = personnelId;
                candidatePostPlacementViewModel.CandidatePostPlacement.CentreId = centreId;
                candidatePostPlacementViewModel.CandidatePostPlacement = _nidanBusinessService.CreateCandidatePostPlacement(organisationId, candidatePostPlacementViewModel.CandidatePostPlacement);
                return RedirectToAction("Index");
            }
            candidatePostPlacementViewModel.Companies = new SelectList(_nidanBusinessService.RetrieveCompanies(organisationId, e => true).ToList(), "CompanyId", "Name");
            candidatePostPlacementViewModel.CompanyBranches = new SelectList(_nidanBusinessService.RetrieveCompanyBranches(organisationId, e => true).ToList(), "CompanyBranchId", "CompanyBranchName");
            return View(candidatePostPlacementViewModel);
        }

        [HttpPost]
        public ActionResult GetBatches()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBatches(organisationId, e => isSuperAdmin || e.CentreId == centreId, null);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidatePostPlacementByBatchId(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCandidatePostPlacements(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.BatchId == batchId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidatePostPlacementByAdmissionId(int admissionId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCandidatePostPlacements(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.AdmissionId == admissionId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveCandidatePostPlacementBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}