namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventBrainStormingGrid")]
    public partial class EventBrainStormingGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrainstormingId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string BeforePlanningAnswerDiscussTheseQuestion { get; set; }

        public int? EventBrainstormingId { get; set; }

        public int? EventId { get; set; }

        [StringLength(10)]
        public string DisscussionCompletedYesNo { get; set; }

        public string ReferenceDetailDocument { get; set; }

        public int? CentreId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
