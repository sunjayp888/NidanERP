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
    [MetadataType(typeof(FixAssetMetadata))]
    public partial class FixAsset : IOrganisationFilterable
    {
        private class FixAssetMetadata
        {
            [Display(Name = "Product")]
            public int ProductId { get; set; }

            [Display(Name = "Date of Purchase")]
            public DateTime DateofPurchase { get; set; }

            [Display(Name = "Supplier")]
            public string Supplier { get; set; }

            [Display(Name = "Bill Number")]
            public string BillNumber { get; set; }

            [Display(Name = "Cost Amount")]
            public decimal CostAmount { get; set; }
            
            }
    }
}
