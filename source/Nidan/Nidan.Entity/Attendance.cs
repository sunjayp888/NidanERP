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
        public Attendance()
        {
            AttendanceDate=DateTime.UtcNow.Date;
        }
        public int AttendanceId { get; set; }

        public int PersonnelId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentCode { get; set; }

        public int? InHour { get; set; }

        public int? InMinute { get; set; }

        [StringLength(50)]
        public string InTimeSpan { get; set; }

        public int? OutHour { get; set; }

        public int? OutMinute { get; set; }

        [StringLength(50)]
        public string OutTimeSpan { get; set; }

        public bool? IsPresent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AttendanceDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Personnel Personnel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchAttendance> BatchAttendances { get; set; }
    }
}
