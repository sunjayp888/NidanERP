namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreVoucherNumber")]
    public partial class CentreVoucherNumber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreVoucherNumberId { get; set; }

        public int CentreId { get; set; }

        public int Number { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
