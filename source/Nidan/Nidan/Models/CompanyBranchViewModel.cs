using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CompanyBranchViewModel : BaseViewModel
    {
        public CompanyBranch CompanyBranch { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public SelectList Sectors { get; set; }
    }
}