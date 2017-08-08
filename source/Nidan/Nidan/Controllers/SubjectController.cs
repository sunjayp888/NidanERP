using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Excel;
using Nidan.Business.Dto;
using Nidan.Business.Extensions;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class SubjectController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        public SubjectController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Subject
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Subject/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var coursetypes = NidanBusinessService.RetrieveCourseTypes(organisationId, e => true);

            var viewModel = new SubjectViewModel
            {
                Subject = new Subject(),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Name"),
                CourseTypes = new SelectList(coursetypes, "CourseTypeId", "Name"),
                SelectedCourseIds = new List<int> { },
                SelectedTrainerIds = new List<int> { }
            };

            return View(viewModel);
        }

        // POST: Subject/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectViewModel subjectViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                subjectViewModel.Subject.OrganisationId = UserOrganisationId;
                subjectViewModel.Subject = NidanBusinessService.CreateSubject(UserOrganisationId, subjectViewModel.Subject, subjectViewModel.SelectedCourseIds, subjectViewModel.SelectedTrainerIds);
                return RedirectToAction("Edit", new { id = subjectViewModel.Subject.SubjectId });
            }
            subjectViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            subjectViewModel.Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(organisationId, e => true).ToList());
            subjectViewModel.CourseTypes = new SelectList(NidanBusinessService.RetrieveCourseTypes(organisationId, e => true).ToList());

            return View(subjectViewModel);
        }

        // GET: Subject/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["SubjectId"] = id;
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var trainers = NidanBusinessService.RetrieveTrainers(organisationId, e => true);
            var courseTypes = NidanBusinessService.RetrieveCourseTypes(organisationId, e => true);
            var subject = NidanBusinessService.RetrieveSubject(UserOrganisationId, id.Value);
            var selectedCourseIds = NidanBusinessService.RetrieveSubjectCourses(organisationId, s => s.SubjectId == id.Value);

            if (subject == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SubjectViewModel
            {
                Subject = subject,
                Courses = new SelectList(courses, "CourseId", "Name"),
                Trainers = new SelectList(trainers, "TrainerId", "Name"),
                CourseTypes = new SelectList(courseTypes, "CourseTypeId", "Name"),
                SelectedCourseIds = subject?.SubjectCourses.Select(e => e.CourseId).ToList(),
                //SelectedCourseIds = subject?.SubjectCourses..ToList(),
                SelectedTrainerIds = subject?.SubjectTrainers.Select(e => e.TrainerId).ToList()
            };
            return View(viewModel);
        }

        // POST: Subject/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectViewModel subjectViewModel)
        {
            if (ModelState.IsValid)
            {
                subjectViewModel.Subject.OrganisationId = UserOrganisationId;
                subjectViewModel.Subject = NidanBusinessService.UpdateSubject(UserOrganisationId, subjectViewModel.Subject, subjectViewModel.SelectedCourseIds, subjectViewModel.SelectedTrainerIds);
                return RedirectToAction("Edit", new { id = subjectViewModel.Subject.SubjectId });
            }
            var viewModel = new SubjectViewModel
            {
                Subject = subjectViewModel.Subject
            };
            return View(viewModel);
        }

        public ActionResult Upload()
        {
            var subjectId = Convert.ToInt32(TempData["SubjectId"]);
            var viewModel = new SubjectViewModel
            {
                SubjectId = subjectId,
                Files = new List<HttpPostedFileBase>(),
                Trainers = new SelectList(NidanBusinessService.RetrieveTrainers(UserOrganisationId, e => true).ToList(), "TrainerId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(SubjectViewModel subjectViewModel)
        {
            var subjectId = Convert.ToInt32(TempData["SubjectId"]);
            if (ModelState.IsValid)
            {
                if (subjectViewModel.Files != null && subjectViewModel.Files[0].ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    var stream = subjectViewModel.Files[0].InputStream;
                    // We return the interface, so that
                    IExcelDataReader reader = null;

                    if (subjectViewModel.Files[0].FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (subjectViewModel.Files[0].FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("", "This file format is not supported");
                        return View();
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    DataSet result = reader.AsDataSet();
                    reader.Close();
                    var sessions = result.Tables[0].ToList<Session>();
                    NidanBusinessService.UploadSession(UserOrganisationId, sessions.ToList());
                    return RedirectToAction("Edit", new { id = subjectId });
                }
                else
                {
                    ModelState.AddModelError("", "Please Upload Your file");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            var courseId = Convert.ToInt32(TempData["CourseId"]);
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var centreId = UserCentreId;
            var organisationId = UserOrganisationId;
            var centreCourseIds = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId).Items.Select(e => e.CourseId).ToList();
            if (courseId != 0)
            {
                var data = NidanBusinessService.RetrieveSubjects(organisationId, p => (isSuperAdmin) || p.SubjectCourses.Select(e => e.CourseId).Contains(courseId), orderBy, paging);
                return this.JsonNet(data);
            }
            else
            {
                var data = NidanBusinessService.RetrieveSubjects(organisationId, p => (isSuperAdmin) || p.SubjectCourses.Any(e => centreCourseIds.Contains(e.CourseId)), orderBy, paging);
                return this.JsonNet(data);
            }
        }

        [HttpPost]
        public ActionResult SessionList(Paging paging, List<OrderBy> orderBy)
        {
            var subjectId = Convert.ToInt32(TempData["SubjectId"]);
            TempData["SubjectId"] = subjectId;
            bool isAdmin = User.IsInAnyRoles("Admin");
            return this.JsonNet(NidanBusinessService.RetrieveSessions(UserOrganisationId, p => (isAdmin) && p.SubjectId == subjectId, orderBy, paging));
        }
    }
}