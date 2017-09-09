namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixAsset")]
    public partial class FixAsset
    {
        public int FixAssetId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        [Required]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Required]
        [StringLength(100)]
        public string BillNumber { get; set; }

        public decimal CostAmount { get; set; }

        public int RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofPutToUse { get; set; }

        [Required]
        [StringLength(500)]
        public string AssetCode { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Room Room { get; set; }
    }
}
