using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(StockPurchaseMetadata))]
    public partial class StockPurchase : IOrganisationFilterable
    {

        private class StockPurchaseMetadata
        {
            [Display(Name = "Purchase Date")]
            public DateTime StockPurchaseDate { get; set; }

            [Display(Name = "Inventory Item")]
            public string InventoryItem { get; set; }

            [Display(Name = "Total Quantity")]
            public int Quantity { get; set; }

            [Display(Name = "Supplier")]
            public string Supplier { get; set; }

            [Display(Name = "Bill Number")]
            public string BillNumber { get; set; }

            [Display(Name = "Amount")]
            public decimal Amount { get; set; }

            [Display(Name = "Stock Type")]
            public string StockTypeId { get; set; }

        }
    }
}
