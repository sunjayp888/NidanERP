namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateFeeSearchField")]
    public partial class CandidateFeeSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateFeeId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime PaymentDate { get; set; }

        public int? CandidateInstallmentId { get; set; }

        public decimal? PaidAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeId { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public decimal? Penalty { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime InstallmentDate { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public decimal? InstallmentAmount { get; set; }

        public decimal? BalanceInstallmentAmount { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string FiscalYear { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        public string Particulars { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(300)]
        public string SearchField { get; set; }
    }
}
