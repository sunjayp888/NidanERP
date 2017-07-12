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
    public class RoomController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        public RoomController(INidanBusinessService nidanBusinessService) : base(nidanBusinessService)
        {
        }

        // GET: Room
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Room/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var roomTypes = NidanBusinessService.RetrieveRoomTypes(organisationId, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var viewModel = new RoomViewModel
            {
                Room = new Room(),
                RoomTypes = new SelectList(roomTypes, "RoomTypeId", "Name"),
                Centres = new SelectList(centres, "CentreId", "Name")
            };

            return View(viewModel);
        }

        // POST: Room/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomViewModel roomViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                roomViewModel.Room.OrganisationId = UserOrganisationId;
                //roomViewModel.Room.CentreId = UserCentreId;
                roomViewModel.Room = NidanBusinessService.CreateRoom(UserOrganisationId, roomViewModel.Room);
                return RedirectToAction("Index");
            }
            roomViewModel.RoomTypes = new SelectList(NidanBusinessService.RetrieveRoomTypes(organisationId, e => true).ToList());
            roomViewModel.Centres = new SelectList(NidanBusinessService.RetrieveCentres(organisationId, e => true).ToList());
            return View(roomViewModel);
        }

        // GET: Room/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organisationId = UserOrganisationId;
            var roomTypes = NidanBusinessService.RetrieveRoomTypes(organisationId, e => true);
            var centres = NidanBusinessService.RetrieveCentres(organisationId, e => true);
            var room = NidanBusinessService.RetrieveRoom(UserOrganisationId, id.Value);

            if (room == null)
            {
                return HttpNotFound();
            }
            var viewModel = new RoomViewModel
            {
                Room = room,
                RoomTypes = new SelectList(roomTypes, "RoomTypeId", "Name"),
                Centres = new SelectList(centres, "CentreId", "Name")
            };
            return View(viewModel);
        }

        // POST: Room/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomViewModel roomViewModel)
        {
            if (ModelState.IsValid)
            {
                roomViewModel.Room.OrganisationId = UserOrganisationId;
                //roomViewModel.Room.CentreId = UserCentreId;
                roomViewModel.Room = NidanBusinessService.UpdateRoom(UserOrganisationId, roomViewModel.Room);
                return RedirectToAction("Index");
            }
            var viewModel = new RoomViewModel
            {
                Room = roomViewModel.Room
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(NidanBusinessService.RetrieveRooms(UserOrganisationId, p => (isSuperAdmin || p.CentreId==UserCentreId), orderBy, paging));
        }
    }
}