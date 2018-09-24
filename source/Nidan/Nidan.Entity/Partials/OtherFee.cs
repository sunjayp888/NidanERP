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
    [MetadataType(typeof(OtherFeeMetadata))]
    public partial class OtherFee : IOrganisationFilterable
    {
        private class OtherFeeMetadata
        {
            [Display(Name = "Fee Type")]
            public int FeeTypeId { get; set; }

            [Display(Name = "Online Exam")]
            public int OnlineExamId { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

            [Display(Name = "Paid Amount")]
            public decimal PaidAmount { get; set; }

            [Display(Name = "Payment Date")]
            public DateTime PaymentDate { get; set; }

            [Display(Name = "Remarks")]
            public string Remark { get; set; }
        }
    }
}
