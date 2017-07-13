namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchTrainer")]
    public partial class BatchTrainer
    {
        public int BatchTrainerId { get; set; }

        public int BatchId { get; set; }

        public int TrainerId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchAttendance> BatchAttendances { get; set; }
    }
}
