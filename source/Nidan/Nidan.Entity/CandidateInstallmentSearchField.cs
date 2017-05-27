namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateInstallmentSearchField")]
    public partial class CandidateInstallmentSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateInstallmentId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public int? CourseFee { get; set; }

        public int? DownPayment { get; set; }

        public int? DiscountAmount { get; set; }

        public int? NumberOfInstallment { get; set; }

        public int? LumpsumAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseInstallmentId { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsPercentageDiscount { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsTotalAmountDiscount { get; set; }

        [StringLength(100)]
        public string PaymentMethod { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(350)]
        public string SearchField { get; set; }
    }
}
