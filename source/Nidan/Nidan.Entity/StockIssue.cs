namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockIssue")]
    public partial class StockIssue
    {
        [Key]
        public int StockIssuedId { get; set; }

        public int StockPurchaseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime IssuedDate { get; set; }

        public int IssuedQuantity { get; set; }

        [Required]
        [StringLength(500)]
        public string IssuedToPerson { get; set; }

        public int? BalanceQuantity { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual StockPurchase StockPurchase { get; set; }
    }
}
