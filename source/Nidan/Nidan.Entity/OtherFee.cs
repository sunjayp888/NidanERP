namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherFee")]
    public partial class OtherFee
    {
        public OtherFee()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int OtherFeeId { get; set; }

        public int FeeTypeId { get; set; }

        public int OnlineExamId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? EnquiryId { get; set; }

        [Required]
        [StringLength(500)]
        public string StudentCode { get; set; }

        public decimal PaidAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        [StringLength(1000)]
        public string RupeesInWords { get; set; }

        public string Remark { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(500)]
        public string ReceiptNumber { get; set; }

        public int PaymentModeId { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual OnlineExam OnlineExam { get; set; }

        public virtual FeeType FeeType { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }
    }
}
