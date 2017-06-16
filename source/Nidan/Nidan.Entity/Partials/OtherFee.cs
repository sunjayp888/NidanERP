using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;


namespace Nidan.Entity
{
    [MetadataType(typeof(OtherFeeMetadata))]
    public partial class OtherFee : IOrganisationFilterable
    {
        private class OtherFeeMetadata
        {
            [Display(Name = "Expense Head")]
            public int ExpenseHeaderId { get; set; }

            [Display(Name = "Project")]
            public int ProjectId { get; set; }

            [Display(Name = "Cash Memo Number")]
            public string CashMemo { get; set; }

            [Display(Name = "Debit Amount")]
            public decimal DebitAmount { get; set; }

            [Display(Name = "Rupees In Word")]
            public string RupeesInWord { get; set; }

            [Display(Name = "Paid To")]
            public string PaidTo { get; set; }
        }
    }
}
