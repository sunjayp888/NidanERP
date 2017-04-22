namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrationPaymentReceipt")]
    public partial class RegistrationPaymentReceipt
    {
        public RegistrationPaymentReceipt()
        {
            FollowUps= new HashSet<FollowUp>();
            Admissions=new HashSet<Admission>();
        }

        public int RegistrationPaymentReceiptId { get; set; }

        public int CentreId { get; set; }

        public int EnquiryId { get; set; }

        public int CounsellingId { get; set; }

        public int CourseId { get; set; }

        public int Fees { get; set; }

        [Required]
        [StringLength(100)]
        public string ChequeNo { get; set; } = "Paid By Cash";

        [Column(TypeName = "date")]
        public DateTime ChequeDate { get; set; }

        [Required]
        [StringLength(500)]
        public string BankName { get; set; } = "Paid By Cash";

        [Required]
        [StringLength(500)]
        public string Particulars { get; set; }

        public string Remarks { get; set; }

        public int PaymentModeId { get; set; }

        public int OrganisationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Course Course { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Counselling Counselling { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowUp> FollowUps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }
    }
}
