using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class MobilizationSummaryReport
    {
        public string MonthName { get; set; }
        public int CentreId { get; set; }
        public string CentreName { get; set; }
        public int MobilizationCount { get; set; }
        public int EnquiryCount { get; set; }
        public int CounsellingCount { get; set; }
        public int RegistrationCount { get; set; }
        public int AdmissionCount { get; set; }
        public decimal CourseBooking { get; set; }
        public decimal FeeCollected { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Date { get; set; }
    }
}
