using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class BankDepositeSummaryReport
    {
        public string MonthName { get; set; }
        public int CentreId { get; set; }
        public string CentreName { get; set; }
        public decimal TotalBankDepositeAmount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Date { get; set; }
    }
}
