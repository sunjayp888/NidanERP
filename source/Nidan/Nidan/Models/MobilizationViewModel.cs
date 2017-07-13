using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;


namespace Nidan.Models
{
    public class MobilizationViewModel : BaseViewModel
    {
        public MobilizationViewModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public Mobilization Mobilization { get; set; }
        public MobilizationDataGrid MobilizationDataGrid { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Qualifications { get; set; }
        public SelectList Events { get; set; }
        public SelectList MobilizationTypes { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
        public int EventId { get; set; }
        public DateTime GeneratedDate { get; set; }

        public IEnumerable<SelectListItem> TitleList { get; set; }

        public List<TitleType> TitleType => new List<TitleType>()
        {
           new TitleType() { Name = "Mr.",Value ="Mr." },
           new TitleType() { Name = "Ms.",Value ="Ms." },
           new TitleType() { Name = "Mrs.",Value = "Mrs."}
        };

    }

}