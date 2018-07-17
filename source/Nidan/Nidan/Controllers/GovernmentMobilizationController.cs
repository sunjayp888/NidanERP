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

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveGovernmentMobilizations(UserOrganisationId,p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}