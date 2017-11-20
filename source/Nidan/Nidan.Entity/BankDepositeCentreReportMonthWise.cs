namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankDepositeCentreReportMonthWise")]
    public partial class BankDepositeCentreReportMonthWise
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        [StringLength(30)]
        public string MonthName { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        public decimal? TotalBankDepositeAmount { get; set; }
    }
}
