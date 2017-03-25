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

        [Required]
        [StringLength(100)]
        public string BatchType { get; set; } = "Non-CSR";

        public int SectorId { get; set; }

        public int CourseId { get; set; }

        public int? NoOfDays { get; set; }

        public int NoOfHrs { get; set; }

        public int? NoOfHolidays { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime BatchEndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime PreferredAssesmentDate { get; set; }

        [StringLength(100)]
        public string BatchTime { get; set; }

        public int TrainerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}
