using Nidan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace Nidan.Models
{
    public class BatchViewModel : BaseViewModel
    {
        public Batch Batch { get; set; }
        public BatchDay BatchDay { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList CourseInstallments { get; set; }

    }
}