namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateInstallmentGrid")]
    public partial class CandidateInstallmentGrid
    {
        public int? CandidateInstallmentId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string BatchName { get; set; }

        public decimal? TotalFee { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? PendingAmount { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public int AdmissionId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 5)]
        public int CentreId { get; set; }
    }
}
