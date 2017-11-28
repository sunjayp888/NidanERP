using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class CentreBankDepositeReport
    {
        public decimal TotalBankDepositeAmount { get; set; }
        public string CentreName { get; set; }
        public string Date { get; set; }
        public int CentreId { get; set; }
    }
}
