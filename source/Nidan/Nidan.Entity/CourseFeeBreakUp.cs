namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseFeeBreakUp")]
    public partial class CourseFeeBreakUp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseFeeBreakUp()
        {
            CourseInstallments = new HashSet<CourseInstallment>();
            Batches=new HashSet<Batch>();
        }

        public int CourseFeeBreakUpId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseInstallment> CourseInstallments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Batch> Batches { get; set; }
    }
}
