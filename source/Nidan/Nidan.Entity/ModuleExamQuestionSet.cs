namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleExamQuestionSet")]
    public partial class ModuleExamQuestionSet
    {
        public ModuleExamQuestionSet()
        {
            CreatedDate = DateTime.UtcNow.Date;
            CandidateAssessmentQuestionAnswers = new HashSet<CandidateAssessmentQuestionAnswer>();
        }
        public int ModuleExamQuestionSetId { get; set; }

        public int ModuleExamSetId { get; set; }

        public int QuestionTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestionDescription { get; set; }

        public int MarkPerQuestion { get; set; }

        [StringLength(100)]
        public string OptionA { get; set; }

        [StringLength(100)]
        public string OptionB { get; set; }

        [StringLength(100)]
        public string OptionC { get; set; }

        [StringLength(100)]
        public string OptionD { get; set; }

        [StringLength(100)]
        public string AnswerType { get; set; }

        public bool IsOptionA { get; set; }

        public bool IsOptionB { get; set; }

        public bool IsOptionC { get; set; }

        public bool IsOptionD { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string SubjectiveAnswer { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual ModuleExamSet ModuleExamSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssessmentQuestionAnswer> CandidateAssessmentQuestionAnswers { get; set; }
    }
}
