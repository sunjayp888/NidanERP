namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchAttendance")]
    public partial class BatchAttendance
    {
        public int BatchAttendanceId { get; set; }

        public int BatchId { get; set; }

        public int AttendanceId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [StringLength(500)]
        public string Topic { get; set; }

        public int? BatchTrainerId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentCode { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual BatchTrainer BatchTrainer { get; set; }

        public virtual Attendance Attendance { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Session Session { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
