using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Extensions;

namespace Nidan.Controllers
{
    public class EnquiryCourseController : BaseController
    {
        // GET: EnquiryCourse
        public ActionResult Index()
        {
            return View();
        }

        public EnquiryCourseController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult Create(EnquiryCourse enquiryCourse)
        {
            var result = NidanBusinessService.CreateEnquiryCourse(UserOrganisationId, enquiryCourse);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int enquiryId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveEnquiryCourses(UserOrganisationId, UserCentreId, enquiryId));
        }

        [HttpPost]
        public ActionResult Delete(int enquiryId, int courseId)
        {
            NidanBusinessService.DeleteEnquiryCourse(UserOrganisationId, enquiryId, courseId);
            return this.JsonNet("");
        }
    }
}