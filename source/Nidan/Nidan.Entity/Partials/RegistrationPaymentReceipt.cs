using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;


namespace Nidan.Entity
{
    [MetadataType(typeof(RegistrationPaymentReceiptMetadata))]
    public partial class RegistrationPaymentReceipt : IOrganisationFilterable
    {
        private class RegistrationPaymentReceiptMetadata
        {
            [Display(Name = "Payment Mode")]
            public int PaymentModeId { get; set; }

            [Display(Name = "Cheque No")]
            public string ChequeNo { get; set; }

            [Display(Name = "Cheque Date")]
            public DateTime ChequeDate { get; set; }

            [Display(Name = "Bank Name")]
            public string BankName { get; set; }
        }
    }
}
