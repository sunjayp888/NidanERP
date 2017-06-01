namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrainerAvailable")]
    public partial class TrainerAvailable
    {
        public int TrainerAvailableId { get; set; }

        public int TrainerId { get; set; }

        public int BatchId { get; set; }

        [Required]
        [StringLength(10)]
        public string Day { get; set; }

        public int StartTimeHours { get; set; }

        public int StartTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        public int EndTimeHours { get; set; }

        public int EndTimeMinutes { get; set; }

        [Required]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
