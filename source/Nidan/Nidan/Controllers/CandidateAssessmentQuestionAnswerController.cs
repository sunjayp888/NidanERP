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
    public class CandidateAssessmentQuestionAnswerController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public CandidateAssessmentQuestionAnswerController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CandidateAssessmentQuestionAnswer
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:CandidateAssessmentQuestionAnswer/create
        //[Authorize(Roles = "User")]
        //public ActionResult Create(int? id)
        //{
        //    var organisationId = UserOrganisationId;
        //    id = id ?? 0;
        //    var candidateAssessment = NidanBusinessService.RetrieveCandidateAssessment(organisationId, id.Value, e => true);
        //    var moduleExamSet = NidanBusinessService.RetrieveModuleExamSet(organisationId, candidateAssessment.ModuleExamSetId, e => true);
        //    var moduleExamQuestionSet = NidanBusinessService.RetrieveModuleExamQuestionSets(organisationId, e => e.ModuleExamSetId == moduleExamSet.ModuleExamSetId);
        //    var viewModel = new CandidateAssessmentQuestionAnswerViewModel()
        //    {
        //        CandidateAssessment = candidateAssessment,
        //        ModuleExamSet = moduleExamSet,
        //        ModuleExamQuestionAnswerGrid= moduleExamQuestionSet
        //    };
        //    return View(viewModel);
        //}

        public ActionResult CandidateAssessmentQuestionAnswer(int? id)
        {
            return View(new CandidateAssessmentQuestionAnswerViewModel()
            {
                CandidateAssessmentId = id.Value
            });
        }


        [HttpPost]
        public ActionResult CandidateAssessmentQuestionAnswerList(int candidateAssessmentId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var candidateAssessment = NidanBusinessService.RetrieveCandidateAssessment(organisationId, candidateAssessmentId, e => true);
            var moduleExamSet = NidanBusinessService.RetrieveModuleExamSet(organisationId, candidateAssessment.ModuleExamSetId, e => true);
            var moduleExamQuestionSet = NidanBusinessService.RetrieveModuleExamQuestionSets(organisationId, e => e.ModuleExamSetId == moduleExamSet.ModuleExamSetId);
            return this.JsonNet(moduleExamQuestionSet);
        }

        //CandidateAssessmentQuestionAnswer
        [HttpPost]
        public ActionResult CandidateAssessmentQuestionAnswer(List<CandidateAssessmentQuestionAnswer> candidateAssessments)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var candidateAssessmentId = candidateAssessments.Select(e => e.CandidateAssessmentId).FirstOrDefault();
            var data = NidanBusinessService.CreateCandidateQuestionAnswer(organisationId, personnelId, candidateAssessmentId, candidateAssessments);
            return this.JsonNet(data);
        }
    }
}