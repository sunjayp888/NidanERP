using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class ActivityViewModel: BaseViewModel
    {
        public Activity Activity { get; set; }
        public SelectList ActivityTypes { get; set; }
        public SelectList Centres { get; set; }
        public SelectList Projects { get; set; }
        public SelectList ActivityAssigneeGroups { get; set; }
    }
}