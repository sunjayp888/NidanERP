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
    [MetadataType(typeof(CandidateFeeMetadata))]
    partial class CandidateFee : IOrganisationFilterable
    {
        private class CandidateFeeMetadata
        {
            [Display(Name = "Paid Amount")]
            public decimal? PaidAmount { get; set; }

            [Display(Name = "Payment Mode")]
            public int PaymentModeId { get; set; }
        }
    }
}
