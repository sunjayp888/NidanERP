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
        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string BillNumber { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal CostAmount { get; set; }

        [Key]
        [Column(Order = 6)]
        public string Room { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime DateofPutToUse { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(500)]
        public string AssetCode { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(1630)]
        public string SearchField { get; set; }
    }
}
