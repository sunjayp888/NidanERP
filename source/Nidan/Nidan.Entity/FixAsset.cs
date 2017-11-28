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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FixAsset()
        {
            FixAssetMappings = new HashSet<FixAssetMapping>();
            CreatedDate = DateTime.UtcNow.Date;
        }

        public int FixAssetId { get; set; }

        public int AssetClassId { get; set; }

        public int ItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string InvoiceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        public decimal Cost { get; set; }

        [Required]
        [StringLength(100)]
        public string PurchaseFrom { get; set; }

        public int Quantity { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string Remark { get; set; }

        public virtual AssetClass AssetClass { get; set; }

        public virtual Item Item { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixAssetMapping> FixAssetMappings { get; set; }
    }
}
