namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DistrictBlock")]
    public partial class DistrictBlock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DistrictBlock()
        {
            BlockPanchayats = new HashSet<BlockPanchayat>();
            GovernmentMobilizations = new HashSet<GovernmentMobilization>();
        }

        public int DistrictBlockId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int DistrictId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlockPanchayat> BlockPanchayats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GovernmentMobilization> GovernmentMobilizations { get; set; }
    }
}
