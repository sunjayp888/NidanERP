namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Planning")]
    public partial class Planning
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Planning()
        {
            EventPlannings = new HashSet<EventPlanning>();
        }

        public int PlanningId { get; set; }

        [Required]
        [StringLength(500)]
        public string MajorPoint { get; set; }

        [Required]
        public string Point { get; set; }

        public int OrganisationId { get; set; }

        public int? CentreId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventPlanning> EventPlannings { get; set; }
    }
}
