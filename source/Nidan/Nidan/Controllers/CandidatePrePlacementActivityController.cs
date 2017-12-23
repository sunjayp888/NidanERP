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
            var centreId = UserCentreId;
            id = id ?? 0;
            var admission = NidanBusinessService.RetrieveAdmission(organisationId, id.Value);
            var viewModel = new CandidatePrePlacementActivityViewModel
            {
                CandidatePrePlacementActivity = new CandidatePrePlacementActivity()
                {
                    AdmissionId = admission.AdmissionId,
                }
            };
            return View(viewModel);
        }

        // POST: Counselling/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CounsellingViewModel counsellingViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                counsellingViewModel.Counselling.OrganisationId = organisationId;
                counsellingViewModel.Counselling.PersonnelId = personnelId;
                counsellingViewModel.Counselling.CreatedBy = UserPersonnelId;
                counsellingViewModel.Counselling.CentreId = UserCentreId;
                counsellingViewModel.Counselling.CreatedBy = personnelId;
                counsellingViewModel.Counselling = NidanBusinessService.CreateCounselling(organisationId, counsellingViewModel.Counselling);
                return RedirectToAction("Index");
            }
            counsellingViewModel.Courses = new SelectList(
                NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name");
            counsellingViewModel.Sectors = new SelectList(
                NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            return View(counsellingViewModel);
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

        //CandidatePrePlacementActivityByBatchId
        [HttpPost]
        public ActionResult CandidatePrePlacementActivityByBatchId(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCandidatePrePlacementActivityGrid(organisationId, e => (isSuperAdmin || e.CentreId == centreId)&& e.BatchId==batchId,orderBy,paging);
            return this.JsonNet(data);
        }
    }
}