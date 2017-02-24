using Nidan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nidan.Models
{
    public class BatchViewModel : BaseViewModel
    {
        public Batch Batch { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
    }
}