namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateInstallment")]
    public partial class CandidateInstallment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidateInstallment()
        {
            CandidateFees = new HashSet<CandidateFee>();
            Registrations=new HashSet<Registration>();
            CreatedDate = DateTime.UtcNow.Date;
        }

        public int CandidateInstallmentId { get; set; }

        public int CourseInstallmentId { get; set; }

        public decimal? CourseFee { get; set; }

        public decimal? DownPayment { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int? NumberOfInstallment { get; set; }

        public decimal? LumpsumAmount { get; set; }

        public string StudentCode { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual CourseInstallment CourseInstallment { get; set; }

        public bool IsPercentageDiscount { get; set; }

        public string PaymentMethod { get; set; }

        public bool IsTotalAmountDiscount { get; set; }

        public DateTime CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateFee> CandidateFees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
