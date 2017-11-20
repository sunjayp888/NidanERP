namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixAssetDetailGrid")]
    public partial class FixAssetDetailGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FixAssetMappingId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssetClassId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string AssetClassName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string ItemName { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime DateofPurchase { get; set; }

        [StringLength(207)]
        public string AssetCodeAsPerTally { get; set; }

        [StringLength(100)]
        public string AssetCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssetOutDate { get; set; }

        [StringLength(100)]
        public string AssetOutOwner { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssetOutStatusId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string AssetOutStatusName { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime StatusDate { get; set; }

        public decimal? StatusCost { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
