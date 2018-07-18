using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class GovernmentMobilizationController : BaseController
    {
        public GovernmentMobilizationController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: GovernmentMobilization
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: GovernmentMobilization/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e=>true);
            var districtBlocks = NidanBusinessService.RetrieveDistrictBlocks(organisationId, e =>true);
            var blockPanchayats = NidanBusinessService.RetrieveBlockPanchayats(organisationId, e => true);
            var qualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var castecategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var viewModel = new GovernmentMobilizationViewModel
            {
                GovernmentMobilization = new GovernmentMobilization(),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                DistrictBlocks = new SelectList(districtBlocks, "DistrictBlockId", "Name"),
                BlockPanchayats = new SelectList(blockPanchayats, "BlockPanchayatId", "Name"),
                Qualifications = new SelectList(qualifications, "QualificationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(castecategories, "CasteCategoryId", "Caste")
            };
            return View(viewModel);
        }

        // POST: GovernmentMobilization/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GovernmentMobilizationViewModel governmentMobilizationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                governmentMobilizationViewModel.GovernmentMobilization.OrganisationId = organisationId;
                governmentMobilizationViewModel.GovernmentMobilization.CentreId = centreId;
                governmentMobilizationViewModel.GovernmentMobilization.CreatedBy = personnelId;
                governmentMobilizationViewModel.GovernmentMobilization = NidanBusinessService.CreateGovernmentMobilization(UserOrganisationId, governmentMobilizationViewModel.GovernmentMobilization);
                return RedirectToAction("Index");
            }
            governmentMobilizationViewModel.Districts = new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            governmentMobilizationViewModel.DistrictBlocks = new SelectList(NidanBusinessService.RetrieveDistrictBlocks(organisationId, e => true).ToList());
            governmentMobilizationViewModel.BlockPanchayats = new SelectList(NidanBusinessService.RetrieveBlockPanchayats(organisationId, e => true));
            governmentMobilizationViewModel.Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            governmentMobilizationViewModel.Religions = new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true));
            governmentMobilizationViewModel.CasteCategories = new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            return View(governmentMobilizationViewModel);
        }

        // GET: GovernmentMobilization/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var governmentMobilizationmobilization = NidanBusinessService.RetrieveGovernmentMobilization(UserOrganisationId, id.Value);
            if (governmentMobilizationmobilization == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GovernmentMobilizationViewModel
            {
                GovernmentMobilization = governmentMobilizationmobilization,
                Districts = new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList(), "DistrictId", "Name"),
                DistrictBlocks = new SelectList(NidanBusinessService.RetrieveDistrictBlocks(organisationId, e => true).ToList(), "DistrictBlockId", "Name"),
                BlockPanchayats = new SelectList(NidanBusinessService.RetrieveBlockPanchayats(organisationId, e => true), "BlockPanchayatId", "Name"),
                Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList(), "QualificationId", "Name"),
                Religions = new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true), "ReligionId", "Name"),
                CasteCategories = new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList(), "CasteCategoryId", "Caste")
            };
            return View(viewModel);
        }

        // POST: GovernmentMobilization/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GovernmentMobilizationViewModel governmentMobilizationViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                governmentMobilizationViewModel.GovernmentMobilization.OrganisationId = organisationId;
                governmentMobilizationViewModel.GovernmentMobilization.CentreId = centreId;
                governmentMobilizationViewModel.GovernmentMobilization.CreatedBy = personnelId;
                governmentMobilizationViewModel.GovernmentMobilization = NidanBusinessService.UpdateGovernmentMobilization(UserOrganisationId, governmentMobilizationViewModel.GovernmentMobilization);
                return RedirectToAction("Index");
            }
            var viewModel = new GovernmentMobilizationViewModel
            {
                GovernmentMobilization = governmentMobilizationViewModel.GovernmentMobilization
            };
            return View(viewModel);
        }

        // GET: GovernmentMobilization/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var governmentMobilizationGrid = NidanBusinessService.RetrieveGovernmentMobilizations(organisationId, e => e.GovernmentMobilizationId == id).Items.FirstOrDefault();
            if (governmentMobilizationGrid == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GovernmentMobilizationViewModel
            {
                GovernmentMobilizationGrid = governmentMobilizationGrid
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveGovernmentMobilizations(UserOrganisationId,p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetDistrictBlock(int districtId)
        {
            var data = NidanBusinessService.RetrieveDistrictBlocks(UserOrganisationId, e => e.DistrictId == districtId ).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBlockPanchayat(int districtBlockId)
        {
            var data = NidanBusinessService.RetrieveBlockPanchayats(UserOrganisationId, e => e.BlockPanchayatId == districtBlockId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveGovernmentMobilizationBySearchKeyword(organisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveGovernmentMobilizations(organisationId, e => (isSuperAdmin || e.CentreId == centreId) && e.CreatedDate >= fromDate && e.CreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}