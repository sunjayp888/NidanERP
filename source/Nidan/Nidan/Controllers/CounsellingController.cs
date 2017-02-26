﻿using System;
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
    public class CounsellingController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;

        public CounsellingController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: Counselling
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        //// GET: Counselling/Create
        //[Authorize(Roles = "Admin")]
        //public ActionResult Create()
        //{
        //    var organisationId = UserOrganisationId;
        //    var courses = NidanBusinessService.RetrieveCourses(organisationId, e => true);
        //    var enquiries = NidanBusinessService.RetrieveEnquiries(organisationId, e => true);
        //    var viewModel = new CounsellingViewModel
        //    {
        //        Counselling = new Counselling(),
        //        Courses = new SelectList(courses, "CourseId", "Name"),
        //        Enquiries = new SelectList(enquiries,"EnquiryId", "CandidateName")
        //    };
        //    return View(viewModel);
        //}

        //// POST: Counselling/Create
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CounsellingViewModel counsellingViewModel)
        //{
        //    var organisationId = UserOrganisationId;
        //    if (ModelState.IsValid)
        //    {
        //        counsellingViewModel.Counselling.OrganisationId = UserOrganisationId;
        //        counsellingViewModel.Counselling.CentreId = 1;
        //        counsellingViewModel.Counselling = NidanBusinessService.CreateCounselling(UserOrganisationId, counsellingViewModel.Counselling);
        //        return RedirectToAction("Index");
        //    }
        //    counsellingViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
        //    counsellingViewModel.Enquiries = new SelectList(NidanBusinessService.RetrieveEnquiries(organisationId, e => true).ToList());
        //    return View(counsellingViewModel);
        //}

        // GET: Counselling/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var counselling = NidanBusinessService.RetrieveCounselling(UserOrganisationId, id.Value);
            if (counselling == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CounsellingViewModel
            {
                Counselling = counselling,
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name")
            };
            return View(viewModel);
        }

        // POST: Counselling/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CounsellingViewModel counsellingViewModel)
        {
            if (ModelState.IsValid)
            {
                counsellingViewModel.Counselling.OrganisationId = UserOrganisationId;
                counsellingViewModel.Counselling.PersonnelId = UserPersonnelId;
                counsellingViewModel.Counselling.CentreId = UserCentreId;
                counsellingViewModel.Counselling = NidanBusinessService.UpdateCounselling(UserOrganisationId, counsellingViewModel.Counselling);
                return RedirectToAction("Index");
            }
            var viewModel = new EnquiryViewModel
            {
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name"),
                Counselling = counsellingViewModel.Counselling
            };
            return View(viewModel);
        }

        public ActionResult Upload(CounsellingViewModel counsellingViewModel)
        {

            if (ModelState.IsValid)
            {
                if (counsellingViewModel.Files != null && counsellingViewModel.Files[0].ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    var stream = counsellingViewModel.Files[0].InputStream;
                    // We return the interface, so that

                    if (counsellingViewModel.Files[0].FileName.EndsWith(".pdf"))
                    {
                       //Create and Upload document

                    }
                    else
                    {
                        ModelState.AddModelError("", "This file format is not supported");
                        return View();
                    }
                //    NidanBusinessService.UploadMobilization(UserOrganisationId, mobilizationViewModel.EventId, UserPersonnelId, mobilizationViewModel.GeneratedDate, mobilizations.ToList());
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
            return this.JsonNet(NidanBusinessService.RetrieveCounsellings(UserOrganisationId, p => true, orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveCounsellingBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveCounsellings(UserOrganisationId, e => e.FollowUpDate >= fromDate && e.FollowUpDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}