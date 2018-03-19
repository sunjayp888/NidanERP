namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchPrePlacementSearchField")]
    public partial class BatchPrePlacementSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchPrePlacementId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [StringLength(500)]
        public string BatchName { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime ScheduledStartDate { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime ScheduledEndDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        public string Remark { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(1770)]
        public string SearchField { get; set; }
    }
}
