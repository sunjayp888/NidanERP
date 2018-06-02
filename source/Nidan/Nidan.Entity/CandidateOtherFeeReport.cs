namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateOtherFeeReport")]
    public partial class CandidateOtherFeeReport
    {
        [StringLength(500)]
        public string StudentCodeNumber { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [StringLength(1000)]
        public string CourseName { get; set; }

        public int? FeeTypeNumber { get; set; }

        [StringLength(50)]
        public string FeeTypeName { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? PaidAmount { get; set; }

        [Key]
        [StringLength(3)]
        public string IsPaymentDone { get; set; }

        public int? PaymentModeId { get; set; }

        [StringLength(100)]
        public string PaymentModeName { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public int? CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [StringLength(500)]
        public string ReceiptNumber { get; set; }

        public int? OrganisationId { get; set; }
    }
}
