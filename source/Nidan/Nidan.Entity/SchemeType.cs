namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SchemeType")]
    public partial class SchemeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeType()
        {
            Schemes = new HashSet<Scheme>();
        }

        public int SchemeTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scheme> Schemes { get; set; }
    }
}