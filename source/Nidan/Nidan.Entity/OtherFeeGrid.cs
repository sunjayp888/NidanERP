namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherFeeGrid")]
    public partial class OtherFeeGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OtherFeeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string FeeTypeName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OnlineExamId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string ExamName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? EnquiryId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string StudentCode { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string PaymentModeName { get; set; }

        [StringLength(1000)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal PaidAmount { get; set; }

        [StringLength(500)]
        public string ReceiptNumber { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        [StringLength(1000)]
        public string RupeesInWords { get; set; }

        public string Remark { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
