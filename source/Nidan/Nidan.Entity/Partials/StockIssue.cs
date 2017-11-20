using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(StockIssueMetadata))]
    public partial class StockIssue : IOrganisationFilterable
    {
        private class StockIssueMetadata
        {
            [Display(Name = "Issued Date")]
            public DateTime IssuedDate { get; set; }

            [Display(Name = "Issued Quantity")]
            public int IssuedQuantity { get; set; }

            [Display(Name = "Issued To Person")]
            public string IssuedToPerson { get; set; }

        }
    }
}
