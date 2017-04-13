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
    public class SubjectTrainerController : BaseController
    {
        // GET: SubjectTrainer
        public ActionResult Index()
        {
            return View();
        }

        public SubjectTrainerController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult Create(SubjectTrainer subjectTrainer)
        {
            var result = NidanBusinessService.CreateSubjectTrainer(UserOrganisationId, subjectTrainer);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int subjectId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveSubjectTrainers(UserOrganisationId, subjectId));
        }

        [HttpPost]
        public ActionResult Delete(int subjectId, int trainerId)
        {
            NidanBusinessService.DeleteSubjectTrainer(UserOrganisationId, subjectId, trainerId);
            return this.JsonNet("");
        }
    }
}