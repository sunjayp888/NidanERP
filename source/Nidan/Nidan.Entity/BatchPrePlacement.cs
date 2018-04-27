namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchPrePlacement")]
    public partial class BatchPrePlacement
    {
        public BatchPrePlacement()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int BatchPrePlacementId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int BatchId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScheduledStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScheduledEndDate { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public string Remark { get; set; }

        public int OrganisationId { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

    }
}
