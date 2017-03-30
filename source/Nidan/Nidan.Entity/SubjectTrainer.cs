namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubjectTrainer")]
    public partial class SubjectTrainer
    {
        public int SubjectTrainerId { get; set; }

        public int SubjectId { get; set; }

        public int TrainerId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
