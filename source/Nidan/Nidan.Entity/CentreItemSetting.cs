namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreItemSetting")]
    public partial class CentreItemSetting
    {
        public int CentreItemSettingId { get; set; }

        public int CentreId { get; set; }

        public int ItemId { get; set; }

        public int ItemNumber { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Item Item { get; set; }
    }
}
