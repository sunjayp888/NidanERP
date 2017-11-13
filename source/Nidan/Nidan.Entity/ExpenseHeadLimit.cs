namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpenseHeadLimit")]
    public partial class ExpenseHeadLimit
    {
        public int ExpenseHeadLimitId { get; set; }

        public int ExpenseHeaderId { get; set; }

        public int CentreId { get; set; }

        public decimal LimitAmount { get; set; }

        public int OrganisationId { get; set; }

        public int ExpenseMonth { get; set; }

        public int ExpenseYear { get; set; }
    }
}
