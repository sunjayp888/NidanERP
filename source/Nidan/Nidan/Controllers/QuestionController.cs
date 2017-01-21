using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Nidan.Business;
using Nidan.Business.Interfaces;
using Nidan.Entity;
using Nidan.Entity.Dto;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    public class QuestionController : BaseController
    {
        public QuestionController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Question
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Create()
        {
            var viewModel = new QuestionViewModel
            {
                Question = new Question(),
                EventActivityTypes = NidanBusinessService.RetrieveActivityTypes(UserOrganisationId)
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionId")] QuestionViewModel question)
        {
            if (ModelState.IsValid)
            {
                var result = NidanBusinessService.CreateQuestion(UserOrganisationId, question.Question);
                return View();
            }
            return View(new QuestionViewModel
            {
                Question = question.Question,
            });
        }

        // GET: AbsenceType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = NidanBusinessService.RetrieveQuestion(UserOrganisationId, id.Value, q => true);

            var viewmodel = new QuestionViewModel()
            {
                Question = question
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveQuestions(UserOrganisationId, q => true, orderBy, paging));
        }

    }
}