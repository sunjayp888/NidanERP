namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventFunctionType")]
    public partial class EventFunctionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventFunctionType()
        {
            EventQuestions = new HashSet<EventQuestion>();
            EventManagements=new HashSet<EventManagement>();
        }

        public int EventFunctionTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventQuestion> EventQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventManagement> EventManagements { get; set; }
    }
}
