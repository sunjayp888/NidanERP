namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreProductSetting")]
    public partial class CentreProductSetting
    {
        public int CentreProductSettingId { get; set; }

        public int CentreId { get; set; }

        public int ProductId { get; set; }

        public int Code { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Product Product { get; set; }
    }
}
