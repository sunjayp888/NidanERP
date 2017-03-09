using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Data;
using Nidan.Document;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CounsellingController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly IDocumentService _documentService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);

        public CounsellingController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
            _documentService = documentService;
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
            TempData["CounsellingId"] = id;
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
            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
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
                //counsellingViewModel.Counselling.Close =;
                counsellingViewModel.Counselling.FollowUpDate = DateTime.UtcNow.AddDays(2);
                counsellingViewModel.Counselling = NidanBusinessService.UpdateCounselling(UserOrganisationId, counsellingViewModel.Counselling);
                return RedirectToAction("Index");
            }
            var viewModel = new CounsellingViewModel
            {
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(UserOrganisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(UserOrganisationId, e => true).ToList(), "SectorId", "Name"),
                Counselling = counsellingViewModel.Counselling
            };
            return View(viewModel);
        }

        public ActionResult Upload(int id)
        {

            var viewModel = new CounsellingViewModel
            {
                CounsellingId = Convert.ToInt32(TempData["CounsellingId"]),
                EnquiryId = id,
                Files = new List<HttpPostedFileBase>(),
                DocumentTypes = new SelectList(NidanBusinessService.RetrieveDocumentTypes(UserOrganisationId), "DocumentTypeId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(CounsellingViewModel counsellingViewModel)
        {
            counsellingViewModel.DocumentTypes = new SelectList(NidanBusinessService.RetrieveDocumentTypes(UserOrganisationId),
                "DocumentTypeId", "Name");

            if (counsellingViewModel.Files != null)
            {
                if (counsellingViewModel.Files != null && counsellingViewModel.Files[0].ContentLength > 0)
                {
                    var enquiryData = _nidanBusinessService.RetrieveEnquiry(UserOrganisationId, counsellingViewModel.EnquiryId);

                    if (counsellingViewModel.Files[0].FileName.EndsWith(".pdf"))
                    {
                        _documentService.Create(UserOrganisationId, UserCentreId,
                            counsellingViewModel.Document.DocumentTypeId, enquiryData.StudentCode,
                            enquiryData.CandidateName, "Counselling Document", counsellingViewModel.Files[0].FileName,
                            counsellingViewModel.Files[0].InputStream.ToBytes());
                    }
                    else
                    {
                        ModelState.AddModelError("FileFormat", "This file format is not supported");
                        return View(counsellingViewModel);
                    }
                    return RedirectToAction("Edit", new { id = enquiryData.Counsellings.FirstOrDefault().CounsellingId });
                }
                ModelState.AddModelError("", "Please Upload Your file");
            }
            return View(counsellingViewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCounsellings(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.FollowUpDate == _today && p.Close != "yes", orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveCounsellingBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            var data = NidanBusinessService.RetrieveCounsellings(UserOrganisationId, e => e.FollowUpDate >= fromDate && e.FollowUpDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, e => e.Sector.SectorId == sectorId).ToList();
            return this.JsonNet(data);
        }
    }
}