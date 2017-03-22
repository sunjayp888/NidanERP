using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class TrainerViewModel : BaseViewModel
    {
        public Trainer Trainer { get; set; }
        public DateTime CreatedDate { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Sectors { get; set; }
    }
}