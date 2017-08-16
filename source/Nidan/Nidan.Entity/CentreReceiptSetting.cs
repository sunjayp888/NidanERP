namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreReceiptSetting")]
    public partial class CentreReceiptSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreReceiptSettingId { get; set; }

        public int ReceiptNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TaxYear { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
