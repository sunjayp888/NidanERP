namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventPostEvent")]
    public partial class EventPostEvent
    {
        public int EventPostEventId { get; set; }

        public int EventId { get; set; }

        public int PostEventId { get; set; }

        [StringLength(50)]
        public string Completed { get; set; }

        public string RefernceDetail { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public virtual PostEvent PostEvent { get; set; }

        public virtual Event Event { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
