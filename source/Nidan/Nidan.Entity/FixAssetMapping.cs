namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FixAssetMapping")]
    public partial class FixAssetMapping
    {
        public FixAssetMapping()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }

        public int FixAssetMappingId { get; set; }

        public int FixAssetId { get; set; }

        public int ItemSettingId { get; set; }

        public decimal CostPerAsset { get; set; }

        [StringLength(100)]
        public string AssetCode { get; set; }

        public int? AssignTypeId { get; set; }

        public int? RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssetOutDate { get; set; }

        [StringLength(100)]
        public string AssetOutOwner { get; set; }

        public int AssetOutStatusId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StatusDate { get; set; }

        public decimal? StatusCost { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public virtual AssetOutState AssetOutState { get; set; }

        public virtual FixAsset FixAsset { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Room Room { get; set; }

        public virtual AssignType AssignType { get; set; }


    }
}
