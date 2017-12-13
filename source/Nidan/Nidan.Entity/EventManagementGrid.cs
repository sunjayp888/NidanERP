namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventManagementGrid")]
    public partial class EventManagementGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventQuestionId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string QuestionDescription { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventFunctionTypeId { get; set; }

        public int? EventId { get; set; }

        public bool? EventQuestionAnswerCompleted { get; set; }

        public string Description { get; set; }

        public int? CentreId { get; set; }
    }
}
