namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlacementState")]
    public partial class PlacementState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlacementState()
        {
            CandidateFinalPlacements = new HashSet<CandidateFinalPlacement>();
        }

        [Key]
        public int PlacementStatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateFinalPlacement> CandidateFinalPlacements { get; set; }
    }
}
