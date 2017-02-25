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
        public SelectList Courses { get; set; }
        //public SelectList Enquiries { get; set; }
    }

    //public class Counselling
    //{
    //    public string Name { get; set; }
    //    public string RemarkByBm { get; set; }
    //    public DateTime FollowUpDate { get; set; }
    //    public string Remarks { get; set; }
    //    public string PreferTiming { get; set; }
    //    public string CounselledBy { get; set; }
    //}
}