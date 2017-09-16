namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreFixAsset")]
    public partial class CentreFixAsset
    {
        public int CentreFixAssetId { get; set; }

        public int FixAssetId { get; set; }

        public int? RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateofPutToUse { get; set; }

        [StringLength(100)]
        public string AssetCode { get; set; }

        public string Remarks { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual FixAsset FixAsset { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Room Room { get; set; }
    }
}
