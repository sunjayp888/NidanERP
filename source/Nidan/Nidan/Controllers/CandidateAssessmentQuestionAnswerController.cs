﻿using System;
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

        public ActionResult CandidateAssessmentDetail(int? id)
        {
            return View(new CandidateAssessmentQuestionAnswerViewModel()
            {
                 BatchId= id.Value,
                //ModuleExamSetId = assesmentId.Value 
            });
        }

        public ActionResult CandidateAssessmentQuestionAnswerSet(int? id)
        {
            var organisationId = UserOrganisationId;
            var candidateAssessment = NidanBusinessService.RetrieveCandidateAssessment(organisationId, id.Value, e => true);
            var moduleExamSet = NidanBusinessService.RetrieveModuleExamSet(organisationId, candidateAssessment.ModuleExamSetId, e => true);
            if (Session["Rem_Time"] == null)
            {
                Session["Rem_Time"] = DateTime.Now.AddMinutes(2).ToString("dd-MM-yyyy h:mm:ss tt");
            }
            ViewBag.Rem_Time = Session["Rem_Time"];

            ViewBag.Message = "Modify this template to jump-start your MVC application.";
            return View(new CandidateAssessmentQuestionAnswerViewModel()
            {
                CandidateAssessmentId = id.Value,
                TotalMark=moduleExamSet.TotalMark??0,
                AssessmentName = candidateAssessment.Assessment.Name,
            });
        }


        [HttpPost]
        public ActionResult CandidateAssessmentQuestionAnswerList(int candidateAssessmentId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var candidateAssessment = NidanBusinessService.RetrieveCandidateAssessment(organisationId, candidateAssessmentId, e => true);
            var moduleExamSet = NidanBusinessService.RetrieveModuleExamSet(organisationId, candidateAssessment.ModuleExamSetId, e => true);
            var moduleExamQuestionSet = NidanBusinessService.RetrieveModuleExamQuestionSets(organisationId, e => e.ModuleExamSetId == moduleExamSet.ModuleExamSetId && e.PersonnelId==personnelId,orderBy,paging);
            return this.JsonNet(moduleExamQuestionSet);
        }

        [HttpPost]
        public ActionResult CandidateAssessmentQuestionAnswer(CandidateAssessmentQuestionAnswer candidateAssessment)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.CreateCandidateQuestionAnswer(organisationId, personnelId, centreId, candidateAssessment);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidateAssessmentDetailByBatchIdAssessmentId(int batchId,Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var candidateAssessmentDetail = NidanBusinessService.RetrieveCandidateAssessmentGrid(organisationId, e => e.BatchId==batchId);
            return this.JsonNet(candidateAssessmentDetail);
        }

        //CandidateAssessmentQuestionAnswerbyId
        [HttpPost]
        public ActionResult CandidateAssessmentQuestionAnswerbyId(int candidateAssessmentQuestionAnswerId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var candidateAssessmenbyId = NidanBusinessService.RetrieveCandidateAssessmentQuestionAnswer(organisationId, candidateAssessmentQuestionAnswerId);
            return this.JsonNet(candidateAssessmenbyId);
        }
    }
}