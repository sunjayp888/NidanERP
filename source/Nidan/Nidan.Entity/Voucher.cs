namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        public int VoucherId { get; set; }

        [StringLength(100)]
        public string VoucherNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string CashMemo { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        }
}
