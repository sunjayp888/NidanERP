namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateFeeReport")]
    public partial class CandidateFeeReport
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateFeeId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string CourseName { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? CandidateInstallmentId { get; set; }

        public decimal? PaidAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PaymentModeName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string FeeTypeName { get; set; }

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
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsPaymentDone { get; set; }

        public int? PersonnelId { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool IsPaidAmountOverride { get; set; }

        public decimal? AdvancedAmount { get; set; }

        [StringLength(100)]
        public string ReferenceReceiptNumber { get; set; }

        [Key]
        [Column(Order = 11)]
        public bool HaveReceipt { get; set; }

        [StringLength(500)]
        public string ReceiptNumber { get; set; }
    }
}
