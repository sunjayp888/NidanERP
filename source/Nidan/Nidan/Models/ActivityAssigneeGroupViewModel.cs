using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class ActivityAssigneeGroupViewModel : BaseViewModel
    {
        public ActivityAssigneeGroup ActivityAssigneeGroup { get; set; }
        public SelectList Centres { get; set; }
    }
}