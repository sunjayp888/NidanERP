namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchPlannerDay")]
    public partial class BatchPlannerDay
    {
        public int BatchPlannerDayId { get; set; }

        public int BatchPlannerId { get; set; }

        public bool IsMonday { get; set; }

        public bool IsTuesday { get; set; }

        public bool IsWednesday { get; set; }

        public bool IsThursday { get; set; }

        public bool IsFriday { get; set; }

        public bool IsSaturday { get; set; }

        public bool IsSunday { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual BatchPlanner BatchPlanner { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
