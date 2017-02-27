using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using System.IO;

namespace Nidan.Controllers
{
    public class DocumentController : BaseController
    {
        public DocumentController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentDocument(string studentCode, string category, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveDocuments(UserOrganisationId, e => e.CentreId == UserCentreId && e.StudentCode == studentCode && e.DocumentType.Name.ToLower() == category.ToLower(), orderBy, paging));
        }
        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(null);
        }

        public ActionResult Download(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }
    }
}