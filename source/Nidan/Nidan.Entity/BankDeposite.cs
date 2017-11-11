namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankDeposite")]
    public partial class BankDeposite
    {
        public int BankDepositeId { get; set; }

        public int ProjectId { get; set; }

        [Required]
        [StringLength(1000)]
        public string ReceiptNumber { get; set; }

        [Required]
        [StringLength(4000)]
        public string ReceivedFrom { get; set; }

        public decimal CreditAmount { get; set; }

        [Required]
        [StringLength(1000)]
        public string RupeesInWords { get; set; }

        [Column(TypeName = "date")]
        public DateTime DepositedDate { get; set; }

        public int PaymentModeId { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public bool IsCleared { get; set; }

        public bool IsBounced { get; set; }

        public string Remark { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Project Project { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }
    }

}
