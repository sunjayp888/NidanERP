using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class ModuleExamSetViewModel : BaseViewModel
    {
        public ModuleExamSet ModuleExamSet { get; set; }
        public SelectList Subjects { get; set; }
    }
}