namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityTaskStateDataGrid")]
    public partial class ActivityTaskStateDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityTaskStateId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1000)]
        public string ActivityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityTaskId { get; set; }

        [Key]
        [Column(Order = 4)]
        public string ActivityTaskName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string TaskPriority { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime ActivityTaskStateDate { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskStateId { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(500)]
        public string TaskStateName { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        public string Problem { get; set; }

        public string Solution { get; set; }

        public int? NumberOfHours { get; set; }

        public int? NumberOfMinutes { get; set; }

        [Key]
        [Column(Order = 13)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(202)]
        public string AssignToName { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(256)]
        public string AssignToEmail { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(256)]
        public string CreatedByEmail { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityAssigneeGroupId { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(256)]
        public string HeadEmail { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(256)]
        public string MonitoredByEmail { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(1000)]
        public string ActivityAssigneeGroupName { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(202)]
        public string HeadName { get; set; }
    }
}
