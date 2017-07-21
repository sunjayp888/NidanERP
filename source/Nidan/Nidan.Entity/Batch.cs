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
        public Batch()
        {
            BatchDays = new HashSet<BatchDay>();
            BatchTrainers = new HashSet<BatchTrainer>();
            Admissions = new HashSet<Admission>();
            RoomAvailables=new HashSet<RoomAvailable>();
            TrainerAvailables=new HashSet<TrainerAvailable>();
            BatchAttendances = new HashSet<BatchAttendance>();
            CreatedDate=DateTime.UtcNow.Date;
        }

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

        public int NumberOfHolidays { get; set; }

        public int NumberOfHoursDaily { get; set; }

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
        public DateTime AssessmentDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        public int Month { get; set; }

        public int NumberOfInstallment { get; set; }

        public int? FirstInstallment { get; set; }

        public int? SecondInstallment { get; set; }

        public int? ThirdInstallment { get; set; }

        public int? FourthInstallment { get; set; }

        public int? FifthInstallment { get; set; }

        public int? SixthInstallment { get; set; }

        public int? SeventhInstallment { get; set; }

        public int? EighthInstallment { get; set; }

        public int? NinethInstallment { get; set; }

        public int? TenthInstallment { get; set; }

        public int? EleventhInstallment { get; set; }

        public int? TwelvethInstallment { get; set; }

        public int RoomId { get; set; }

        public virtual CourseInstallment CourseInstallment { get; set; }

        public virtual Course Course { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Room Room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchDay> BatchDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchTrainer> BatchTrainers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomAvailable> RoomAvailables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainerAvailable> TrainerAvailables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchAttendance> BatchAttendances { get; set; }
    }
}
