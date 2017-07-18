using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class MobilizationCountData
    {
        public string MobilizationCount { get; set; }
        public string EnquiryCount { get; set; }
        public string CounsellingCount { get; set; }
        public string RegistrationCount { get; set; }
        public string AdmissionCount { get; set; }
        public string Booking { get; set; }
        public string RevenueCollected { get; set; }
        public DateTime Date { get; set; }
    }
}
