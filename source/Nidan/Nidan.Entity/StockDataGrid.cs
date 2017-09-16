namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockDataGrid")]
    public partial class StockDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockPurchaseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string InventoryItem { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime StockPurchaseDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockTypeId { get; set; }

        [StringLength(500)]
        public string StockTypeName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        public int? TotalBalanceQuantity { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string BillNumber { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal Amount { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(1530)]
        public string SearchField { get; set; }
    }
}
