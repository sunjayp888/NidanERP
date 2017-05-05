using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var paymentmodes = NidanBusinessService.RetrievePaymentModes(organisationId, e => true);

            var viewModel = new CandidateFeeViewModel()
            {
                CandidateFee = new CandidateFee(),
                PaymentModes = new SelectList(paymentmodes, "PaymentModeId", "Name"),
            };
            return View(viewModel);
        }


        //post : create/candidatefee
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateFeeViewModel candidateFeeViewModel)
        {
            var organisationId = UserOrganisationId;
            candidateFeeViewModel.CandidateFee.PaymentDate = DateTime.UtcNow;
            candidateFeeViewModel.CandidateFee.InstallmentDate = DateTime.UtcNow;
            candidateFeeViewModel.CandidateFee.FeeTypeId = 2;
            candidateFeeViewModel.CandidateFee.FiscalYear = "2017-18";
            if (ModelState.IsValid)
            {
                candidateFeeViewModel.CandidateFee.OrganisationId = organisationId;
                candidateFeeViewModel.CandidateFee.CentreId = UserCentreId;

                candidateFeeViewModel.CandidateFee = NidanBusinessService.CreateCandidateFee(organisationId, candidateFeeViewModel.CandidateFee);
                return RedirectToAction("Index");
            }

            candidateFeeViewModel.PaymentModes = new SelectList(NidanBusinessService.RetrievePaymentModes(organisationId, e => true).ToList());
            return View(candidateFeeViewModel);
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
        public ActionResult Detail(int? candidateInstallmentId)
        {
            var isSuperAdmin = User.IsSuperAdmin();
            var data = NidanBusinessService.RetrieveCandidateFees(UserOrganisationId, p => (isSuperAdmin || p.CentreId == UserCentreId) && p.CandidateInstallmentId == candidateInstallmentId).Items;
            var candidateFeeModel = new CandidateFeeViewModel
            {
                CandidateFeeList = data.ToList()
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