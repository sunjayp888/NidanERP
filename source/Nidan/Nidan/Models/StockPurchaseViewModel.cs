using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class StockPurchaseViewModel : BaseViewModel
    {
        public StockIssue StockIssue { get; set; }
        public StockPurchase StockPurchase { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList StockTypes { get; set; }
        public SelectList StudentKits { get; set; }
    }
}