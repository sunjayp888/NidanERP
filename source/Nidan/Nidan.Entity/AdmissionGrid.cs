namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdmissionGrid")]
    public partial class AdmissionGrid
    {
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1000)]
        public string CourseName { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        public int? BatchId { get; set; }

        [StringLength(500)]
        public string BatchName { get; set; }

        public decimal? TotalFee { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? PendingAmount { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(1620)]
        public string SearchField { get; set; }
    }
}
