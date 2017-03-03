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
        public SelectList Sectors { get; set; }
        public SelectList BatchTimePrefers { get; set; }
        public SelectList StudentTypes { get; set; }
        public SelectList EnquiryTypes { get; set; }
        public int CreateEnquiryFromMobilizationFollowUpId { get; set; }
        public int MobilizationId { get; set; }
        //  public Counselling Counselling { get; set; }
    }
}