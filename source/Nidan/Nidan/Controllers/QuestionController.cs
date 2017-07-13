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

        // GET: Question/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var eventFunctionTypes = NidanBusinessService.RetrieveEventFunctionTypes(organisationId, e => true);
            var viewModel = new QuestionViewModel
            {
                Question = new Question(),
                EventFunctionTypes = new SelectList(eventFunctionTypes, "EventFunctionTypeId", "Name")
                // EventActivityTypes = NidanBusinessService.RetrieveActivityTypes(UserOrganisationId)
            };
            return View(viewModel);
        }

        // POST: Question/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel questionViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                questionViewModel.Question.OrganisationId = UserOrganisationId;
                questionViewModel.Question = NidanBusinessService.CreateQuestion(UserOrganisationId, questionViewModel.Question);
                return RedirectToAction("Index");
            }
            questionViewModel.EventFunctionTypes = new SelectList(NidanBusinessService.RetrieveEventFunctionTypes(organisationId, e => true).ToList());
            return View(questionViewModel);
        }

        // GET: Question/Edit/{id}
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Edit(int? id)
        {
            var organisationId = UserOrganisationId;
            var eventFunctionTypes = NidanBusinessService.RetrieveEventFunctionTypes(organisationId, e => true);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = NidanBusinessService.RetrieveQuestion(UserOrganisationId, id.Value, q => true);

            var viewModel = new QuestionViewModel
            {
                Question = question,
                EventFunctionTypes = new SelectList(eventFunctionTypes, "EventFunctionTypeId", "Name")
            };

            return View(viewModel);
        }

        // POST: Question/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel questionViewModel)
        {
            if (ModelState.IsValid)
            {
                questionViewModel.Question.OrganisationId = UserOrganisationId;
                questionViewModel.Question = NidanBusinessService.UpdateQuestion(UserOrganisationId, questionViewModel.Question);
                return RedirectToAction("Index");
            }
            var viewModel = new QuestionViewModel
            {
                Question = questionViewModel.Question
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveQuestions(UserOrganisationId,q=>true, orderBy, paging));
        }

    }
}