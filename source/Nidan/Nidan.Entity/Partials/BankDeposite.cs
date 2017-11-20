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
    [MetadataType(typeof(Entity.BankDeposite.BankDepositeMetadata))]
    public partial class BankDeposite : IOrganisationFilterable
    {
        private class BankDepositeMetadata
        {

            [Display(Name = "Project")]
            public int ProjectId { get; set; }

            [Display(Name = "Receipt Number")]
            public string ReceiptNumber { get; set; }

            [Display(Name = "Received From")]
            public string ReceivedFrom { get; set; }

            [Display(Name = "Credit Amount")]
            public decimal CreditAmount { get; set; }

            [Display(Name = "Rupees In Words")]
            public string RupeesInWords { get; set; }

            [Display(Name = "Deposited Date")]
            public DateTime DepositedDate { get; set; }

            [Display(Name = "Payment Mode")]
            public int PaymentModeId { get; set; }

            [Display(Name = "Cheque Number")]
            public string ChequeNumber { get; set; }

            [Display(Name = "Cheque Date")]
            public DateTime? ChequeDate { get; set; }

            [Display(Name = "Bank Name")]
            public string BankName { get; set; }
        }

    }
}
