namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssessmentGrid")]
    public partial class AssessmentGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string AssessmentName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentTypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string AssessmentTypeName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime AssessmentDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartTimeHour { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartTimeMinute { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndTimeHour { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndTimeMinute { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [StringLength(72)]
        public string AssessmentTime { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(1000)]
        public string CourseName { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(500)]
        public string BatchName { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(202)]
        public string CreatedByName { get; set; }
    }
}
