using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class CandidatePrePlacementActivityController : BaseController
    {
        private readonly INidanBusinessService _nidanBusinessService;
        public CandidatePrePlacementActivityController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }
        // GET: CandidatePrePlacementActivity
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
    }
}