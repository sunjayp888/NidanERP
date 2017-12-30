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
    public class CandidatePrePlacementActivityController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public CandidatePrePlacementActivityController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }

        // GET: CandidatePrePlacementActivity
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: CandidatePrePlacementActivity/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            id = id ?? 0;
            var admission = _nidanBusinessService.RetrieveAdmissionGrid(organisationId, id.Value,e=>true);
            var viewModel = new CandidatePrePlacementActivityViewModel
            {
                AdmissionId=id.Value,
                BatchId=admission.BatchId??0,
                CandidateName=admission.CandidateName,
                Mobile=admission.Mobile,
                EmailId=admission.EmailId,
                Course=admission.CourseName,
                CandidatePrePlacementActivity = new CandidatePrePlacementActivity()
                {
                    AdmissionId =id.Value,
                    BatchId=admission.BatchId??0
                }
            };
            return View(viewModel);
        }

        // POST: CandidatePrePlacementActivity/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidatePrePlacementActivityViewModel candidatePrePlacementActivityViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            if (ModelState.IsValid)
            {
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.OrganisationId = organisationId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.CreatedBy = personnelId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.CentreId = centreId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity = _nidanBusinessService.CreateCandidatePrePlacementActivity(organisationId, candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity);
                return RedirectToAction("Index");
            }
            return View(candidatePrePlacementActivityViewModel);
        }

        // GET: CandidatePrePlacementActivity/Edit/{id}
        public ActionResult Edit(int? id)

        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidatePrePlacementActivity = _nidanBusinessService.RetrieveCandidatePrePlacementActivity(organisationId, id.Value);
            if (candidatePrePlacementActivity == null)
            {
                return HttpNotFound();
            }
            var admission = _nidanBusinessService.RetrieveAdmissionGrid(organisationId,candidatePrePlacementActivity.AdmissionId, e => true);
            var viewModel = new CandidatePrePlacementActivityViewModel()
            {
                CandidatePrePlacementActivity = candidatePrePlacementActivity,
                CandidateName = admission.CandidateName,
                Mobile = admission.Mobile,
                EmailId = admission.EmailId,
                Course = admission.CourseName,
            };
            return View(viewModel);
        }

        // POST: CandidatePrePlacementActivity/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidatePrePlacementActivityViewModel candidatePrePlacementActivityViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.OrganisationId = organisationId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.CentreId = centreId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity.CreatedBy = personnelId;
                candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity = NidanBusinessService.UpdateCandidatePrePlacementActivity(UserOrganisationId, candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity);
                return RedirectToAction("Index");
            }
            var viewModel = new CandidatePrePlacementActivityViewModel
            {
                CandidatePrePlacementActivity = candidatePrePlacementActivityViewModel.CandidatePrePlacementActivity
            };
            return View(viewModel);
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
        public ActionResult CandidatePrePlacementActivityByBatchId(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCandidatePrePlacementActivityGrid(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.BatchId==batchId,orderBy,paging);
            return this.JsonNet(data);
        }
    }
}