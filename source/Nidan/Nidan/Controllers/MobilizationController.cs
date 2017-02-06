using Excel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Attributes;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using Nidan.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;

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
                Mobilization = new Mobilization
                {
                    OrganisationId = UserOrganisationId,
                    CentreId = 1,
                    Name = "Shraddha",
                    Mobile = 9870754355,
                    //Qualification = "BSCIT",
                    GeneratedDate = DateTime.Now,
                    FollowUpDate = DateTime.Now.AddDays(2),
                    Remark = "",
                },
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
            if (ModelState.IsValid)
            {
                mobilizationViewModel.Mobilization.OrganisationId = UserOrganisationId;
                mobilizationViewModel.Mobilization.CentreId = 1;
                mobilizationViewModel.Mobilization.EventId = 3;
                mobilizationViewModel.Mobilization.CreatedDate = DateTime.Now;
                mobilizationViewModel.Mobilization.MobilizerStatus = "Open";
                mobilizationViewModel.Mobilization = NidanBusinessService.CreateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            mobilizationViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            //mobilizationViewModel.Events = new SelectList(NidanBusinessService.RetrieveEvents(organisationId, e => true).ToList());
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
                mobilizationViewModel.Mobilization.EventId = 3;
                mobilizationViewModel.Mobilization.CreatedDate = DateTime.Now;
                mobilizationViewModel.Mobilization.MobilizerStatus = "Open";
                mobilizationViewModel.Mobilization = NidanBusinessService.UpdateMobilization(UserOrganisationId, mobilizationViewModel.Mobilization);
                return RedirectToAction("Index");
            }
            var viewModel = new MobilizationViewModel
            {
                Mobilization = mobilizationViewModel.Mobilization
            };
            return View(viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload(MobilizationViewModel mobilizationViewModel)
        {
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
                    NidanBusinessService.UploadMobilization(UserOrganisationId, mobilizationViewModel.EventId, mobilizationViewModel.GeneratedDate, mobilizations.ToList());
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please Upload Your file");
                }
            }
            return View();
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
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