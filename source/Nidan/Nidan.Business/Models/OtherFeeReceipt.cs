using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity;

namespace Nidan.Business.Models
{
    class OtherFeeReceipt
    {
        public string OrganisationName { get; set; }
        public string CentreAddress { get; set; }
        public string CentreName { get; set; }
        public string VoucherNumber { get; set; }
        public string Project { get; set; }
        public string ExpenseHeader { get; set; }
        public string CashMemo { get; set; }
        public List<decimal> DebitAmount { get; set; }
        public List<int> Unit { get; set; }
        public List<decimal> Rate { get; set; }
        public List<string> Description { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public string RupeesInWords { get; set; }
        public string PaidTo { get; set; }
        public string VoucherCreatedDate { get; set; }
    }
}
