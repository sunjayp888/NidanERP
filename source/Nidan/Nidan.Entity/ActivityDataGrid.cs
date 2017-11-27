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

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string ActivityTypeName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string ProjectName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityAssigneeGroupId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(1000)]
        public string ActivityAssigneeGroupName { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartHour { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartMinute { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndHour { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndMinute { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Key]
        [Column(Order = 16)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 19)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(100)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(2600)]
        public string SearchField { get; set; }
    }
}
