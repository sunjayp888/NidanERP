using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Entity;

namespace Nidan.Models
{
    public class EnquiryViewModel : BaseViewModel
    {
        public Enquiry Enquiry { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public SelectList Occupations { get; set; }
        public SelectList Religions { get; set; }
        public SelectList CasteCategories { get; set; }
        //public SelectList AreaOfInterests { get; set; }
        public SelectList HowDidYouKnowAbouts { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Schemes { get; set; }
    }
}