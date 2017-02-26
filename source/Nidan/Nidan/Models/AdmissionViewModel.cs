using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class AdmissionViewModel : BaseViewModel
    {
        public Admission Admission { get; set; }
        public SelectList CasteCategories { get; set; }
        public SelectList Religions { get; set; }
        public SelectList Talukas { get; set; }
        public SelectList States { get; set; }
        public SelectList Districts { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public SelectList Courses { get; set; }
        public SelectList SchemeTypes { get; set; }
        public SelectList Batches { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList SubSectors { get; set; }
        public SelectList Disabilities { get; set; }
        public SelectList AlternateIdTypes { get; set; }
    }
}