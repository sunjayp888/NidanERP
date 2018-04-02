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
    public class BatchPrePlacementController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public BatchPrePlacementController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: BatchPrePlacement
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var centres = _nidanBusinessService.RetrieveCentres(organisationId, e=>true);
            var batches = _nidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId==centreId);
            var viewModel = new BatchPrePlacementViewModel
            {
                BatchPrePlacement = new BatchPrePlacement(),
                Centres = new SelectList(centres, "CentreId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name"),
            };
            return View(viewModel);
        }

        // POST: BatchPrePlacement/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchPrePlacementViewModel batchPrePlacementViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                batchPrePlacementViewModel.BatchPrePlacement.OrganisationId = organisationId;
                batchPrePlacementViewModel.BatchPrePlacement.CentreId = centreId;
                batchPrePlacementViewModel.BatchPrePlacement.CreatedBy = personnelId;
                batchPrePlacementViewModel.BatchPrePlacement.BatchId = batchPrePlacementViewModel.BatchId;
                batchPrePlacementViewModel.BatchPrePlacement = NidanBusinessService.CreateBatchPrePlacement(UserOrganisationId, batchPrePlacementViewModel.BatchPrePlacement);
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", new { id = batchPrePlacementViewModel.BatchPrePlacement.BatchPrePlacementId });
            }
            batchPrePlacementViewModel.Centres = new SelectList(_nidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            batchPrePlacementViewModel.Batches = new SelectList(_nidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId == centreId).ToList());
            return View(batchPrePlacementViewModel);
        }

        // GET: BatchPrePlacement/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centres = _nidanBusinessService.RetrieveCentres(organisationId, e => true);
            var batches = _nidanBusinessService.RetrieveBatches(organisationId, e => e.CentreId == centreId);
            var batchPrePlacement = _nidanBusinessService.RetrieveBatchPrePlacement(organisationId, id.Value);
            var candidatePrePlacements= _nidanBusinessService.RetrieveCandidatePrePlacements(organisationId,centreId,e=>e.BatchPrePlacementId==id.Value).Items.Select(e=>e.PrePlacementActivityId).ToList();
            var prePlacementActivities =_nidanBusinessService.RetrievePrePlacementActivities(organisationId, e => true);
            if (batchPrePlacement == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchPrePlacementViewModel
            {
                BatchPrePlacement = batchPrePlacement,
                Centres = new SelectList(centres, "CentreId", "Name"),
                CentreId=batchPrePlacement.CentreId,
                Batches = new SelectList(batches, "BatchId", "Name"),
                BatchId=batchPrePlacement.BatchId,
                PrePlacementActivities=new SelectList(prePlacementActivities, "PrePlacementActivityId","Name")
            };
            return View(viewModel);
        }

        // POST: BatchPrePlacement/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BatchPrePlacementViewModel batchPrePlacementViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                batchPrePlacementViewModel.BatchPrePlacement.OrganisationId = organisationId;
                batchPrePlacementViewModel.BatchPrePlacement.CentreId = centreId;
                batchPrePlacementViewModel.BatchPrePlacement.CreatedBy = personnelId;
                batchPrePlacementViewModel.BatchPrePlacement.BatchId = batchPrePlacementViewModel.BatchId;
                batchPrePlacementViewModel.BatchPrePlacement = _nidanBusinessService.UpdateBatchPrePlacement(UserOrganisationId, batchPrePlacementViewModel.BatchPrePlacement);
                return RedirectToAction("Index");
            }
            var viewModel = new BatchPrePlacementViewModel
            {
                BatchPrePlacement = batchPrePlacementViewModel.BatchPrePlacement
            };
            return View(viewModel);
        }

        // GET: BatchPrePlacement/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var batchPrePlacement = _nidanBusinessService.RetrieveBatchPrePlacement(organisationId,id.Value);
            if (batchPrePlacement == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BatchPrePlacementViewModel
            {
                BatchPrePlacement = batchPrePlacement
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveBatchPrePlacementSearchFields(organisationId, centreId,p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveBatchPrePlacementBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveBatchPrePlacementSearchFields(organisationId,centreId, e => (isSuperAdmin || e.CentreId == centreId) && e.ScheduledStartDate>= fromDate && e.ScheduledStartDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        //RetrieveCandidatePrePlacementByBatchPrePlacementId
        [HttpPost]
        public ActionResult RetrieveCandidatePrePlacementByBatchPrePlacementId(int batchPrePlacementId, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = _nidanBusinessService.RetrieveCandidatePrePlacementGrids(organisationId, centreId, e => (isSuperAdmin || e.CentreId == centreId)&& e.BatchPrePlacementId==batchPrePlacementId, orderBy, paging);
            return this.JsonNet(data);
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult GetBatchPrePlacement(int id)
        {
            var userOrganisationId = UserOrganisationId;
            return this.JsonNet(_nidanBusinessService.RetrieveBatchPrePlacement(userOrganisationId, id));
        }

        [HttpPost]
        public ActionResult SaveCandidatePrePlacementActivity(CandidatePrePlacementViewModel candidatePrePlacementViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            try
            {
                candidatePrePlacementViewModel.CandidatePrePlacement.OrganisationId = organisationId;
                candidatePrePlacementViewModel.CandidatePrePlacement.CentreId = centreId;
                candidatePrePlacementViewModel.CandidatePrePlacement.CreatedBy = personnelId;
                int id = candidatePrePlacementViewModel.CandidatePrePlacement.CandidatePrePlacementId;
                if (id != 0)
                {
                    candidatePrePlacementViewModel.CandidatePrePlacement = _nidanBusinessService.UpdateCandidatePrePlacement(organisationId,candidatePrePlacementViewModel.CandidatePrePlacement);
                    return this.JsonNet(true);
                }
                var batchPrePlacement = _nidanBusinessService.RetrieveBatchPrePlacement(organisationId, candidatePrePlacementViewModel.CandidatePrePlacement.BatchPrePlacementId);
                var admissionIds = _nidanBusinessService.RetrieveAdmissions(organisationId, e => e.BatchId == batchPrePlacement.BatchId).Items.Select(e => e.AdmissionId).ToList();
                candidatePrePlacementViewModel.CandidatePrePlacement =_nidanBusinessService.CreateCandidatePrePlacement(organisationId,candidatePrePlacementViewModel.CandidatePrePlacement, admissionIds);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }
        }


        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult GetCandidatePrePlacement(int id)
        {
            var userOrganisationId = UserOrganisationId;
            var data = _nidanBusinessService.RetrieveCandidatePrePlacement(userOrganisationId, id);
            return this.JsonNet(data);
        }

    }
}