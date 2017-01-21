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
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Nidan.Controllers
{
    [Authorize]
    public class EnquiryController : BaseController
    {
        private static string EnquiryName { get; set; }
        private ApplicationRoleManager _roleManager;
      
        public EnquiryController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Enquiry

        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Enquiry/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var viewModel = new EnquiryViewModel
            {
                Enquiry = new Enquiry
                {
                    OrganisationId = UserOrganisationId,
                    CandidateName = "Shraddha",
                    ContactNo = 9812398123,
                    EmailId = string.Format("{0}@hr.com", Guid.NewGuid()),
                    Age = 24,
                    Qualification = "BSCIT",
                    Address = "Koperkhairane",
                    GuardianName = "",
                    GuardianContactNo = 123,
                    Occupation = "",
                    Religion = "",
                    CategoryCode = "",
                    Gender = "Female",
                    EducationalQualification = "BSCIT",
                    YearOFPassOut = "",
                    Marks = "",
                    AreaOfInterest = "",
                    HowDidYouKnowAboutUs = "",
                    PreTrainingStatus = "",
                    EmploymentStatus = "",
                    Promotional = "",
                    EnquiryDate = DateTime.Today,
                    Place="Thane",
                    CounselledBy="Deepali",
                    CourseOffered=".net",
                    PreferTiming= DateTime.Now,
                    Remarks="",
                    FollowUpDate=DateTime.Today,
                    CentreId=1,
                },
                
            };
            return View(viewModel);
        }

        // POST: Enquiry/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnquiryViewModel enquiryViewModel)
        {
           
            if (ModelState.IsValid)
            {
                enquiryViewModel.Enquiry.OrganisationId = UserOrganisationId;
                enquiryViewModel.Enquiry.CentreId = 1;
                enquiryViewModel.Enquiry = NidanBusinessService.CreateEnquiry(UserOrganisationId, enquiryViewModel.Enquiry);
                return RedirectToAction("Index");
            }
            return View(enquiryViewModel);
        }

        // GET: Enquiry/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enquiry = NidanBusinessService.RetrieveEnquiry(UserOrganisationId, id.Value);
            if (enquiry == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EnquiryViewModel
            {
                Enquiry = enquiry
            };
            return View(viewModel);
        }

        // POST: Enquiry/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnquiryViewModel enquiryViewModel)
        {
            if (ModelState.IsValid)
            {
                enquiryViewModel.Enquiry.OrganisationId = UserOrganisationId;
                enquiryViewModel.Enquiry.CentreId = 1;
                enquiryViewModel.Enquiry = NidanBusinessService.UpdateEnquiry(UserOrganisationId, enquiryViewModel.Enquiry);
            }
            var viewModel = new EnquiryViewModel
            {
                Enquiry = enquiryViewModel.Enquiry
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId,orderBy,paging));
        }

        //[HttpPost]
        //public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        //{
        //    return this.JsonNet(NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, orderBy, paging));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}