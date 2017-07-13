using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CourseInstallmentViewModel : BaseViewModel
    {
        public CourseInstallment CourseInstallment { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Centres { get; set; }
    }
}