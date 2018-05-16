namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementGrid")]
    public partial class CandidatePrePlacementGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidatePrePlacementId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchPrePlacementId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string BatchPrePlacementName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrePlacementActivityId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string PrePlacementActivityName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string BatchName { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime ScheduledStartDate { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime ScheduledEndDate { get; set; }

        public string Remark { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
