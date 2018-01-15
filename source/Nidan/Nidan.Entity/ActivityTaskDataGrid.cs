namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityTaskDataGrid")]
    public partial class ActivityTaskDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityTaskId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityId { get; set; }

        [StringLength(1000)]
        public string ActivityName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssignTo { get; set; }

        [StringLength(202)]
        public string AssignToName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartHour { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartMinute { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndHour { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndMinute { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Key]
        [Column(Order = 12)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        public int? NumberOfDays { get; set; }

        public int? LastTaskStateId { get; set; }

        [StringLength(500)]
        public string ActivityTaskStatus { get; set; }

        public int? NumberOfDaysDelayed { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 17)]
        public string SearchField { get; set; }
    }
}
