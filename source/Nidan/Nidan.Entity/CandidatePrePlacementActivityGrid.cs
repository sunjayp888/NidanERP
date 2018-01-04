namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementActivityGrid")]
    public partial class CandidatePrePlacementActivityGrid
    {
        public int? CandidatePrePlacementActivityId { get; set; }

        public int? BatchId { get; set; }

        [StringLength(500)]
        public string BatchName { get; set; }

        public int? AdmissionId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        public bool? IsSoftSkill { get; set; }

        public bool? IsCvMaking { get; set; }

        public bool? IsInterviewTechnique { get; set; }

        public bool? IsTechnicalKnowledge { get; set; }

        public bool? IsMockInterview { get; set; }

        public bool? IsIndustryVisit { get; set; }

        public bool? IsOjtOrLiveProject { get; set; }

        public bool? IsCandidateProfiling { get; set; }

        public int? CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? OrganisationId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string IsPostPlacementDone { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(700)]
        public string SearchField { get; set; }
    }
}
