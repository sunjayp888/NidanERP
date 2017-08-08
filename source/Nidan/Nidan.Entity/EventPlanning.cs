namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventPlanning")]
    public partial class EventPlanning
    {
        public int EventPlanningId { get; set; }

        public int EventId { get; set; }

        public int PlanningId { get; set; }

        public string Input { get; set; }

        public string Description { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Planning Planning { get; set; }

        public virtual Event Event { get; set; }
    }
}
