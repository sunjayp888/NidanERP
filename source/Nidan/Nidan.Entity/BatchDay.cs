namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchDay")]
    public partial class BatchDay
    {
        public int BatchDayId { get; set; }

        public int BatchId { get; set; }

        public bool? IsMonday { get; set; }

        public bool? IsTuesday { get; set; }

        public bool? IsWednusday { get; set; }

        public bool? IsThusday { get; set; }

        public bool? IsFriday { get; set; }

        public bool? IsSaturday { get; set; }

        public bool? IsSunday { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Batch> Batches { get; set; }
    }
}
