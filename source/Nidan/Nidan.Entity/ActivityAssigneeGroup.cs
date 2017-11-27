namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityAssigneeGroup")]
    public partial class ActivityAssigneeGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActivityAssigneeGroup()
        {
            Activities = new HashSet<Activity>();
            ActivityAssignPersonnels = new HashSet<ActivityAssignPersonnel>();
        }

        public int ActivityAssigneeGroupId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityAssignPersonnel> ActivityAssignPersonnels { get; set; }
    }
}
