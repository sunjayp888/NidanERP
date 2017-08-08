namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventBudget")]
    public partial class EventBudget
    {
        public int EventBudgetId { get; set; }

        public int EventId { get; set; }

        public int BudgetId { get; set; }

        [StringLength(500)]
        public string SpecificHead { get; set; }

        public decimal? EstimatedQuantity { get; set; }

        public decimal? EstimatedCostPerUnit { get; set; }

        [StringLength(10)]
        public string EstimatedSubtotal { get; set; }

        public string Notes { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Budget Budget { get; set; }

        public virtual Event Event { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
