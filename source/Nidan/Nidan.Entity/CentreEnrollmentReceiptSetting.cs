namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreEnrollmentReceiptSetting")]
    public partial class CentreEnrollmentReceiptSetting
    {
        public int CentreEnrollmentReceiptSettingId { get; set; }

        public int EnrollmentNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TaxYear { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
