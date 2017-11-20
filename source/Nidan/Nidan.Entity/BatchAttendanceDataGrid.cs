namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchAttendanceDataGrid")]
    public partial class BatchAttendanceDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttendanceId { get; set; }

        public int? PersonnelId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

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

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(50)]
        public string BioMetricLogTime { get; set; }

        [StringLength(2)]
        public string Direction { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string BatchName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string SubjectName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SessionId { get; set; }

        [Key]
        [Column(Order = 9)]
        public string SessionName { get; set; }

        [StringLength(500)]
        public string Topic { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(151)]
        public string AttendanceBy { get; set; }

        [StringLength(100)]
        public string CentreName { get; set; }
    }
}
