namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateAssesment")]
    public partial class CandidateAssesment
    {
        public CandidateAssesment()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CandidateAssesmentId { get; set; }

        public int AssesmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentCode { get; set; }

        public int SubjectId { get; set; }

        public int ModuleExamSetId { get; set; }

        public int? AdmissionId { get; set; }

        public int PersonnelId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }
         
        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Assesment Assesment { get; set; }

        public virtual ModuleExamSet ModuleExamSet { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Admission Admission { get; set; }
    }
}
