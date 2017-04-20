namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Batch")]
    public partial class Batch
    {
        public int BatchId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int Intake { get; set; }

        public int CourseInstallmentId { get; set; }

        public int CourseId { get; set; }

        public int TrainerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchEndDate { get; set; }

        public int NoOfHolidays { get; set; }

        public int NoOfHoursDaily { get; set; }

        public int BatchStartTimeHours { get; set; }

        public int BatchStartTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string BatchStartTimeSpan { get; set; } = "AM";

        public int BatchEndTimeHours { get; set; }

        public int BatchEndTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string BatchEndTimeSpan { get; set; } = "AM";

        [Column(TypeName = "date")]
        public DateTime AssesmentDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        public int Month { get; set; }

        public int NoOfInstallment { get; set; }

        public int? FirstInstallment { get; set; }

        public int? SecondInstallment { get; set; }

        public int? ThirdInstallment { get; set; }

        public int? ForthInstallment { get; set; }

        public int? FifthInstallment { get; set; }

        public int? SixthInstallment { get; set; }

        public int? SeventhInstallment { get; set; }

        public int? EighthInstallment { get; set; }

        public int? NinethInstallment { get; set; }

        public int? TenthInstallment { get; set; }

        public int? EleventhInstallment { get; set; }

        public int? TwelvethInstallment { get; set; }

        public virtual CourseInstallment CourseInstallment { get; set; }

        public virtual Course Course { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchDay> BatchDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchTrainer> BatchTrainers { get; set; }

    }
}
