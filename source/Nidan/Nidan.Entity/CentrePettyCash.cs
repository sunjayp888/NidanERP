namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentrePettyCash")]
    public partial class CentrePettyCash
    {
        public CentrePettyCash()
        {
            CreatedDate=DateTime.UtcNow.Date;
        }
        public int CentrePettyCashId { get; set; }

        public int CentreId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(500)]
        public string Particulars { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
