using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class ExpensePettyCashData
    {
        public string CentreName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherNumber { get; set; }
        public string ExpenseHeader { get; set; }
        public string CashMemoNumbers { get; set; }
        public decimal DebitAmount { get; set; }
        public string PaidTo { get; set; }
        public string Particulars { get; set; }
        public string CreatedBy { get; set; }
        public int PettyCashAmount { get; set; }
        public int OpeningBalance { get; set; }
        public string DetailOfPettyCashRecieved { get; set; }
    }
}
