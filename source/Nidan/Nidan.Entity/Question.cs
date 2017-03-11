namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public int QuestionId { get; set; }

        [Required]
        public string Description { get; set; }

        public int EventFunctionTypeId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual EventFunctionType EventFunctionType { get; set; }
    }
}
