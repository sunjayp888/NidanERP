namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixAssetSearchGrid")]
    public partial class FixAssetSearchGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FixAssetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        public int? BalanceQuantity { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string BillNumber { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal CostAmount { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(850)]
        public string SearchField { get; set; }
    }
}
