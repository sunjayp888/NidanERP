using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class StudentAttendance
    {
        public string CandidateName { get; set; }
        public string StudentCode { get; set; }
        public int PersonnelId { get; set; }
        public int InHour { get; set; }
        public int InMinute { get; set; }
        public string InTimeSpan { get; set; }
        public int OutHour { get; set; }
        public int OutMinute { get; set; }
        public string OutTimeSpan { get; set; }
        public bool IsPresent { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int CentreId { get; set; }
        public int OrganisationId { get; set; }
        public string BiometricLogTime { get; set; }
        public string Direction { get; set; }
        public string Topic { get; set; }
        public int SubjectId { get; set; }
        public int SessionId { get; set; }
    }
}
