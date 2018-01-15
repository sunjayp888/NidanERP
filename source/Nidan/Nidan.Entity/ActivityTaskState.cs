namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityTaskState")]
    public partial class ActivityTaskState
    {
        public int ActivityTaskStateId { get; set; }

        public int ActivityTaskId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CompletedDate { get; set; }

        public int TaskStateId { get; set; }

        [Required]
        public string Remark { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual ActivityTask ActivityTask { get; set; }

        public virtual TaskState TaskState { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
