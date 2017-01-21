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

        public decimal? NumberOfAttendees { get; set; }

        public decimal? EventCost { get; set; }

        public decimal? EventPricePerPerson { get; set; }

        public decimal? EstimatedMarketingGrandTotal { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Total { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
