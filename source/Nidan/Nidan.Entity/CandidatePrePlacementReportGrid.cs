namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementReportGrid")]
    public partial class CandidatePrePlacementReportGrid
    {
        [StringLength(100)]
        public string Studentcode { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchpreplacementId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        public int? BatchId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string IsCvMaking { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string IsInterviewTechnique { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(3)]
        public string IsTechnicalKnowledge { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(3)]
        public string IsCandidateProfiling { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(3)]
        public string IsMockInterview { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
