using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CompanyViewModel : BaseViewModel
    {
        public Company Company { get; set; }
        public SelectList Centres { get; set; }
    }
}