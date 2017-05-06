using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class CandidateFeeController : BaseController
    {
        public CandidateFeeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }
        // GET: CandidateFee
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["CandidateFee"] = id;
            var organisationId = UserOrganisationId;
            var candidateFee= NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value);
           // var paymentmodes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);

            if (candidateFee == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CandidateFeeViewModel
            {
                CandidateFee = candidateFee,
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, e => true).ToList(), "PaymentModeId", "Name"),
            };
            return View(viewModel);
        }


        //post : create/candidatefee
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateFeeViewModel candidateFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                candidateFeeViewModel.CandidateFee.OrganisationId = organisationId;
                candidateFeeViewModel.CandidateFee.CentreId = UserCentreId;
                candidateFeeViewModel.CandidateFee.PaymentDate = DateTime.UtcNow;
                candidateFeeViewModel.CandidateFee.InstallmentDate = DateTime.UtcNow;
                candidateFeeViewModel.CandidateFee.FeeTypeId = 2;
                candidateFeeViewModel.CandidateFee.FiscalYear = "2017-18";
                candidateFeeViewModel.CandidateFee = NidanBusinessService.UpdateCandidateFee(organisationId, candidateFeeViewModel.CandidateFee);
                return RedirectToAction("Index");
            }
            var viewModel = new CandidateFeeViewModel
            {
                PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList(), "PaymentModeId", "Name")
            };
            return View(viewModel);
        }

        //[Authorize(Roles = "Admin")]
        //public ActionResult GetCandidateFee(int? id)
        //{
        //    return View(viewModel);
        //}

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveRegistrations(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId), orderBy, paging);
            return this.JsonNet(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Detail(int? id)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveCandidateFees(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CandidateInstallmentId == id).Items;
            var candidateFeeModel = new CandidateFeeViewModel
            {
                CandidateFeeList = data.ToList(),
                //var candidateFee = NidanBusinessService.RetrieveCandidateFee(organisationId, id.Value);
            PaymentModes =new SelectList(NidanBusinessService.RetrievePaymentModes(UserOrganisationId, e => true).ToList(),"PaymentModeId", "Name")
            };
            return View(candidateFeeModel);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsSuperAdmin();
            return this.JsonNet(NidanBusinessService.RetrieveCandidateFeeBySearchKeyword(UserOrganisationId, searchKeyword, p => isSuperAdmin || p.CentreId == UserCentreId, orderBy, paging));
        }
    }
}