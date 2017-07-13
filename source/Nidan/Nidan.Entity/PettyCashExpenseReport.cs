namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PettyCashExpenseReport")]
    public partial class PettyCashExpenseReport
    {
        public DateTime? PettyCashCreatedDate { get; set; }

        public int? PettyCashForCentreId { get; set; }

        [StringLength(500)]
        public string PettyCashForCentreName { get; set; }

        public decimal? PettyCashAmount { get; set; }

        [StringLength(500)]
        public string PettyCashParticulars { get; set; }

        [StringLength(151)]
        public string PettyCashCreatedBy { get; set; }

        [Key]
        [Column(Order = 0)]
        public string ExpenseParticulars { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ExpenseDebitAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string ExpenseCashMemoNumbers { get; set; }

        [StringLength(500)]
        public string ExpenseVoucherNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string ExpensePaidTo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string ExpenseCentreName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(151)]
        public string ExpenseCreatedBy { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime ExpenseCreatedDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string ExpenseHeader { get; set; }
    }
}
