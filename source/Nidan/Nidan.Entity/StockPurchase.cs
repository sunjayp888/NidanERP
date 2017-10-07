namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockPurchase")]
    public partial class StockPurchase
    {
        public int StockPurchaseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StockPurchaseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string InventoryItem { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Required]
        [StringLength(500)]
        public string BillNumber { get; set; }

        public decimal Amount { get; set; }

        public int StockTypeId { get; set; }

        public int? SectorId { get; set; }

        public int? StudentKitId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual StudentKit StudentKit { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual StockType StockType { get; set; }
    }
}
