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
    public class ProjectController : BaseController
    {

        private ApplicationRoleManager _roleManager;

        public ProjectController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isAdmin = User.IsInAnyRoles("Admin");
            var data = NidanBusinessService.RetrieveProjects(UserOrganisationId, p => isAdmin, orderBy, paging);
            return this.JsonNet(data);
        }

    }
}