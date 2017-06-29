using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using HR.Entity;
using Newtonsoft.Json;
using Nidan.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace Nidan.Models
{
    public class EnquiryViewModel : BaseViewModel
    {
        public Enquiry Enquiry { get; set; }
        public List<string>CourseNames { get; set; }
        public EnquiryDataGrid EnquiryDataGrid { get; set; }
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
        public SelectList Talukas { get; set; }
        public SelectList Districts { get; set; }
        public SelectList States { get; set; }
        public int CreateEnquiryFromMobilizationFollowUpId { get; set; }
        public int MobilizationId { get; set; }
        public double ConversionProspect { get; set; }
        public IEnumerable<SelectListItem> ConversionProspectList { get; set; }
        public string PreferredMonthForJoining { get; set; }
        public IEnumerable<SelectListItem> PreferredMonthForJoiningList { get; set; }
        public IEnumerable<SelectListItem> TitleList { get; set; }
        //public Month Month { get; set; }

        //  public Counselling Counselling { get; set; }

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

        public List<TitleType> TitleType => new List<TitleType>()
        {
            new TitleType() {Name = "Mr.",Value = "Mr."},
            new TitleType() {Name = "Ms.",Value = "Ms."},
            new TitleType() {Name = "Mrs.",Value = "Mrs."}
        };

        public List<int> SelectedCourseIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<int>>(SelectedCourseIdsJson);
            }
            set
            {
                SelectedCourseIdsJson = JsonConvert.SerializeObject(value);
            }
        }

        public string SelectedCourseIdsJson { get; set; }
    }

    public class TitleType
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ConversionProspectType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class PreferredMonthForJoiningType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}