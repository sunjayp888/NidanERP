namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankDepositeCentreReport")]
    public partial class BankDepositeCentreReport
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime DepositedDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string CentreName { get; set; }

        public decimal? TotalBankDeposite { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
