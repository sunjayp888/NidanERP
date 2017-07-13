using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Entity;
using Nidan.Extensions;
using Nidan.Models;

namespace Nidan.Controllers
{
    [Authorize]
    public class AbsenceTypeController : BaseController
    {
        private static string AbsenceName { get; set; }
        public AbsenceTypeController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: AbsenceType
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: AbsenceType/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Create()
        {
            var viewModel = new AbsenceTypeViewModel
            {
                AbsenceType = new AbsenceType()
                {
                    OrganisationId = UserOrganisationId,
                },
                ColoursList = NidanBusinessService.RetrieveColours().ToList()
            };
            return View(viewModel);
        }

        // POST: AbsenceType/Create
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //OrganisationId,Name,ReduceEntitlement
        public ActionResult Create([Bind(Include = "AbsenceTypeId,Name,ColourId")] AbsenceType absenceType)
        {
            if (ModelState.IsValid)
            {
                //Create AbsenceType               
                //var result = NidanBusinessService.CreateAbsenceType(UserOrganisationId, absenceType);
                //if (result.Succeeded)
                //{
                //    return RedirectToAction("Index");
                //}
                //foreach (var error in result.Errors)    
                //{
                //    ModelState.AddModelError("", error);
                //}
                return View();
            }

            return View(new AbsenceTypeViewModel {AbsenceType = absenceType,
                ColoursList = NidanBusinessService.RetrieveColours().ToList()
            });
        }

        // GET: AbsenceType/Edit/5
        [Authorize(Roles = "Admin , SuperAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var absenceType = NidanBusinessService.RetrieveAbsenceType(UserOrganisationId, id.Value);
            AbsenceName = absenceType.Name;
            var viewmodel = new AbsenceTypeViewModel()
            {
                AbsenceType = absenceType,
                ColoursList = NidanBusinessService.RetrieveColours().ToList()
            };

            return View(viewmodel);
        }

        // POST: AbsenceType/Edit/5
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbsenceTypeId,Name,ColourId")] AbsenceType absenceType)
        {
            //if (ModelState.IsValid)
            //{
            //    var result = NidanBusinessService.UpdateAbsenceType(UserOrganisationId, absenceType);
            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error);
            //    }

            //}
            return View(new AbsenceTypeViewModel { AbsenceType = new Entity.AbsenceType(),
                ColoursList = NidanBusinessService.RetrieveColours().ToList()
            });
        }

        // POST: AbsenceType/Delete/5
        [Authorize(Roles = "Admin , SuperAdmin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
         //   NidanBusinessService.DeleteAbsenceType(UserOrganisationId, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(NidanBusinessService.RetrieveAbsenceTypes(UserOrganisationId, orderBy, paging));
        }

        [HttpPost]
        public ActionResult CanDeleteAbsenceType(int id)
        {
          //  return this.JsonNet(NidanBusinessService.CanDeleteAbsenceType(UserOrganisationId, id));
            return null;
        }
    }
}
