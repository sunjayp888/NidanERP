using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
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

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveBatchPrePlacementSearchFields(organisationId, centreId,p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }
    }
}