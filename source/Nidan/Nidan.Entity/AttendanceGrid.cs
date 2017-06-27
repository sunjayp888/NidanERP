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

        [Key]
        [Column(Order = 0)]
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [StringLength(114)]
        public string InTime { get; set; }

        [StringLength(114)]
        public string OutTime { get; set; }
    }
}
