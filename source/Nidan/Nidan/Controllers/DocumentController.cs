using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using System.IO;
using Nidan.Document.Interfaces;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;
        public DocumentController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _documentService = documentService;
        }
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentDocument(string studentCode, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveDocuments(UserOrganisationId, e => e.CentreId == UserCentreId && e.StudentCode == studentCode, orderBy, paging));
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

        [HttpPost]
        public ActionResult DocumentTypes()
        {
            return this.JsonNet(NidanBusinessService.RetrieveDocumentTypes(UserOrganisationId));
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var studentData = NidanBusinessService.RetrieveEnquiries(organisationId,e => e.CentreId == centreId && e.StudentCode == documentViewModel.StudentCode).ToList().FirstOrDefault();

            var trainerData =NidanBusinessService.RetrieveTrainers(organisationId,e => e.CentreId == centreId && e.TrainerId.ToString() == documentViewModel.StudentCode).ToList().FirstOrDefault();

            var expenseData = NidanBusinessService.RetrieveExpenses(organisationId,centreId,e => e.CentreId == centreId && e.CashMemoNumbers == documentViewModel.StudentCode).Items.ToList().FirstOrDefault();

            if (documentViewModel.DocumentTypeId == 2)
            {
                _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           studentData?.FirstName, "Psychometric Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
            }

            if (documentViewModel.DocumentTypeId == 6)
            {
                _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           studentData?.FirstName, "Counselling Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
            }

            if (documentViewModel.DocumentTypeId == 9)
            {
                _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           trainerData?.FirstName, "Trainer Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
            }

            if (documentViewModel.DocumentTypeId == 12)
            {
                _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           expenseData?.CashMemoNumbers, "Expense Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
            }

            if (documentViewModel.DocumentTypeId == 15)
            {
                _documentService.Create(organisationId, centreId,
                           documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                           studentData?.FirstName, "Admission Document", documentViewModel.Attachment.FileName,
                           documentViewModel.Attachment.InputStream.ToBytes());
            }
        }
    }
}