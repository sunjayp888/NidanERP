namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventQuestion")]
    public partial class EventQuestion
    {
        public EventQuestion()
        {
            EventManagements = new HashSet<EventManagement>();
        }
        public int EventQuestionId { get; set; }

        [Required]
        public string QuestionDescription { get; set; }

        public int EventFunctionTypeId { get; set; }

        public int OrganisationId { get; set; }

        public virtual EventFunctionType EventFunctionType { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventManagement> EventManagements { get; set; }
    }
}
