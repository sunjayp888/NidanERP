namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateAssessmentGrid")]
    public partial class CandidateAssessmentGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        public int? BatchId { get; set; }

        [StringLength(500)]
        public string BatchName { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        public int? PersonnelId { get; set; }

        public int? CandidateAssessmentId { get; set; }

        [StringLength(100)]
        public string AssessmentName { get; set; }

        public int? AssessmentTypeId { get; set; }

        [StringLength(50)]
        public string AssessmentTypeName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssessmentDate { get; set; }

        [StringLength(72)]
        public string AssessmentTime { get; set; }

        public int? ModuleExamSetId { get; set; }

        [StringLength(100)]
        public string ExamSet { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string IsAssignExamSet { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
