namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixAssetDataGrid")]
    public partial class FixAssetDataGrid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreFixAssetId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string AssetCode { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateofPurchase { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateofPutToUse { get; set; }

        [StringLength(500)]
        public string Supplier { get; set; }

        [StringLength(100)]
        public string BillNumber { get; set; }

        public int? CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        public int? OrganisationId { get; set; }
    }
}
