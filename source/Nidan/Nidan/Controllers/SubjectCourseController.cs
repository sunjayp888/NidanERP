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
    public class SubjectCourseController : BaseController
    {
        // GET: SubjectCourse
        public ActionResult Index()
        {
            return View();
        }

        public SubjectCourseController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        [HttpPost]
        public ActionResult Create(SubjectCourse subjectCourse)
        {
            var result = NidanBusinessService.CreateSubjectCourse(UserOrganisationId, subjectCourse);
            return this.JsonNet(result);
        }

        [HttpPost]
        public ActionResult List(int subjectId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveSubjectCourses(UserOrganisationId, s => s.SubjectId == subjectId));
        }

        [HttpPost]
        public ActionResult Delete(int subjectId, int courseId)
        {
            NidanBusinessService.DeleteSubjectCourse(UserOrganisationId, subjectId, courseId);
            return this.JsonNet("");
        }
    }
}