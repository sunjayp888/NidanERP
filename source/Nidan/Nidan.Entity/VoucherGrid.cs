namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherGrid")]
    public partial class VoucherGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string VoucherNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string CashMemo { get; set; }

        public decimal? TotalDebitAmount { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string PaidTo { get; set; }
    }
}
