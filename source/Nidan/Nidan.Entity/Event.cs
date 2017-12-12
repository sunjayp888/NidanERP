namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Mobilizations = new HashSet<Mobilization>();
            EventManagements = new HashSet<EventManagement>();
            CreatedDateTime = DateTime.UtcNow.Date;
        }
        public int EventId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int? ApprovedBy { get; set; }

        public int OrganisationId { get; set; }

        public int EventApproveStateId { get; set; }

        public string Remark { get; set; }

        public int CentreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventEndDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual EventApproveState EventApproveState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mobilization> Mobilizations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventManagement> EventManagements { get; set; }
    }
}
