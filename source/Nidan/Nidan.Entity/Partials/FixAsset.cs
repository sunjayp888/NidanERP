using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(FixAssetMetaData))]
    public partial class FixAsset : IOrganisationFilterable
    {
        private class FixAssetMetaData
        {
            [Display(Name = "Asset Class")]
            public int AssetClassId { get; set; }

            [Display(Name = "Item")]
            public int ItemId { get; set; }

            [Display(Name = "Invoice Number")]
            public string InvoiceNumber { get; set; }

            [Display(Name = "Date of Purchase")]
            public DateTime DateofPurchase { get; set; }

            [Display(Name = "Cost")]
            public decimal Cost { get; set; }

            [Display(Name = "Purchase From")]
            public string PurchaseFrom { get; set; }

            [Display(Name = "Quantity")]
            public int Quantity { get; set; }

            [Display(Name = "Remarks")]
            public int Remark { get; set; }
        }
    }
}
