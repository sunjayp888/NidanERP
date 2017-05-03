using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(AdmissionMetadata))]
    public partial class Admission : IOrganisationFilterable
    {
        private class AdmissionMetadata
        {
            //[Display(Name = "Payment Mode")]
            //public int PaymentModeId { get; set; }

            //[Display(Name = "Cheque No")]
            //public string ChequeNo { get; set; }

            //[Display(Name = "Cheque Date")]
            //public DateTime ChequeDate { get; set; }

            //[Display(Name = "Bank Name")]
            //public string BankName { get; set; }

            //[Display(Name = "Fee By Student")]
            //public int? FeeByStudent { get; set; }

            //[Display(Name = "Discount Fee")]
            //public int? DiscountFee { get; set; }

            //[Display(Name = "Down Payment")]
            //public int? DownPayment { get; set; }

            //[Display(Name = "Batch")]
            //public int BatchId { get; set; }
        }
    }
}
