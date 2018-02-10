namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpenseHeaderGrid")]
    public partial class ExpenseHeaderGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExpenseHeaderId { get; set; }

        [StringLength(500)]
        public string ExpenseHeaderName { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime ExpenseGeneratedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal DebitAmount { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string CentreName { get; set; }
    }
}
