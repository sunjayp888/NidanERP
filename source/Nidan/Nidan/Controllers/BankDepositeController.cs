using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Enum;
using Nidan.Business.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class BankDepositeController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        private readonly IDocumentService _documentService;
        private readonly DateTime _todayUTC = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public BankDepositeController(INidanBusinessService nidanBusinessService, IDocumentService documentService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
            _documentService = documentService;
        }

        // GET: BankDeposite
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult PaymentMode()
        {
            return this.JsonNet(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, c => true));
        }

        //GET: BankDeposite/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var projects = NidanBusinessService.RetrieveProjects(organisationId, centreId, e => e.CentreId == centreId);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var viewModel = new BankDepositeViewModel
            {
                BankDeposite = new BankDeposite(),
                Projects = new SelectList(projects, "ProjectId", "Name"),
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
        };
            return View(viewModel);
        }

        // POST: BankDeposite/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankDepositeViewModel bankDepositeViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                bankDepositeViewModel.BankDeposite.OrganisationId = organisationId;
                bankDepositeViewModel.BankDeposite.CentreId = centreId;
                bankDepositeViewModel.BankDeposite.CreatedBy = personnelId;
                bankDepositeViewModel.BankDeposite.IsCleared = false;
                bankDepositeViewModel.BankDeposite.IsBounced = false;
                bankDepositeViewModel.BankDeposite = NidanBusinessService.CreateBankDeposite(organisationId, bankDepositeViewModel.BankDeposite);
                return RedirectToAction("Index");
            }
            bankDepositeViewModel.Projects=new SelectList(NidanBusinessService.RetrieveProjects(organisationId,e=>e.CentreId==centreId).Items.ToList());
            bankDepositeViewModel.PaymentModes=new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId,e=>true));
            return View(bankDepositeViewModel);
        }

        // GET: BankDeposite/Edit/{id}
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projects = NidanBusinessService.RetrieveProjects(organisationId, centreId, e => e.CentreId == centreId);
            var paymentModes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);
            var bankDeposite = NidanBusinessService.RetrieveBankDeposite(UserOrganisationId, id.Value,e=>true);
            if (bankDeposite == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BankDepositeViewModel()
            {
                BankDeposite = bankDeposite,
                Projects = new SelectList(projects, "ProjectId", "Name"),
                PaymentModes = new SelectList(paymentModes, "PaymentModeId", "Name"),
            };
            return View(viewModel);
        }

        // POST: BankDeposite/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankDepositeViewModel bankDepositeViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                bankDepositeViewModel.BankDeposite.OrganisationId = organisationId;
                bankDepositeViewModel.BankDeposite.CentreId = centreId;
                bankDepositeViewModel.BankDeposite.CreatedBy = personnelId;
                bankDepositeViewModel.BankDeposite.IsCleared = false;
                bankDepositeViewModel.BankDeposite.IsBounced = false;
                bankDepositeViewModel.BankDeposite = NidanBusinessService.UpdateBankDeposite(UserOrganisationId, bankDepositeViewModel.BankDeposite);
                return RedirectToAction("Index");
            }
            var viewModel = new BankDepositeViewModel
            {
                BankDeposite = bankDepositeViewModel.BankDeposite
            };
            return View(viewModel);
        }

        // GET: BankDeposite/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var bankDepositeData = NidanBusinessService.RetrieveBankDeposite(organisationId, id.Value, e => true);
            if (bankDepositeData == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BankDepositeViewModel()
            {
                BankDeposite = bankDepositeData
            };
            return View(viewModel);
        }

        //BankDeposite/list
        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBankDeposites(UserOrganisationId,p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveBankDeposites(UserOrganisationId, e => (isSuperAdmin || e.CentreId == UserCentreId) && e.DepositedDate >= fromDate && e.DepositedDate <= toDate, orderBy, paging);
            return this.JsonNet(data);
        }

        //Update IsCleared
        public ActionResult UpdateIsCleared(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var bankDepositeData = NidanBusinessService.RetrieveBankDeposite(organisationId, id.Value, e => true);
            if (bankDepositeData == null)
            {
                return HttpNotFound();
            }
            bankDepositeData.IsCleared = true;
            NidanBusinessService.UpdateBankDeposite(organisationId, bankDepositeData);
            return RedirectToAction("Index");
        }

        //Update UpdateIsBounced
        public ActionResult UpdateIsBounced(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var bankDepositeData = NidanBusinessService.RetrieveBankDeposite(organisationId, id.Value, e => true);
            if (bankDepositeData == null)
            {
                return HttpNotFound();
            }
            bankDepositeData.IsBounced = true;
            NidanBusinessService.UpdateBankDeposite(organisationId, bankDepositeData);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveBankDepositeDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var bankDepositeData = NidanBusinessService.RetrieveBankDeposites(organisationId,e=>e.BankDepositeId.ToString() == documentViewModel.StudentCode).Items.FirstOrDefault();
            _documentService.Create(organisationId, centreId,
                documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                bankDepositeData?.CentreName, "Bank Deposite Document", documentViewModel.Attachment.FileName,
                documentViewModel.Attachment.InputStream.ToBytes());
        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }

    }
}