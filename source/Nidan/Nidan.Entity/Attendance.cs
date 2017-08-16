namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public int AttendanceId { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        public bool? IsPresent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AttendanceDate { get; set; }

        public DateTime? AttendanceDateTime { get; set; }

        public int? CentreId { get; set; }

        public int? OrganisationId { get; set; }

        [StringLength(50)]
        public string BioMetricLogTime { get; set; }

        [StringLength(2)]
        public string Direction { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Personnel Personnel { get; set; }
    }
}
