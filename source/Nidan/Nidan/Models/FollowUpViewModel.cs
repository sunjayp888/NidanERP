using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class FollowUpViewModel:BaseViewModel
    {
        public FollowUp FollowUp { get; set; }
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