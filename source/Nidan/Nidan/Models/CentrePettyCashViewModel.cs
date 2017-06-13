using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CentrePettyCashViewModel : BaseViewModel
    {
        public CentrePettyCash CentrePettyCash { get; set; }
        public SelectList Centres { get; set; }
    }
}