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
    public class BatchTrainerController : BaseController
    {
        public BatchTrainerController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: BatchTrainer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BatchTrainer batchTrainer)
        {
            var result = NidanBusinessService.CreateBatchTrainer(UserOrganisationId, batchTrainer);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int batchId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveBatchTrainers(UserOrganisationId, batchId));
        }

        [HttpPost]
        public ActionResult Delete(int batchId, int trainerId)
        {
            NidanBusinessService.DeleteBatchTrainer(UserOrganisationId, batchId, trainerId);
            return this.JsonNet("");
        }
    }
}