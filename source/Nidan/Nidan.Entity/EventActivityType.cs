namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventActivityType")]
    public partial class EventActivityType
    {
        [Key]
        public int EventActivityId { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public int? Sequence { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
