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
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FixAssetMappingId { get; set; }

        public int? FixAssetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemSettingId { get; set; }

        [StringLength(100)]
        public string AssetCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssetClassId { get; set; }

        public int? RoomId { get; set; }

        public string RoomName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string AssetClassName { get; set; }

        public int? ItemId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(100)]
        public string InvoiceNumber { get; set; }

        [StringLength(100)]
        public string PurchaseFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateofPurchase { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal CostPerAsset { get; set; }

        public int? AssignTypeId { get; set; }

        [StringLength(100)]
        public string AssignTypeName { get; set; }

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
        public string AssignOutStatusName { get; set; }

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
