using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;


namespace Nidan.Models
{
    public class ModuleViewModel : BaseViewModel
    {
        public Module Module { get; set; }
        public SelectList Courses { get; set; }
    }
}