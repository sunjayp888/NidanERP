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
        public int BatchId { get; set; }

        public int SchemeId { get; set; }

        public int? CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(500)]
        public string TrainingType { get; set; }

        public int SectorId { get; set; }

        [StringLength(500)]
        public string SubSector { get; set; }

        public int CourseId { get; set; }

        public int TrainingHrsPerDay { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchEndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime PrefferdAssesmentDate { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
