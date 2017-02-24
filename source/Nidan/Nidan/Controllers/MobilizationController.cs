using Excel;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Nidan.Business.Dto;

namespace Nidan.Controllers
{
    [Authorize]
    public class MobilizationController : BaseController
    {

        public MobilizationController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }
        // GET: Mobilization

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
            var mobilizationTypes = NidanBusinessService.RetrieveMobilizationTypes(organisationId, e => true);
            var qualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var events = NidanBusinessService.RetrieveEvents(organisationId, e => true).Items.ToList();
            var viewModel = new MobilizationViewModel
            {
                Mobilization = new Mobilization(),
                Courses = new SelectList(courses, "CourseId", "Name"),
                MobilizationTypes = new SelectList(mobilizationTypes, "MobilizationTypeId", "Name"),
                Events = new SelectList(events, "EventId", "Name"),
                Qualifications = new SelectList(qualifications, "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Mobilization/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MobilizationViewModel mobilizationViewModel)
        {
            var organisationId = UserOrganisationId;
            mobilizationViewModel.GeneratedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.FollowUpDate = DateTime.Now.AddDays(2);
                mobilizationViewModel.Mobilization.PersonnelId = UserPersonnelId;
                mobilizationViewModel.Mobilization.EventId = mobilizationViewModel.EventId;
                mobilizationViewModel.Mobilization = NidanBusinessService.CreateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            mobilizationViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            mobilizationViewModel.Events = new SelectList(NidanBusinessService.RetrieveEvents(organisationId, e => true).Items.ToList());
            mobilizationViewModel.Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            return View(mobilizationViewModel);
        }

        // GET: Mobilization/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mobilization = NidanBusinessService.RetrieveMobilization(UserOrganisationId, id.Value);
            if (mobilization == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilization,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                MobilizationTypes = new SelectList(NidanBusinessService.RetrieveMobilizationTypes(UserOrganisationId, e => true).ToList(), "MobilizationTypeId", "Name"),
                Events = new SelectList(NidanBusinessService.RetrieveEvents(UserOrganisationId, e => true).Items.ToList(), "EventId", "Name"),
                EventId = mobilization.EventId,
                Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(UserOrganisationId, e => true).ToList(), "QualificationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Mobilizations/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MobilizationViewModel mobilizationViewModel)
        {
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.PersonnelId = UserPersonnelId;
                mobilizationViewModel.Mobilization.EventId = mobilizationViewModel.EventId;
                mobilizationViewModel.Mobilization = NidanBusinessService.UpdateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilizationViewModel.Mobilization
            };
            return View(viewModel);
        }

        public ActionResult Upload()
        {
            var viewModel = new MobilizationViewModel
            {
                Events = new SelectList(NidanBusinessService.RetrieveEvents(UserOrganisationId, e => true).Items.ToList(), "EventId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(MobilizationViewModel mobilizationViewModel)
        {
            mobilizationViewModel.Events = new SelectList(NidanBusinessService.RetrieveEvents(UserOrganisationId, e => true).Items.ToList(), "EventId", "Name");

            if (ModelState.IsValid)
            {
                if (mobilizationViewModel.Files != null && mobilizationViewModel.Files[0].ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    var stream = mobilizationViewModel.Files[0].InputStream;
                    // We return the interface, so that
                    IExcelDataReader reader = null;

                    if (mobilizationViewModel.Files[0].FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (mobilizationViewModel.Files[0].FileName.EndsWith(".xlsx"))
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
                    var mobilizations = result.Tables[0].ToList<Mobilization>();
                    NidanBusinessService.UploadMobilization(UserOrganisationId, mobilizationViewModel.EventId, UserPersonnelId, mobilizationViewModel.GeneratedDate, mobilizations.ToList());
                    return RedirectToAction("Index");
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
            return this.JsonNet(NidanBusinessService.RetrieveMobilizations(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveMobilizationBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }

    }
}