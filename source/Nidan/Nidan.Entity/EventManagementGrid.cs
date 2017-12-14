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
        public int? EventManagementId { get; set; }

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

        public string EventFunctionTypeName { get; set; }

        public int? EventId { get; set; }

        [StringLength(200)]
        public string EventName { get; set; }

        public bool? EventQuestionAnswerCompleted { get; set; }

        public string Description { get; set; }

        public int? CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        public int? Createdby { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public int? OrganisationId { get; set; }
    }
}
