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
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Dto;

namespace Nidan.Controllers
{
    [Authorize]
    public class MobilizationController : BaseController
    {
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public MobilizationController(INidanBusinessService hrBusinessService) : base(hrBusinessService)
        {
        }
        // GET: Mobilization

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId);
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
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            return View(viewModel);
        }

        // POST: Mobilization/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MobilizationViewModel mobilizationViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = UserCentreId;
                mobilizationViewModel.Mobilization.PersonnelId = UserPersonnelId;
                mobilizationViewModel.Mobilization.EventId = mobilizationViewModel.EventId;
                mobilizationViewModel.Mobilization.Close = "No";
                mobilizationViewModel.Mobilization = NidanBusinessService.CreateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            mobilizationViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            mobilizationViewModel.MobilizationTypes = new SelectList(NidanBusinessService.RetrieveMobilizationTypes(organisationId, e => true).ToList());
            mobilizationViewModel.Events = new SelectList(NidanBusinessService.RetrieveEvents(organisationId, e => true).Items.ToList());
            mobilizationViewModel.Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            return View(mobilizationViewModel);
        }

        // GET: Mobilization/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId);
            var mobilization = NidanBusinessService.RetrieveMobilization(UserOrganisationId, id.Value);
            if (mobilization == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilization,
                Courses = new SelectList(courses, "CourseId", "Name"),
                MobilizationTypes = new SelectList(NidanBusinessService.RetrieveMobilizationTypes(UserOrganisationId, e => true).ToList(), "MobilizationTypeId", "Name"),
                Events = new SelectList(NidanBusinessService.RetrieveEvents(UserOrganisationId, e => true).Items.ToList(), "EventId", "Name"),
                EventId = mobilization.EventId,
                Qualifications = new SelectList(NidanBusinessService.RetrieveQualifications(UserOrganisationId, e => true).ToList(), "QualificationId", "Name")
            };
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            return View(viewModel);
        }

        // POST: Mobilization/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MobilizationViewModel mobilizationViewModel)
        {
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = UserCentreId;
                mobilizationViewModel.Mobilization.PersonnelId = UserPersonnelId;
                mobilizationViewModel.Mobilization.EventId = mobilizationViewModel.EventId;
                mobilizationViewModel.Mobilization.Close = "No";
                mobilizationViewModel.Mobilization = NidanBusinessService.UpdateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilizationViewModel.Mobilization
            };
            return View(viewModel);
        }

        // GET: Mobilization/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var mobilizationDataGrid = NidanBusinessService.RetrieveMobilizationDataGrid(organisationId, e => e.MobilizationId == id).Items.FirstOrDefault();
            if (mobilizationDataGrid == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MobilizationViewModel
            {
                MobilizationDataGrid = mobilizationDataGrid
            };
            return View(viewModel);
        }

        public ActionResult Upload()
        {
            var viewModel = new MobilizationViewModel
            {
                Files = new List<HttpPostedFileBase>(),
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
                    NidanBusinessService.UploadMobilization(UserOrganisationId, UserCentreId, mobilizationViewModel.EventId, UserPersonnelId, mobilizationViewModel.GeneratedDate, mobilizations.ToList());
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
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveMobilizationDataGrid(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Close != "Yes", orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveMobilizationDataGrid(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Close == "No", orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveMobilizationDataGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.CreatedDate >= fromDate && e.CreatedDate <= toDate && e.Close == "No", orderBy, paging);
            return this.JsonNet(data);
        }
    }
}