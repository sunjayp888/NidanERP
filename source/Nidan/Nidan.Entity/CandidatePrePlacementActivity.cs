namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementActivity")]
    public partial class CandidatePrePlacementActivity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidatePrePlacementActivity()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CandidatePrePlacementActivityId { get; set; }

        public int BatchId { get; set; }

        public int AdmissionId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public bool IsSoftSkill { get; set; }

        public bool IsCvMaking { get; set; }

        public bool IsInterviewTechnique { get; set; }

        public bool IsTechnicalKnowledge { get; set; }

        public bool IsMockInterview { get; set; }

        public bool IsIndustryVisit { get; set; }

        public bool IsOjtOrLiveProject { get; set; }

        public bool IsCandidateProfiling { get; set; }

        public int CentreId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Admission Admission { get; set; }
    }
}
