using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class CentreCandidateFeeSummaryReport
    {
        public decimal TotalRegistrationAmount { get; set; }
        public decimal TotalInstallmentAmount { get; set; }
        public decimal TotalDownPaymentAmount { get; set; }
        public string CentreName { get; set; }
        public string Date { get; set; }
        public int CentreId { get; set; }
    }
}
