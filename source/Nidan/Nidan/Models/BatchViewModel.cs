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
        public BatchDay BatchDay { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList CourseFeeBreakUps { get; set; }
        //public bool IsMonday { get; set; }

        //public bool IsTuesday { get; set; }

        //public bool IsWednusday { get; set; }

        //public bool IsThusday { get; set; }

        //public bool IsFriday { get; set; }

        //public bool IsSaturday { get; set; }

        //public bool IsSunday { get; set; }
    }
}