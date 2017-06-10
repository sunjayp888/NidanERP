namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherFee")]
    public partial class OtherFee
    {
        public int OtherFeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Project { get; set; }

        public int ExpenseHeaderId { get; set; }

        [Required]
        [StringLength(500)]
        public string CashMemo { get; set; }

        [Range(0.0,5000.0, ErrorMessage = "Amount should be less than 5000")]
        public decimal DebitAmount { get; set; }

        [Required]
        public string RupeesInWord { get; set; }

        [Required]
        [StringLength(500)]
        public string PaidTo { get; set; }

        [Required]
        public string Particulars { get; set; }

        public int PaymentModeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PrintDate { get; set; }

        public int Unit { get; set; }

        public decimal Rate { get; set; }

        public string Description { get; set; }

        public bool Approved { get; set; }

        public int? ApprovedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int PersonnelId { get; set; }

        public virtual ExpenseHeader ExpenseHeader { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
