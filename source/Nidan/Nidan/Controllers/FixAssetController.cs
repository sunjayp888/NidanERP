using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class FixAssetController : BaseController
    {
        private readonly IDocumentService _documentService;
        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
        public FixAssetController(INidanBusinessService hrBusinessService, IDocumentService documentService) : base(hrBusinessService)
        {
            _documentService = documentService;
        }

        // GET: FixAsset
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult Centre()
        {
            return this.JsonNet(NidanBusinessService.RetrieveCentres(UserOrganisationId, e => true));
        }

        //Get :FixAsset/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var assetClass = NidanBusinessService.RetrieveAssetClasses(organisationId, e=>true);
            var items = NidanBusinessService.RetrieveItems(organisationId, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = new FixAsset(),
                AssetClasses = new SelectList(assetClass, "AssetClassId", "Name"),
                Itemes = new SelectList(items, "ItemId", "Name"),
                Centres = new SelectList(centres, "CentreId","Name")
            };
            return View(viewModel);
        }

        // POST: FixAsset/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FixAssetViewModel fixAssetViewModel)
        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            if (ModelState.IsValid)
            {
                fixAssetViewModel.FixAsset.OrganisationId = organisationId;
                fixAssetViewModel.FixAsset.CreatedBy = personnelId;
                fixAssetViewModel.FixAsset = NidanBusinessService.CreateFixAsset(UserOrganisationId, fixAssetViewModel.FixAsset);
                return RedirectToAction("Index");
            }
            fixAssetViewModel.AssetClasses = new SelectList(NidanBusinessService.RetrieveAssetClasses(organisationId, e => true).ToList());
            fixAssetViewModel.Itemes = new SelectList(NidanBusinessService.RetrieveItems(organisationId, e => true).ToList());
            fixAssetViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            return View(fixAssetViewModel);
        }

        // GET: FixAsset/View/{id}
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var fixAsset = NidanBusinessService.RetrieveFixAsset(organisationId, id.Value, e => true);
            if (fixAsset == null)
            {
                return HttpNotFound();
            }
            var viewModel = new FixAssetViewModel()
            {
                FixAsset = fixAsset
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, p => (isSuperAdmin), orderBy, paging);
            return this.JsonNet(data);
        }

        //FixAssetByCentreId
        [HttpPost]
        public ActionResult FixAssetByCentreId(int centreId,Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            if (centreId == 6)
            {
                var alldata = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, p => (isSuperAdmin), orderBy, paging);
                return this.JsonNet(alldata);
            }
            var data = NidanBusinessService.RetrieveFixAssets(UserOrganisationId, p => (isSuperAdmin&&p.CentreId==centreId), orderBy, paging);
            return this.JsonNet(data);
        }


        [HttpPost]
        public ActionResult GetItem(int assetClassId)
        {
            return this.JsonNet(NidanBusinessService.RetrieveItems(UserOrganisationId, e => e.AssetClassId == assetClassId).ToList());
        }

        [HttpPost]
        public ActionResult DocumentList(string studentCode)
        {
            var organisationId = UserOrganisationId;
            var centreId = UserCentreId;
            var data = NidanBusinessService.RetrieveFixAssetDocuments(organisationId, centreId, studentCode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            var organisationId = UserOrganisationId;
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            var fixAssetData = NidanBusinessService.RetrieveFixAssets(organisationId, e => isSuperAdmin && e.FixAssetId.ToString() == documentViewModel.StudentCode).Items.FirstOrDefault();
            if (fixAssetData != null)
                try
                {
                    _documentService.Create(organisationId, fixAssetData.CentreId,
                        documentViewModel.DocumentTypeId, documentViewModel.StudentCode,
                        fixAssetData.FixAssetId.ToString(), "Fix Asset Document", documentViewModel.Attachment.FileName,
                        documentViewModel.Attachment.InputStream.ToBytes());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

        }

        public ActionResult DownloadDocument(Guid id)
        {
            var document = NidanBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }

    }
}