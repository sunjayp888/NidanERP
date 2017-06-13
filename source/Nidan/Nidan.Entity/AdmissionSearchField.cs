namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdmissionSearchField")]
    public partial class AdmissionSearchField
    {
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1470)]
        public string SearchField { get; set; }
    }
}
