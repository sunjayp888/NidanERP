namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventBrainstorming")]
    public partial class EventBrainstorming
    {
        public int EventBrainstormingId { get; set; }

        public int EventId { get; set; }

        public int BrainstormingId { get; set; }

        [Required]
        [StringLength(10)]
        public string DisscussionCompletedYesNo { get; set; }

        [DataType(DataType.MultilineText)]
        public string ReferenceDetailDocument { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Event Event { get; set; }

        public virtual Brainstorming Brainstorming { get; set; }
    }
}
