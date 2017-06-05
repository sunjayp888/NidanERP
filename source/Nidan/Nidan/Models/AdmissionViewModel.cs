using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nidan.Entity;

namespace Nidan.Models
{
    public class AdmissionViewModel : BaseViewModel
    {
        public Admission Admission { get; set; }
        public Registration Registration { get; set; }
        public CandidateFee CandidateFee { get; set; }
        public Batch Batch { get; set; }
        public CourseInstallment CourseInstallment { get; set; }
        public Counselling Counselling { get; set; }
        public Course Course { get; set; }
        public BatchDay BatchDay { get; set; }
        public SelectList PaymentModes { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList Courses { get; set; }
        public SelectList BatchTimePrefers { get; set; }
        public SelectList Batches { get; set; }
        public SelectList Rooms { get; set; }
        public SelectList CourseInstallments { get; set; }
        public SelectList Trainers { get; set; }
        public int RegistrationId { get; set; }
        public decimal? LumpsumAfterRegistration { get; set; }
        public int CourseFeeAfterRegistration { get; set; }
        public int IntrestedCourseId { get; set; }
        public int BatchTimePreferId { get; set; }
        public IEnumerable<SelectListItem> TitleList { get; set; }
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public IEnumerable<string> TrainerName { get; set; }
        public List<TitleType> TitleType => new List<TitleType>()
        {
            new TitleType() {Name = "Mr.",Value = "Mr."},
            new TitleType() {Name = "Ms.",Value = "Ms."},
            new TitleType() {Name = "Mrs.",Value = "Mrs."}
        };

        public List<DiscountType> DiscountType => new List<DiscountType>()
        {
            new DiscountType() {Id=5,Name = "5%"},
            new DiscountType() {Id=10,Name = "10%"}
        };
    }
    public class DiscountType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}