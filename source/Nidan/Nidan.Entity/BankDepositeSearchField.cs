namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankDepositeSearchField")]
    public partial class BankDepositeSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BankDepositeId { get; set; }

        public int? ProjectId { get; set; }

        [StringLength(100)]
        public string Project { get; set; }

        [StringLength(4000)]
        public string ReceivedFrom { get; set; }

        [StringLength(1000)]
        public string ReceiptNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal CreditAmount { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DepositedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string PaymentMode { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsCleared { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool IsBounced { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(1350)]
        public string SearchField { get; set; }
    }
}
