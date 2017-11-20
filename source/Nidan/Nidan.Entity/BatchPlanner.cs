namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchPlanner")]
    public partial class BatchPlanner
    {
        public int BatchPlannerId { get; set; }

        public int RoomId { get; set; }

        public int StartTimeHours { get; set; }

        public int StartTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTimeSpan { get; set; } = "AM";

        public int EndTimeHours { get; set; }

        public int EndTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string EndTimeSpan { get; set; } = "AM";

        [Required]
        [StringLength(50)]
        public string SlotType { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseType { get; set; }

        public int CourseId { get; set; }

        public int MaximumCandidate { get; set; }

        public int MinimumCandidate { get; set; }

        public decimal MonthlyInstallment { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchEndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime AssessmentDate { get; set; }

        public int InstallmentDuration { get; set; }

        public int DurationOfBatchSlot { get; set; }

        public int NumberOfBatch { get; set; }

        public int NumberOfCandidateEngaged { get; set; }

        public decimal MinimumRevenue { get; set; }

        public int TrainerId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Room Room { get; set; }

        public virtual Course Course { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchPlannerDay> BatchPlannerDays { get; set; }
    }
}
