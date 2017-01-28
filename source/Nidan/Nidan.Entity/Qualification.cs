namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Qualification")]
    public partial class Qualification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Qualification()
        {
            Enquiries = new HashSet<Enquiry>();
        }

        public int QualificationId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enquiry> Enquiries { get; set; }
    }
}
