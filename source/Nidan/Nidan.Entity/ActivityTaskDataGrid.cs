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

        [Key]
        [Column(Order = 2)]
        [StringLength(1000)]
        public string ActivityName { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssignTo { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(202)]
        public string AssignToName { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartHour { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StartMinute { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndHour { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EndMinute { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Key]
        [Column(Order = 14)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 19, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 20)]
        public string SearchField { get; set; }
    }
}
