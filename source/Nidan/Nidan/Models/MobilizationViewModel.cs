using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Entity;


namespace Nidan.Models
{
    public class MobilizationViewModel : BaseViewModel
    {
        public Mobilization Mobilization { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Qualifications { get; set; }
       // public SelectList Events { get; set; }
    }
}