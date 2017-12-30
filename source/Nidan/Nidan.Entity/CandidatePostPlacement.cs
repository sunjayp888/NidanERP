namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePostPlacement")]
    public partial class CandidatePostPlacement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidatePostPlacement()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CandidatePostPlacementId { get; set; }

        public int AdmissionId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public int BatchId { get; set; }

        public int CompanyId { get; set; }

        public int CompanyBranchId { get; set; }

        [Required]
        public string Feedback { get; set; }

        public int CentreId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Company Company { get; set; }

        public virtual CompanyBranch CompanyBranch { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Admission Admission { get; set; }
    }
}
