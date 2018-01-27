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
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Counselling/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            id = id ?? 0;
            var sectors = NidanBusinessService.RetrieveSectors(organisationId, e => true);
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, id.Value);
            var interestedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, UserCentreId, e => e.CentreId == UserCentreId);
            var enquiryCourses = NidanBusinessService.RetrieveCentreCourses(organisationId, UserCentreId, e => e.CentreId == UserCentreId).Where(e => interestedCourseIds.Contains(e.CourseId));
            var occupations = NidanBusinessService.RetrieveOccupations(organisationId, e => true);
            var schemes = NidanBusinessService.RetrieveCentreSchemes(organisationId, centreId, e => e.CentreId == centreId);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var viewModel = new CounsellingViewModel
            {
                Enquiry = enquiry,
                EnquiryId = id.Value,
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                EnquiryCourses = new SelectList(enquiryCourses, "CourseId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Counselling = new Counselling()
                {
                    Title = enquiry.Title,
                    FirstName = enquiry.FirstName,
                    MiddleName = enquiry.MiddleName,
                    LastName = enquiry.LastName,
                    EnquiryId = enquiry.EnquiryId,
                    Enquiry = enquiry
                }
            };
            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.PreferredMonthForJoiningList = new SelectList(viewModel.PreferredMonthForJoiningType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Counselling/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CounsellingViewModel counsellingViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                counsellingViewModel.Counselling.OrganisationId = organisationId;
                counsellingViewModel.Counselling.PersonnelId = personnelId;
                counsellingViewModel.Counselling.CreatedBy = UserPersonnelId;
                counsellingViewModel.Counselling.CentreId = UserCentreId;
                counsellingViewModel.Counselling.CreatedBy = personnelId;
                counsellingViewModel.Counselling = NidanBusinessService.CreateCounselling(organisationId, counsellingViewModel.Counselling);
                return RedirectToAction("Index");
            }
            counsellingViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name");
            counsellingViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name");
            counsellingViewModel.Occupations = new SelectList(NidanBusinessService.RetrieveOccupations(organisationId, e => true).ToList());
            counsellingViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            counsellingViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList());
            counsellingViewModel.EducationalQualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            return View(counsellingViewModel);
        }

        // GET: Counselling/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["CounsellingId"] = id;
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var counselling = NidanBusinessService.RetrieveCounselling(organisationId, id.Value);
            if (counselling == null)
            {
                return HttpNotFound();
            }
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, counselling.EnquiryId);
            var interestedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList();
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId);
            var viewModel = new CounsellingViewModel
            {
                Counselling = counselling,
                Courses = new SelectList(courses, "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name")
            };
            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            return View(viewModel);
        }

        // POST: Counselling/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CounsellingViewModel counsellingViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                counsellingViewModel.Counselling.OrganisationId = organisationId;
                counsellingViewModel.Counselling.PersonnelId = personnelId;
                counsellingViewModel.Counselling.CentreId = UserCentreId;
                counsellingViewModel.Counselling.CreatedBy = personnelId;
                counsellingViewModel.Counselling.FollowUpDate = DateTime.UtcNow.AddDays(2);
                counsellingViewModel.Counselling = NidanBusinessService.UpdateCounselling(organisationId, counsellingViewModel.Counselling);
                return RedirectToAction("Index");
            }
            var viewModel = new CounsellingViewModel
            {
                Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList(), "CourseId", "Name"),
                Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList(), "SectorId", "Name"),
                Counselling = counsellingViewModel.Counselling
            };
            return View(viewModel);
        }

        // GET: Counselling/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            //var mobilizationDataGrid = NidanBusinessService.RetrieveCounsellingGrid(organisationId, e => e.CounsellingId == id).Items.FirstOrDefault();
            var counselling = NidanBusinessService.RetrieveCounselling(organisationId, id.Value);
            if (counselling == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CounsellingViewModel
            {
                Counselling = counselling
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
            var organisationId = UserOrganisationId;
            counsellingViewModel.DocumentTypes = new SelectList(NidanBusinessService.RetrieveDocumentTypes(organisationId),
                "DocumentTypeId", "Name");

            if (counsellingViewModel.Files != null)
            {
                if (counsellingViewModel.Files != null && counsellingViewModel.Files[0].ContentLength > 0)
                {
                    var enquiryData = _nidanBusinessService.RetrieveEnquiry(organisationId, counsellingViewModel.EnquiryId);

                    if (counsellingViewModel.Files[0].FileName.EndsWith(".pdf"))
                    {
                        _documentService.Create(organisationId, UserCentreId,
                            counsellingViewModel.Document.DocumentTypeId, enquiryData.StudentCode,
                            enquiryData.FirstName, "Counselling Document", counsellingViewModel.Files[0].FileName,
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
            var data = NidanBusinessService.RetrieveCounsellingDataGrid(UserOrganisationId,p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsRegistrationDone == "NO" && p.Close != "Yes",orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveCounsellingBySearchKeyword(UserOrganisationId, searchKeyword,
                p => (isSuperAdmin || p.CentreId == centreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveCounsellingDataGrid(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.CreatedDate >= fromDate && e.CreatedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCourses(UserOrganisationId, e => e.Sector.SectorId == sectorId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveCounsellingDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var studentData = NidanBusinessService.RetrieveEnquiries(organisationId, e => e.CentreId == centreId && e.StudentCode == documentViewModel.StudentCode).ToList().FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                studentData?.FirstName, "Counselling Document", documentViewModel.Attachment.FileName,
                documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }
    }
}