namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CasteCategory")]
    public partial class CasteCategory
    {
        public CasteCategory()
        {
            Enquiries = new HashSet<Enquiry>();
            GovernmentMobilizations = new HashSet<GovernmentMobilization>();
        }
        public int CasteCategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string Caste { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enquiry> Enquiries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GovernmentMobilization> GovernmentMobilizations { get; set; }
    }
}
