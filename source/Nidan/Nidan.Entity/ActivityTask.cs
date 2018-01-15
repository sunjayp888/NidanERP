namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityTask")]
    public partial class ActivityTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActivityTask()
        {
            ActivityTaskStates=new HashSet<ActivityTaskState>();
            CreatedDate = DateTime.UtcNow.Date;
        }

        public int ActivityTaskId { get; set; }

        public int ActivityId { get; set; }

        [Required]
        public string Name { get; set; }

        public int AssignTo { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        public int StartHour { get; set; }

        public int StartMinute { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int EndHour { get; set; }

        public int EndMinute { get; set; }

        [Required]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        [Required]
        public string Remark { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityTaskState> ActivityTaskStates { get; set; }
    }
}
