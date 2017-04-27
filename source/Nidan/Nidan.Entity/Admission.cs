namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admission")]
    public partial class Admission
    {
        public Admission()
        {
            CandidateInstallments=new HashSet<CandidateInstallment>();
        }

        public int AdmissionId { get; set; }

        public int RegistrationPaymentReceiptId { get; set; }

        public int EnquiryId { get; set; }

        public int BatchId { get; set; }

        public int CentreId { get; set; }

        public int? Fee { get; set; }

        public int? DownPayment { get; set; }

        public int? Lumpsum { get; set; }

        public int? Discount { get; set; }

        public int? DiscountFee { get; set; }

        public int? FeeByStudent { get; set; }

        [StringLength(500)]
        public string Particulars { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymentType { get; set; }

        public int PaymentModeId { get; set; }

        [Required]
        [StringLength(100)]
        public string ChequeNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime ChequeDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string FinancialYear { get; set; }

        [Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual RegistrationPaymentReceipt RegistrationPaymentReceipt { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateInstallment> CandidateInstallments { get; set; }
    }
}
