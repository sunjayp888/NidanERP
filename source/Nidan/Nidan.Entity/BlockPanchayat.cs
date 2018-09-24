namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlockPanchayat")]
    public partial class BlockPanchayat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BlockPanchayat()
        {
            GovernmentMobilizations = new HashSet<GovernmentMobilization>();
        }

        public int BlockPanchayatId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int DistrictBlockId { get; set; }

        public int OrganisationId { get; set; }

        public virtual DistrictBlock DistrictBlock { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GovernmentMobilization> GovernmentMobilizations { get; set; }
    }
}
