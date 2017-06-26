namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpenseProject")]
    public partial class ExpenseProject
    {
        public int ExpenseProjectId { get; set; }

        public int ExpenseId { get; set; }

        public int ProjectId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Expense Expense { get; set; }

        public virtual Project Project { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
