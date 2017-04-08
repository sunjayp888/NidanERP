using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class RoomViewModel : BaseViewModel
    {
        public Room Room { get; set; }
        public SelectList RoomTypes { get; set; }
        public SelectList Centres { get; set; }
    }
}