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
    public class AssessmentController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public AssessmentController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Assessment
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Assesment/AssessmentByUser
        public ActionResult AssessmentByUser()
        {
            return View(new BaseViewModel());
        }

        public ActionResult AssessmentByTrainer()
        {
            return View(new BaseViewModel());
        }

        //Get:Assessment/create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var assessmentTypes = NidanBusinessService.RetrieveAssessmentTypes(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true);
            var viewModel = new AssessmentViewModel()
            {
                Assessment = new Assessment(),
                Centres = new SelectList(centres, "CentreId", "Name"),
                AssessmentTypes = new SelectList(assessmentTypes, "AssessmentTypeId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name")
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Assessment/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssessmentViewModel assessmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                assessmentViewModel.Assessment.OrganisationId = organisationId;
                assessmentViewModel.Assessment.CreatedBy = personnelId;
                assessmentViewModel.Assessment = NidanBusinessService.CreateAssessment(organisationId, assessmentViewModel.Assessment);
                return RedirectToAction("Index");
            }
            assessmentViewModel.AssessmentTypes = new SelectList(NidanBusinessService.RetrieveAssessmentTypes(organisationId, e => true).ToList());
            assessmentViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            assessmentViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            assessmentViewModel.Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList());
            return View(assessmentViewModel);
        }

        // GET: Assessment/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["AssessmentId"] = id;
            var assessment = NidanBusinessService.RetrieveAssessment(organisationId, id.Value, e => true);
            var courseIds = NidanBusinessService.RetrieveCentreCourses(organisationId, assessment.CentreId, e => e.CentreId == assessment.CentreId).Select(e => e.CourseId);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => courseIds.Contains(e.CourseId));
            var assessmentTypes = NidanBusinessService.RetrieveAssessmentTypes(organisationId, e => true).ToList();
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList();
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var viewModel = new AssessmentViewModel()
            {
                Assessment = assessment,
                Courses = new SelectList(courses, "CourseId", "Name"),
                AssessmentTypes = new SelectList(assessmentTypes, "AssessmentTypeId", "Name"),
                Centres = new SelectList(centres, "CentreId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name"),
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Assessment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssessmentViewModel assessmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                assessmentViewModel.Assessment.OrganisationId = organisationId;
                assessmentViewModel.Assessment.CreatedBy = personnelId;
                assessmentViewModel.Assessment = NidanBusinessService.UpdateAssessment(UserOrganisationId, assessmentViewModel.Assessment);
                return RedirectToAction("Index");
            }
            var viewModel = new AssessmentViewModel()
            {
                Assessment = assessmentViewModel.Assessment
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            bool isUser = User.IsInAnyRoles("User");
            bool isTraier = User.IsInAnyRoles("Trainer");
            var organisationId = UserOrganisationId;
            if (isSuperAdmin)
            {
                var data = NidanBusinessService.RetrieveAssessmentGrid(organisationId, e => true, orderBy, paging);
                return this.JsonNet(data);
            }
            if (isUser)
            {
                var personnelId = UserPersonnelId;
                var data = NidanBusinessService.RetrieveCandidateAssessmentGrid(organisationId, e => e.PersonnelId == personnelId, orderBy, paging);
                return this.JsonNet(data);
            }
            if (isTraier)
            {
                var personnelId = UserPersonnelId;
                var trainer = NidanBusinessService.RetrieveTrainers(organisationId, e => e.PersonnelId == personnelId).FirstOrDefault();
                var batchTrainer = NidanBusinessService.RetrieveBatchTrainers(organisationId, e => e.TrainerId == trainer.TrainerId);
                var batchIds = batchTrainer.Items.Select(e => e.BatchId);
                var data = NidanBusinessService.RetrieveAssessmentGrid(organisationId, e => batchIds.Contains(e.BatchId), orderBy, paging);
                return this.JsonNet(data);
            }
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult GetCourses(int centreId)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBatches(int courseId, int centreId)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveBatches(organisationId, e => e.CourseId == courseId && e.CentreId == centreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidateAssessmentByBatchId(int batchId, Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateAssessmentGrid(organisationId, e => e.BatchId == batchId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Subject()
        {
            return this.JsonNet(NidanBusinessService.RetrieveSubjects(UserOrganisationId, e => true));
        }

        [HttpPost]
        public ActionResult ModuleExamSet()
        {
            return this.JsonNet(NidanBusinessService.RetrieveModuleExamSets(UserOrganisationId, e => true));
        }

        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult CandidateAssessmentCheckedList(List<CandidateAssessmentGrid> assessments, Paging paging, List<OrderBy> orderBy)
        {
            var userOrganisationId = UserOrganisationId;
            var candidateAssessmentChecked = assessments.Where(e => e.Ischecked).ToList();
            var batchId = candidateAssessmentChecked.Select(e => e.BatchId).FirstOrDefault();
            var studentCodes = candidateAssessmentChecked.Select(e => e.StudentCode).ToList();
            if (batchId != null)
            {
                var data = NidanBusinessService.RetrieveCandidateAssessmentGrid(userOrganisationId, e => studentCodes.Contains(e.StudentCode), orderBy, paging).Items.ToList();
                return this.JsonNet(data);
            }
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult AssignModuleExamSet(List<CandidateAssessment> assessments)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var assessmentId = Convert.ToInt32(TempData["AssessmentId"]);
            var data = NidanBusinessService.AssignModuleExamSet(organisationId, personnelId, assessmentId, assessments);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult UpdateCandidateAssessment(CandidateAssessmentViewModel candidateAssessmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            try
            {
                var candidateAssessmentData = NidanBusinessService.RetrieveCandidateAssessment(organisationId, candidateAssessmentViewModel.CandidateAssessment.CandidateAssessmentId, e => true);
                candidateAssessmentViewModel.CandidateAssessment.OrganisationId = organisationId;
                candidateAssessmentViewModel.CandidateAssessment.CreatedBy = personnelId;
                candidateAssessmentData.SubjectId = candidateAssessmentViewModel.CandidateAssessment.SubjectId;
                candidateAssessmentData.ModuleExamSetId = candidateAssessmentViewModel.CandidateAssessment.ModuleExamSetId;
                candidateAssessmentViewModel.CandidateAssessment = NidanBusinessService.UpdateCandidateAssessment(organisationId, candidateAssessmentData);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }

        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ModuleExamSetByAssessmentId(int candidateassessmentId)
        {
            var userOrganisationId = UserOrganisationId;
            return this.JsonNet(NidanBusinessService.RetrieveCandidateAssessment(userOrganisationId, candidateassessmentId, e => true));
        }

    }
}