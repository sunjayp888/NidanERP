namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateFeeGrid")]
    public partial class CandidateFeeGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateFeeId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? CandidateInstallmentId { get; set; }

        public decimal? PaidAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string PaymentMode { get; set; }

        [StringLength(100)]
        public string FeeType { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public decimal? Penalty { get; set; }

        public DateTime? InstallmentDate { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public int? InstallmentNumber { get; set; }

        public decimal? InstallmentAmount { get; set; }

        public decimal? BalanceInstallmentAmount { get; set; }

        public string Particulars { get; set; }

        [StringLength(50)]
        public string FiscalYear { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Organisation { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsPaymentDone { get; set; }

        public int? PersonnelId { get; set; }

        public bool? IsPaidAmountOverride { get; set; }

        public decimal? AdvancedAmount { get; set; }

        public DateTime? NextInstallmentDate { get; set; }
    }
}
