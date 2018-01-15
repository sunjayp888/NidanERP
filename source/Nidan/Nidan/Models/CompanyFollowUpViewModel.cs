using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CompanyFollowUpViewModel : BaseViewModel
    {
        public CompanyFollowUp CompanyFollowUp { get; set; }
        public CompanyBranch CompanyBranch { get; set; }
        public Company Company { get; set; }
        public string HREmail1 { get; set; }
        public long HRContact1 { get; set; }
        public string HREmail2 { get; set; }
        public long HRContact2 { get; set; }
        public string HRName1 { get; set; }
        public string HRName2 { get; set; }
    }
}