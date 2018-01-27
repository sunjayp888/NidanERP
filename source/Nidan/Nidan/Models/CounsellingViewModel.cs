using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CounsellingViewModel : BaseViewModel
    {
        public Counselling Counselling { get; set; }
        public CounsellingDataGrid CounsellingDataGrid { get; set; }
        public Enquiry Enquiry { get; set; }
        public SelectList Courses { get; set; }
        public SelectList EnquiryCourses { get; set; }
        public SelectList Sectors { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
        public SelectList DocumentTypes { get; set; }
        public Entity.Document Document { get; set; }
        public int EnquiryId { get; set; }
        public int CounsellingId { get; set; }
        public double ConversionProspect { get; set; }
        public IEnumerable<SelectListItem> ConversionProspectList { get; set; }
        public IEnumerable<SelectListItem> TitleList { get; set; }
        public SelectList Occupations { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList BatchTimePrefers { get; set; }
        public SelectList EducationalQualifications { get; set; }
        public IEnumerable<SelectListItem> PreferredMonthForJoiningList { get; set; }

        public List<ConversionProspectType> ConversionProspectType => new List<ConversionProspectType>()
        {
            new ConversionProspectType() {Id=90,Name="90 - 100" },
            new ConversionProspectType() {Id=80,Name="80 - 90" },
            new ConversionProspectType() {Id=70,Name="70 - 80" },
            new ConversionProspectType() {Id=60,Name="60 - 70" },
            new ConversionProspectType() {Id=50,Name="50 - 60" },
            new ConversionProspectType() {Id=40,Name="40 - 50" },
            new ConversionProspectType() {Id=30,Name="Below 40" }
        };

        public List<TitleType> TitleType => new List<TitleType>()
        {
            new TitleType() {Name = "Mr.",Value = "Mr."},
            new TitleType() {Name = "Ms.",Value = "Ms."},
            new TitleType() {Name = "Mrs.",Value = "Mrs."}
        };

        public List<PreferredMonthForJoiningType> PreferredMonthForJoiningType => new List<PreferredMonthForJoiningType>()
        {
            new PreferredMonthForJoiningType() {Id = 01,Name = "January"},
            new PreferredMonthForJoiningType() {Id = 02,Name = "February"},
            new PreferredMonthForJoiningType() {Id = 03,Name = "March"},
            new PreferredMonthForJoiningType() {Id = 04,Name = "April"},
            new PreferredMonthForJoiningType() {Id = 05,Name = "May"},
            new PreferredMonthForJoiningType() {Id = 06,Name = "June"},
            new PreferredMonthForJoiningType() {Id = 07,Name = "July"},
            new PreferredMonthForJoiningType() {Id = 08,Name = "August"},
            new PreferredMonthForJoiningType() {Id = 09,Name = "September"},
            new PreferredMonthForJoiningType() {Id = 10,Name = "October"},
            new PreferredMonthForJoiningType() {Id = 11,Name = "November"},
            new PreferredMonthForJoiningType() {Id = 12,Name = "December"}
        };
    }
}