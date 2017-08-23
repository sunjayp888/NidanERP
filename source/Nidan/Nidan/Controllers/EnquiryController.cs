using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Nidan.Controllers
{
    [Authorize]
    public class EnquiryController : BaseController
    {
        public EnquiryController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Enquiry
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Enquiry/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            id = id ?? 0;
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var occupations = NidanBusinessService.RetrieveOccupations(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var followUp = NidanBusinessService.RetrieveFollowUps(organisationId, e => e.MobilizationId == (id.Value == 0 ? -1 : id.Value)).Items.FirstOrDefault();
            var schemes = NidanBusinessService.RetrieveCentreSchemes(organisationId, centreId, e => e.CentreId == centreId);
            var sectors = NidanBusinessService.RetrieveCentreSectors(organisationId, centreId, e => e.CentreId == centreId);
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var enquiryTypes = NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);
            var enquiryFromMobilization = id.Value != 0
                ? NidanBusinessService.CreateEnquiryFromMobilization(organisationId, UserCentreId, id.Value)
                : new Enquiry();


            var viewModel = new EnquiryViewModel
            {
                CreateEnquiryFromMobilizationFollowUpId = followUp?.FollowUpId ?? 0,
                MobilizationId = id.Value,
                Enquiry = enquiryFromMobilization,
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                SelectedCourseIds = new List<int>(),
            };

            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.PreferredMonthForJoiningList = new SelectList(viewModel.PreferredMonthForJoiningType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Enquiry/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnquiryViewModel enquiryViewModel)
        {
            var organisationId = UserOrganisationId;
            enquiryViewModel.Enquiry.StudentCode = "ABC";
            if (ModelState.IsValid)
            {
                if (enquiryViewModel.CreateEnquiryFromMobilizationFollowUpId != 0)
                {
                    //Close Mobilization 
                    var mobilization = NidanBusinessService.RetrieveMobilization(organisationId, enquiryViewModel.MobilizationId);
                    mobilization.Close = "Yes";
                    mobilization.ClosingRemark = "Converted To Enquiry";
                    NidanBusinessService.UpdateMobilization(organisationId, mobilization);
                    var followup = NidanBusinessService.RetrieveFollowUp(organisationId, enquiryViewModel.CreateEnquiryFromMobilizationFollowUpId);
                    followup.Close = "Yes";
                    followup.Remark = "Converted To Enquiry";
                    NidanBusinessService.UpdateFollowUp(organisationId, followup);
                    // NidanBusinessService.DeleteFollowUp(organisationId, enquiryViewModel.CreateEnquiryFromMobilizationFollowUpId);
                }
                enquiryViewModel.Enquiry.OrganisationId = organisationId;
                enquiryViewModel.Enquiry.CentreId = UserCentreId;
                enquiryViewModel.Enquiry.Close = "No";
                enquiryViewModel.Enquiry.EnquiryStatus = "Enquiry";
                enquiryViewModel.Enquiry.EmailId = enquiryViewModel.Enquiry.EmailId.ToLower();
                enquiryViewModel.Enquiry = NidanBusinessService.CreateEnquiry(organisationId, UserPersonnelId, enquiryViewModel.Enquiry, enquiryViewModel.SelectedCourseIds);
                //return RedirectToAction("Index");
                return RedirectToAction("Index");
            }
            enquiryViewModel.EducationalQualifications = new SelectList(NidanBusinessService.RetrieveQualifications(organisationId, e => true).ToList());
            enquiryViewModel.Occupations = new SelectList(NidanBusinessService.RetrieveOccupations(organisationId, e => true).ToList());
            enquiryViewModel.Religions = new SelectList(NidanBusinessService.RetrieveReligions(organisationId, e => true).ToList());
            enquiryViewModel.CasteCategories = new SelectList(NidanBusinessService.RetrieveCasteCategories(organisationId, e => true).ToList());
            //enquiryViewModel.AreaOfInterests = new SelectList(NidanBusinessService.RetrieveAreaOfInterests(organisationId, e => true).ToList());
            enquiryViewModel.HowDidYouKnowAbouts = new SelectList(NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true).ToList());
            enquiryViewModel.Courses = new SelectList(NidanBusinessService.RetrieveCourses(organisationId, e => true).ToList());
            enquiryViewModel.Schemes = new SelectList(NidanBusinessService.RetrieveSchemes(organisationId, e => true).ToList());
            enquiryViewModel.Sectors = new SelectList(NidanBusinessService.RetrieveSectors(organisationId, e => true).ToList());
            enquiryViewModel.BatchTimePrefers = new SelectList(NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true).ToList());
            enquiryViewModel.StudentTypes = new SelectList(NidanBusinessService.RetrieveStudentTypes(organisationId, e => true).ToList());
            enquiryViewModel.EnquiryTypes = new SelectList(NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true).ToList());
            enquiryViewModel.Talukas = new SelectList(NidanBusinessService.RetrieveTalukas(organisationId, e => true).ToList());
            enquiryViewModel.Districts = new SelectList(NidanBusinessService.RetrieveDistricts(organisationId, e => true).ToList());
            enquiryViewModel.States = new SelectList(NidanBusinessService.RetrieveStates(organisationId, e => true).ToList());
            return View(enquiryViewModel);
        }

        // GET: Enquiry/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var educationalQualifications = NidanBusinessService.RetrieveQualifications(organisationId, e => true);
            var occupations = NidanBusinessService.RetrieveOccupations(organisationId, e => true);
            var religions = NidanBusinessService.RetrieveReligions(organisationId, e => true);
            var casteCategories = NidanBusinessService.RetrieveCasteCategories(organisationId, e => true);
            //var areaOfInterests = NidanBusinessService.RetrieveAreaOfInterests(organisationId, e => true);
            var howDidYouKnowAbouts = NidanBusinessService.RetrieveHowDidYouKnowAbouts(organisationId, e => true);
            var enquiry = NidanBusinessService.RetrieveEnquiry(organisationId, id.Value);
            var schemes = NidanBusinessService.RetrieveCentreSchemes(organisationId, centreId, e => e.CentreId == centreId);
            var sectors = NidanBusinessService.RetrieveCentreSectors(organisationId, centreId, e => e.CentreId == centreId);
            var courses = NidanBusinessService.RetrieveCentreCourses(organisationId, centreId, e => e.CentreId == centreId);
            var talukas = NidanBusinessService.RetrieveTalukas(organisationId, e => true);
            var districts = NidanBusinessService.RetrieveDistricts(organisationId, e => true);
            var states = NidanBusinessService.RetrieveStates(organisationId, e => true);
            var batchTimePrefers = NidanBusinessService.RetrieveBatchTimePrefers(organisationId, e => true);
            var enquiryTypes = NidanBusinessService.RetrieveEnquiryTypes(organisationId, e => true);
            var studentTypes = NidanBusinessService.RetrieveStudentTypes(organisationId, e => true);

            if (enquiry == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EnquiryViewModel
            {
                Enquiry = enquiry,
                EducationalQualifications = new SelectList(educationalQualifications, "QualificationId", "Name"),
                Occupations = new SelectList(occupations, "OccupationId", "Name"),
                Religions = new SelectList(religions, "ReligionId", "Name"),
                CasteCategories = new SelectList(casteCategories, "CasteCategoryId", "Caste"),
                Courses = new SelectList(courses, "CourseId", "Name"),
                Schemes = new SelectList(schemes, "SchemeId", "Name"),
                Sectors = new SelectList(sectors, "SectorId", "Name"),
                Talukas = new SelectList(talukas, "TalukaId", "Name"),
                Districts = new SelectList(districts, "DistrictId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                BatchTimePrefers = new SelectList(batchTimePrefers, "BatchTimePreferId", "Name"),
                StudentTypes = new SelectList(studentTypes, "StudentTypeId", "Name"),
                EnquiryTypes = new SelectList(enquiryTypes, "EnquiryTypeId", "Name"),
                HowDidYouKnowAbouts = new SelectList(howDidYouKnowAbouts, "HowDidYouKnowAboutId", "Name"),
                SelectedCourseIds = enquiry.EnquiryCourses.Select(e => e.CourseId).ToList()
            };
            viewModel.ConversionProspectList = new SelectList(viewModel.ConversionProspectType, "Id", "Name");
            viewModel.TitleList = new SelectList(viewModel.TitleType, "Value", "Name");
            viewModel.PreferredMonthForJoiningList = new SelectList(viewModel.PreferredMonthForJoiningType, "Id", "Name");
            return View(viewModel);
        }

        // POST: Enquiry/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnquiryViewModel enquiryViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                enquiryViewModel.Enquiry.OrganisationId = organisationId;
                enquiryViewModel.Enquiry.CentreId = UserCentreId;
                enquiryViewModel.Enquiry.Close = "No";
                enquiryViewModel.Enquiry.EmailId = enquiryViewModel.Enquiry.EmailId.ToLower();
                enquiryViewModel.Enquiry = NidanBusinessService.UpdateEnquiry(organisationId, enquiryViewModel.Enquiry, enquiryViewModel.SelectedCourseIds);
            }
            return RedirectToAction("Index");
        }

        // GET: Enquiry/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var enquiryDataGrid = NidanBusinessService.RetrieveEnquiryDataGrid(organisationId, e => e.EnquiryId == id).Items.FirstOrDefault();
            var courseIds = NidanBusinessService.RetrieveEnquiryCourses(organisationId, UserCentreId, id.Value).Select(e => e.CourseId).ToList();
            var courseName = NidanBusinessService.RetrieveCourses(organisationId, e => courseIds.Contains(e.CourseId)).Select(e => e.Name).ToList();
            if (enquiryDataGrid == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EnquiryViewModel
            {
                EnquiryDataGrid = enquiryDataGrid,
                CourseNames = courseName
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.IsRegistrationDone == false && p.IsAdmissionDone == false && p.Close == "No", orderBy, paging));
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveEnquiryBySearchKeyword(UserOrganisationId, searchKeyword, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.Close == "No", orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveEnquiries(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.EnquiryDate >= fromDate && e.EnquiryDate <= toDate && e.IsAdmissionDone == false && e.IsRegistrationDone == false, orderBy, paging));
        }

        [HttpPost]
        public ActionResult GetCourse(int sectorId)
        {
            var data = NidanBusinessService.RetrieveCentreCourses(UserOrganisationId, UserCentreId, e => e.Course.SectorId == sectorId && e.CentreId == UserCentreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetSector(int schemeId)
        {
            var data = NidanBusinessService.RetrieveCentreSectors(UserOrganisationId, UserCentreId, e => e.Sector.SchemeId == schemeId && e.CentreId == UserCentreId).ToList();
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult GetTaluka(int districtId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveTalukas(UserOrganisationId, e => e.District.DistrictId == districtId).ToList());
        }

        [HttpPost]
        public ActionResult GetDistrict(int stateId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveDistricts(UserOrganisationId, e => e.State.StateId == stateId).ToList());
        }
    }
}