namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleExamSet")]
    public partial class ModuleExamSet
    {
        public ModuleExamSet()
        {
            CreatedDate = DateTime.UtcNow.Date;
            ModuleExamQuestionSets = new HashSet<ModuleExamQuestionSet>();
            CandidateAssesments = new HashSet<CandidateAssesment>();
        }
        public int ModuleExamSetId { get; set; }

        [Required]
        [StringLength(100)]
        public string QuestionSetName { get; set; }

        public int SubjectId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleExamQuestionSet> ModuleExamQuestionSets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments { get; set; }
    }
}
