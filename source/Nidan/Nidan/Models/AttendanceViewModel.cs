using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class AttendanceViewModel : BaseViewModel
    {
        public Attendance Attendance { get; set; }
    }
}