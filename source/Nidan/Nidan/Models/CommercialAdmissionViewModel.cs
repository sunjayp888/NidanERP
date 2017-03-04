using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CommercialAdmissionViewModel : BaseViewModel
    {
        public CommercialAdmission CommercialAdmission { get; set; }
        public SelectList CasteCategories { get; set; }
        public SelectList Religions { get; set; }
        public SelectList Talukas { get; set; }
        public SelectList States { get; set; }
        public SelectList Districts { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList Courses { get; set; }
        public SelectList StudentTypes { get; set; }
    }
}