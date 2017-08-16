namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttendanceGrid")]
    public partial class AttendanceGrid
    {
        [StringLength(100)]
        public string StudentCode { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [StringLength(50)]
        public string BioMetricLogTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AttendanceDate { get; set; }

        public bool? IsPresent { get; set; }

        [StringLength(2)]
        public string Direction { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }
    }
}
