using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("MobilizationSearchField")]
    public partial class MobilizationSearchField
    {
        [Key]
        [Column(Order = 0)]
        public int MobilizationId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int EventId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 4)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(6)]
        public string Mobile { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string InterestedCourse { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string Qualification { get; set; }
    }
}
