using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Extensions;

namespace Nidan.Controllers
{
    public class ExpenseProjectController : BaseController
    {
        // GET: ExpenseProject
        public ActionResult Index()
        {
            return View();
        }

        public ExpenseProjectController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult Create(ExpenseProject expenseProject)
        {
            var result = NidanBusinessService.CreateExpenseProject(UserOrganisationId, expenseProject);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int expenseId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveExpenseProjects(UserOrganisationId, UserCentreId, expenseId));
        }

        [HttpPost]
        public ActionResult Delete(int expenseId, int projectId)
        {
            NidanBusinessService.DeleteExpenseProject(UserOrganisationId, expenseId, projectId);
            return this.JsonNet("");
        }
    }
}