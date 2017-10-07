using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;

namespace Nidan.Models
{
    public class StockIssueViewModel : BaseViewModel
    {
        public StockIssue StockIssue { get; set; }
        public StockPurchase StockPurchase { get; set; }
        public int StockPurchaseId { get; set; }
        public int BalanceQuantity { get; set; }
    }
}