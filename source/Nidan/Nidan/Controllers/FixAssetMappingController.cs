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
    public class FixAssetMappingController : BaseController
    {
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public FixAssetMappingController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }

        // GET: FixAsset
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: FixAsset
        public ActionResult FixAssetMappingbyAssetClassId(int? assetClassId)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (assetClassId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assetMappings =NidanBusinessService.RetrieveFixAssetDataGrid(organisationId,e => e.AssetClassId == assetClassId && e.CentreId==centreId).Items.ToList();
            var viewModel = new FixAssetMappingViewModel()
            {
                FixAssetMappingList = assetMappings,
                AssetClassId = assetClassId.Value
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isAdmin = User.IsInAnyRoles("Admin");
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssetMappingCountByCentre(UserOrganisationId, p => (isAdmin && p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult FixAssetMappingListByAssetClassId(int assetClassId, Paging paging, List<OrderBy> orderBy)
        {
            bool isAdmin = User.IsInAnyRoles("Admin");
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssetDataGrid(UserOrganisationId, p => (isAdmin && p.CentreId == centreId && p.AssetClassId==assetClassId), orderBy, paging);
            return this.JsonNet(data);
        }


        [HttpPost]
        public ActionResult FixAssetByAssetOutStatusId(int assetOutStatusId, Paging paging, List<OrderBy> orderBy)
        {
            bool isAdmin = User.IsInAnyRoles("Admin");
            var centreId = UserCentreId;
            if (assetOutStatusId == 6)
            {
                var Alldata = NidanBusinessService.RetrieveFixAssetDataGrid(UserOrganisationId, p => (isAdmin && p.CentreId == centreId), orderBy, paging);
                return this.JsonNet(Alldata);
            }
            var data = NidanBusinessService.RetrieveFixAssetDataGrid(UserOrganisationId, p => (isAdmin && p.CentreId == centreId && p.AssetOutStatusId==assetOutStatusId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult AssignType()
        {
            return this.JsonNet(NidanBusinessService.RetrieveAssignTypes(UserOrganisationId, e => true));
        }

        [HttpPost]
        public ActionResult Room()
        {
            var centreId = UserCentreId;
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveRooms(organisationId, e => e.CentreId==centreId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult AssignOutStatus()
        {
            return this.JsonNet(NidanBusinessService.RetrieveAssetOutStates(UserOrganisationId, e => true));
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult FixAssetMappingCheckedList(List<FixAssetMapping> fixAssetMappings, Paging paging, List<OrderBy> orderBy)
        {
            var userOrganisationId = UserOrganisationId;
            var fixAssetMappingChecked = fixAssetMappings.Where(e => e.Ischecked);
            var fixAssetMappingIds = fixAssetMappingChecked.Select(e => e.FixAssetMappingId);
            var data = NidanBusinessService.RetrieveFixAssetMappings(userOrganisationId,e=>fixAssetMappingIds.Contains(e.FixAssetMappingId), orderBy, paging).Items.ToList();
            return this.JsonNet(data);
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult FixAssetMappingByFixAssetMappingId(int fixAssetMappingId)
        {
            var userOrganisationId = UserOrganisationId;
            return this.JsonNet(NidanBusinessService.RetrieveFixAssetMapping(userOrganisationId,fixAssetMappingId));
        }

        [HttpPost]
        public ActionResult UpdateFixAssetMapping(FixAssetMappingViewModel fixAssetMappingViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            try
            {
                var fixAssetMappingData = NidanBusinessService.RetrieveFixAssetMapping(organisationId,fixAssetMappingViewModel.FixAssetMapping.FixAssetMappingId);
                fixAssetMappingViewModel.FixAssetMapping.OrganisationId = organisationId;
                fixAssetMappingViewModel.FixAssetMapping.CentreId = centreId;
                fixAssetMappingViewModel.FixAssetMapping.CreatedBy = personnelId;
                fixAssetMappingData.AssetOutStatusId = fixAssetMappingViewModel.FixAssetMapping.AssetOutStatusId;
                fixAssetMappingData.StatusDate= fixAssetMappingViewModel.FixAssetMapping.StatusDate;
                fixAssetMappingData.StatusCost= fixAssetMappingViewModel.FixAssetMapping.StatusCost;
                fixAssetMappingViewModel.FixAssetMapping = NidanBusinessService.UpdateFixAssetMapping(organisationId, fixAssetMappingData);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }

        }

        [HttpPost]
        public ActionResult AssignFixAssetMapping(List<FixAssetMapping> fixAssetMappings)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            var data = NidanBusinessService.AssignFixAssetMapping(organisationId, personnelId, centreId, fixAssetMappings);
            return this.JsonNet(data);
        }
    }
}