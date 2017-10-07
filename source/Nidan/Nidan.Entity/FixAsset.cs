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
            CentreFixAssets = new HashSet<CentreFixAsset>();
        }

        public int FixAssetId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        [Required]
        [StringLength(500)]
        public string Supplier { get; set; }

        [Required]
        [StringLength(100)]
        public string BillNumber { get; set; }

        public decimal CostAmount { get; set; }

        public string Remarks { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentreFixAsset> CentreFixAssets { get; set; }
    }
}
