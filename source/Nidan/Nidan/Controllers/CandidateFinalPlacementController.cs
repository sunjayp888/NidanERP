using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CandidateFinalPlacementController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly IDocumentService _documentService;
        public CandidateFinalPlacementController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
            _documentService = documentService;
        }
        // GET: CandidateFinalPlacement
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CandidateFinalPlacement/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var admission = _nidanBusinessService.RetrieveAdmissionGrid(organisationId, id.Value, e => true);
            var companies = _nidanBusinessService.RetrieveCompanies(organisationId, e => true);
            var companyBranches = _nidanBusinessService.RetrieveCompanyBranches(organisationId, e => true);
            var placementStates = _nidanBusinessService.RetrievePlacementStates(organisationId, e => true);
            var candidateFinalPlacementLastRecord = _nidanBusinessService.RetrieveCandidateFinalPlacements(organisationId, e => e.AdmissionId == id.Value).Items.LastOrDefault();
            var candidatefinalPlacement = candidateFinalPlacementLastRecord.CandidateFinalPlacementId != null
                ? new CandidateFinalPlacement()
                {
                    AdmissionId = id.Value,
                    BatchId = admission.BatchId ?? 0,
                    IsFinalPlacementDone=candidateFinalPlacementLastRecord.IsFinalPlacementDone
                }
                : new CandidateFinalPlacement()
                {
                    AdmissionId = id.Value,
                    BatchId = admission.BatchId ?? 0
                };
            var viewModel = new CandidateFinalPlacementViewModel
            {
                AdmissionId = id.Value,
                BatchId = admission.BatchId ?? 0,
                CandidateName = admission.CandidateName,
                Mobile = admission.Mobile,
                EmailId = admission.EmailId,
                Course = admission.CourseName,
                //CandidateFinalPlacementId = candidateFinalPlacementLastRecord?.CandidateFinalPlacementId ?? 0,
                Companies = new SelectList(companies, "CompanyId", "Name"),
                CompanyBranches = new SelectList(companyBranches, "CompanyBranchId", "CompanyBranchName"),
                PlacementStates = new SelectList(placementStates, "PlacementStatusId", "Name"),
                CandidateFinalPlacement = candidatefinalPlacement
            };
            return View(viewModel);
        }

        // POST: CandidateFinalPlacement/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateFinalPlacementViewModel candidateFinalPlacementViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                candidateFinalPlacementViewModel.CandidateFinalPlacement.OrganisationId = organisationId;
                candidateFinalPlacementViewModel.CandidateFinalPlacement.CreatedBy = personnelId;
                candidateFinalPlacementViewModel.CandidateFinalPlacement.CentreId = centreId;
                candidateFinalPlacementViewModel.CandidateFinalPlacement = _nidanBusinessService.CreateCandidateFinalPlacement(organisationId, candidateFinalPlacementViewModel.CandidateFinalPlacement);
                return RedirectToAction("Index");
            }
            candidateFinalPlacementViewModel.Companies = new SelectList(_nidanBusinessService.RetrieveCompanies(organisationId, e => true).ToList(), "CompanyId", "Name");
            candidateFinalPlacementViewModel.CompanyBranches = new SelectList(_nidanBusinessService.RetrieveCompanyBranches(organisationId, e => true).ToList(), "CompanyBranchId", "CompanyBranchName");
            candidateFinalPlacementViewModel.PlacementStates = new SelectList(_nidanBusinessService.RetrievePlacementStates(organisationId, e => true).ToList(), "PlacementStatusId", "Name");
            return View(candidateFinalPlacementViewModel);
        }

        [HttpPost]
        public ActionResult GetBatches()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveBatches(organisationId, e => isSuperAdmin || e.CentreId == centreId, null);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidateFinalPlacementByBatchId(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveCandidateFinalPlacementGrid(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.BatchId == batchId, orderBy, paging).Items.LastOrDefault();
            var result = new List<CandidateFinalPlacementGrid>
            {
                data
            };
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult CandidateFinalPlacementByAdmissionId(int admissionId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = _nidanBusinessService.RetrieveCandidateFinalPlacementGrid(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.AdmissionId == admissionId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveCandidateFinalPlacementBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging).Items.LastOrDefault();
            var result = new List<CandidateFinalPlacementGrid>
            {
                data
            };
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrievePlacementDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var palcementData = _nidanBusinessService.RetrieveCandidateFinalPlacements(organisationId, e => e.AdmissionId.ToString() == documentViewModel.StudentCode).Items.FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                palcementData?.Centre.Name, "Placement Document", documentViewModel.Attachment.FileName,
                documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = _nidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }
    }
}