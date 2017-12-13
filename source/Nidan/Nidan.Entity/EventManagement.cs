namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventManagement")]
    public partial class EventManagement
    {
        public int EventManagementId { get; set; }

        public int EventQuestionId { get; set; }

        public int EventId { get; set; }

        public int EventFunctionTypeId { get; set; }

        public bool EventQuestionAnswerCompleted { get; set; }

        [Required]
        public string Description { get; set; }

        public int? Createdby { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual EventQuestion EventQuestion { get; set; }

        public virtual Event Event { get; set; }

        public virtual EventFunctionType EventFunctionType { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
