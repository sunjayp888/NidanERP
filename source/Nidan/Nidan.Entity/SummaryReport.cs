namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SummaryReport")]
    public partial class SummaryReport
    {
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(1000)]
        public string CourseName { get; set; }

        public decimal? PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        public decimal? InstallmentAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string FeeTypeName { get; set; }

        public int? CandidateInstallmentId { get; set; }

        public decimal? CourseFee { get; set; }

        public decimal? LumpsumAmount { get; set; }

        public int? NumberOfInstallment { get; set; }

        [StringLength(500)]
        public string ReceiptNumber { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool ISPaymentDone { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateFeeId { get; set; }

        public decimal? TotalPaidAmount { get; set; }

        public decimal? BalanceAmount { get; set; }
    }
}
