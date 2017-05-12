using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class Graph
    {
        public int CentreId { get; set; }
        public string CentreName { get; set; }
        public int MobilizationCount { get; set; }
        public int EnquiryCount { get; set; }
        public int CounsellingCount { get; set; }
        public int RegistrationCount { get; set; }
        public int AdmissionCount { get; set; }
        public DateTime Date { get; set; }
    }
}
