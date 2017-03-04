using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class GovernmentAdmissionViewModel : BaseViewModel
    {
        public GovernmentAdmission GovernmentAdmission { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList SubSectors { get; set; }
        public SelectList AlternateIdTypes { get; set; }
        public SelectList Courses { get; set; }
        public SelectList HowDidYouKnowAbouts { get; set; }
        public SelectList Disabilities { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public SelectList CasteCategories { get; set; }
        public SelectList Religions { get; set; }
    }
}