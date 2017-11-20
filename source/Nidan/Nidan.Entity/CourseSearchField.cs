namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseSearchField")]
    public partial class CourseSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorId { get; set; }

        public int? SchemeId { get; set; }

        [StringLength(50)]
        public string CourseType { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? Duration { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(2000)]
        public string SearchField { get; set; }
    }
}
