namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpenseDataGrid")]
    public partial class ExpenseDataGrid
    {
        [Key]
        [Column(Order = 0)]
        public int ExpenseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [StringLength(500)]
        public string VoucherNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string ExpenseHeader { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string CashMemoNumbers { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal DebitAmount { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string PaidTo { get; set; }

        [Key]
        [Column(Order = 7)]
        public string Particulars { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(151)]
        public string CreatedBy { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }
    }
}
