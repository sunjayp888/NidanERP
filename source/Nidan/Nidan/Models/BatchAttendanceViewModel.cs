using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class BatchAttendanceViewModel : BaseViewModel
    {
        public BatchAttendance BatchAttendance { get; set; }
        public SelectList Batch { get; set; }
        public SelectList Subject { get; set; }
        public SelectList Session { get; set; }
    }
}