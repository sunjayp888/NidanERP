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
        public RegistrationPaymentReceipt RegistrationPaymentReceipt { get; set; }
        public Batch Batch { get; set; }
        public CourseInstallment CourseInstallment { get; set; }
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
        public int RegistrationPaymentReceiptId { get; set; }
        public int SectorId { get; set; }
        public int IntrestedCourseId { get; set; }
        public int BatchTimePreferId { get; set; }
        public IEnumerable<SelectListItem> TitleList { get; set; }
        public List<TitleType> TitleType => new List<TitleType>()
        {
            new TitleType() {Name = "Mr.",Value = "Mr."},
            new TitleType() {Name = "Ms.",Value = "Ms."},
            new TitleType() {Name = "Mrs.",Value = "Mrs."}
        };
        public List<int> SelectedTrainerIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<int>>(SelectedTrainerIdsJson);
            }
            set
            {
                SelectedTrainerIdsJson = JsonConvert.SerializeObject(value);
            }
        }

        public string SelectedTrainerIdsJson { get; set; }
    }
}