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
    public class AssesmentController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public AssesmentController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Assesment
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //Get:Assesment/create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var assesmentTypes = NidanBusinessService.RetrieveAssesmentTypes(organisationId, e => true);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true);
            var viewModel = new AssesmentViewModel()
            {
                Assesment = new Assesment(),
                Centres = new SelectList(centres, "CentreId", "Name"),
                AssesmentTypes = new SelectList(assesmentTypes,"AssesmentTypeId","Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name")
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Assesment/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssesmentViewModel assesmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                assesmentViewModel.Assesment.OrganisationId = organisationId;
                assesmentViewModel.Assesment.CreatedBy = personnelId;
                assesmentViewModel.Assesment = NidanBusinessService.CreateAssesment(organisationId, assesmentViewModel.Assesment);
                return RedirectToAction("Index");
            }
            assesmentViewModel.AssesmentTypes = new SelectList(NidanBusinessService.RetrieveAssesmentTypes(organisationId, e => true).ToList());
            assesmentViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            assesmentViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            assesmentViewModel.Batches = new SelectList(NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList());
            return View(assesmentViewModel);
        }

        // GET: Assesment/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["AssesmentId"] = id;
            var assesment = NidanBusinessService.RetrieveAssesment(organisationId, id.Value,e=>true);
            var courseIds = NidanBusinessService.RetrieveCentreCourses(organisationId,assesment.CentreId,e=>e.CentreId==assesment.CentreId).Select(e=>e.CourseId);
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => courseIds.Contains(e.CourseId));
            var assesmentTypes = NidanBusinessService.RetrieveAssesmentTypes(organisationId, e => true).ToList();
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList();
            var batches = NidanBusinessService.RetrieveBatches(organisationId, e => true).ToList();
            var viewModel = new AssesmentViewModel()
            {
                Assesment = assesment,
                Courses = new SelectList(courses, "CourseId", "Name"),
                AssesmentTypes = new SelectList(assesmentTypes, "AssesmentTypeId", "Name"),
                Centres = new SelectList(centres, "CentreId", "Name"),
                Batches = new SelectList(batches, "BatchId", "Name"),
            };
            viewModel.HoursList = new SelectList(viewModel.HoursType, "Id", "Name");
            viewModel.MinutesList = new SelectList(viewModel.MinutesType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Assesment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssesmentViewModel assesmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                assesmentViewModel.Assesment.OrganisationId = organisationId;
                assesmentViewModel.Assesment.CreatedBy = personnelId;
                assesmentViewModel.Assesment = NidanBusinessService.UpdateAssesment(UserOrganisationId, assesmentViewModel.Assesment);
                return RedirectToAction("Index");
            }
            var viewModel = new AssesmentViewModel()
            {
                Assesment = assesmentViewModel.Assesment
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveAssesmentGrid(organisationId, e => true, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourses(int centreId)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e=>e.CentreId==centreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetBatches(int courseId,int centreId)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveBatches(organisationId, e=>e.CourseId==courseId && e.CentreId==centreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult CandidateAssesmentByBatchId(int batchId,Paging paging, List<OrderBy> orderBy)
        {
            var organisationId = UserOrganisationId;
            var data = NidanBusinessService.RetrieveCandidateAssesmentGrid(organisationId, batchId,e=>true, orderBy, paging);
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
        public ActionResult CandidateAssesmentCheckedList(List<CandidateAssesmentGrid> assesments, Paging paging, List<OrderBy> orderBy)
        {
            var userOrganisationId = UserOrganisationId;
            var candidateAssesmentChecked = assesments.Where(e => e.Ischecked).ToList();
            var batchId = candidateAssesmentChecked.Select(e => e.BatchId).FirstOrDefault();
            var studentCodes = candidateAssesmentChecked.Select(e => e.StudentCode).ToList();
            if (batchId != null)
            {
                var data = NidanBusinessService.RetrieveCandidateAssesmentGrid(userOrganisationId,batchId.Value, e => studentCodes.Contains(e.StudentCode), orderBy, paging).Items.ToList();
                return this.JsonNet(data);
            }
            return this.JsonNet("");
        }

        [HttpPost]
        public ActionResult AssignModuleExamSet(List<CandidateAssesment> assesments)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var assesmentId = Convert.ToInt32(TempData["AssesmentId"]);
            var data = NidanBusinessService.AssignModuleExamSet(organisationId, personnelId, assesmentId, assesments);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult UpdateCandidateAssesment(CandidateAssesmentViewModel candidateAssesmentViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            try
            {
                var candidateAssesmentData = NidanBusinessService.RetrieveCandidateAssesment(organisationId, candidateAssesmentViewModel.CandidateAssesment.CandidateAssesmentId,e=>true);
                candidateAssesmentViewModel.CandidateAssesment.OrganisationId = organisationId;
                candidateAssesmentViewModel.CandidateAssesment.CreatedBy = personnelId;
                candidateAssesmentData.SubjectId = candidateAssesmentViewModel.CandidateAssesment.SubjectId;
                candidateAssesmentData.ModuleExamSetId = candidateAssesmentViewModel.CandidateAssesment.ModuleExamSetId;
                candidateAssesmentViewModel.CandidateAssesment = NidanBusinessService.UpdateCandidateAssesment(organisationId, candidateAssesmentData);
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }

        }
    }
}