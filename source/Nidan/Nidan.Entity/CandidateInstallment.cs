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
        }

        public int CandidateInstallmentId { get; set; }

        public int AdmissionId { get; set; }

        public int BatchCourseFee { get; set; }

        public int CandidateCourseFee { get; set; }

        public int DownPayment { get; set; }

        public int DiscountPercentage { get; set; }

        public int DiscountAmount { get; set; }

        public int NumberOfInstallment { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateFee> CandidateFees { get; set; }
    }
}
