namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityDataGrid")]
    public partial class ActivityDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityTypeId { get; set; }

        [StringLength(500)]
        public string ActivityTypeName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        [StringLength(100)]
        public string ProjectName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityAssigneeGroupId { get; set; }

        [StringLength(1000)]
        public string ActivityAssigneeGroupName { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartHour { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartMinute { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndHour { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndMinute { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Key]
        [Column(Order = 13)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(100)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int? NumberOfDays { get; set; }

        public int? NumberOfTask { get; set; }

        public int? NumberOfTaskAssignees { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskStateId { get; set; }

        [StringLength(500)]
        public string ActivityStatus { get; set; }

        public int? NumberOfDaysDelayed { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(2600)]
        public string SearchField { get; set; }
    }
}
