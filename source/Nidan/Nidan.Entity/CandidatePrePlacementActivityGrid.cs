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
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidatePrePlacementActivityId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string BatchName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsSoftSkill { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsCvMaking { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool IsInterviewTechnique { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsTechnicaKnowledge { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsMockInterview { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsIndustryVisit { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool IsOjtOrLiveProject { get; set; }

        [Key]
        [Column(Order = 11)]
        public bool IsCandidateProfiling { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
