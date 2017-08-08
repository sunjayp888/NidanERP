namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateFee")]
    public partial class CandidateFee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidateFee()
        {
            Registrations = new HashSet<Registration>();
            PaymentDate=DateTime.UtcNow.Date;
        }
        public int CandidateFeeId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? CandidateInstallmentId { get; set; }

        public decimal? PaidAmount { get; set; }

        public int PaymentModeId { get; set; }

        public int FeeTypeId { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public decimal? Penalty { get; set; }

        public DateTime? InstallmentDate { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public int? InstallmentNumber { get; set; }

        public decimal? InstallmentAmount { get; set; }

        public decimal? BalanceInstallmentAmount { get; set; }

        public string FiscalYear { get; set; }

        public bool IsPaymentDone { get; set; }

        public int? PersonnelId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public string Particulars { get; set; }

        public string ReferenceReceiptNumber { get; set; }

        public bool IsPaidAmountOverride { get; set; }

        public bool HaveReceipt { get; set; }

        public decimal? AdvancedAmount { get; set; }

        public string ReceiptNumber { get; set; }

        public virtual CandidateInstallment CandidateInstallment { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual Personnel Personnel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
