namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brainstorming")]
    public partial class Brainstorming
    {
        public int BrainstormingId { get; set; }

        [Required]
        public string BeforePlanningAnswerDiscussTheseQuestion { get; set; }

        public int OrganisationId { get; set; }

        public int? CentreId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventBrainstorming> EventBrainstormings { get; set; }
    }
}
