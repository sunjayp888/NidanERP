namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchPlannerGrid")]
    public partial class BatchPlannerGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchPlannerId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ClassRoomName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartTimeHours { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartTimeMinutes { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndTimeHours { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndTimeMinutes { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string SlotType { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string CourseType { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(1000)]
        public string Course { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaximumCandidate { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MinimumCandidate { get; set; }

        [Key]
        [Column(Order = 13)]
        public decimal MonthlyInstallment { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "date")]
        public DateTime BatchStartDate { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "date")]
        public DateTime BatchEndDate { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "date")]
        public DateTime AssessmentDate { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstallmentDuration { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DurationOfBatchSlot { get; set; }

        [Key]
        [Column(Order = 19)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfBatch { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfCandidateEngaged { get; set; }

        [Key]
        [Column(Order = 21)]
        public decimal MinimumRevenue { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(354)]
        public string Trainer { get; set; }

        [Key]
        [Column(Order = 23)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 24)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
