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
            Brainstormings = new HashSet<Brainstorming>();
            Plannings = new HashSet<Planning>();
            Budgets = new HashSet<Budget>();
            Eventdays = new HashSet<Eventday>();
            PostEvents = new HashSet<PostEvent>();
            EventBrainstormings = new HashSet<EventBrainstorming>();
            CreatedDateTime=DateTime.UtcNow.Date;
            EventBudgets = new HashSet<EventBudget>();
            EventPlannings = new HashSet<EventPlanning>();
            EventPostEvents = new HashSet<EventPostEvent>();
        }
        public int EventId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int? ApprovedBy { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventEndDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mobilization> Mobilizations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Brainstorming> Brainstormings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Planning> Plannings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Budget> Budgets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eventday> Eventdays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostEvent> PostEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventBrainstorming> EventBrainstormings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventBudget> EventBudgets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventPlanning> EventPlannings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventPostEvent> EventPostEvents { get; set; }

    }
}
