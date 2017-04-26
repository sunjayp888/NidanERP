using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;
using System.Web.Mvc;

namespace Nidan.Models
{
    public class FollowUpViewModel:BaseViewModel
    {
        public FollowUp FollowUp { get; set; }
        public SelectList Courses { get; set; }

        public IEnumerable<SelectListItem> TitleList { get; set; }

        public List<TitleType> TitleType => new List<TitleType>()
        {
           new TitleType() { Name = "Mr.",Value ="Mr." },
           new TitleType() { Name = "Ms.",Value ="Ms." },
           new TitleType() { Name = "Mrs.",Value = "Mrs."}
        };
    }

    public class FollowUpDto 
    {
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string InterestedCourse { get; set; }
        public string Qualification { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string Remark { get; set; }

       
    }
}