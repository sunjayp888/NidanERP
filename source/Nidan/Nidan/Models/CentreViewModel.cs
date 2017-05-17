using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CentreViewModel : BaseViewModel
    {
        public Centre Centre { get; set; }
        public SelectList Talukas { get; set; }
        public SelectList Districts { get; set; }
        public SelectList States { get; set; }
    }
}