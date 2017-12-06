namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateAssessmentQuestionAnswer")]
    public partial class CandidateAssessmentQuestionAnswer
    {
        public CandidateAssessmentQuestionAnswer()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CandidateAssessmentQuestionAnswerId { get; set; }

        public int CandidateAssessmentId { get; set; }

        public int AssessmentId { get; set; }

        public int ModuleExamSetId { get; set; }

        public int ModuleExamQuestionSetId { get; set; }

        public int QuestionTypeId { get; set; }

        [StringLength(100)]
        public string AnswerType { get; set; }

        public bool IsOptionA { get; set; }

        public bool IsOptionB { get; set; }

        public bool IsOptionC { get; set; }

        public bool IsOptionD { get; set; }

        public string SubjectiveAnswer { get; set; }

        public int MarkPerQuestion { get; set; }

        public int MarkObtained { get; set; }

        public bool IsAttempted { get; set; }

        public bool IsExamined { get; set; }

        public int? ExaminedBy { get; set; }

        public string Remark { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExaminedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int PersonnelId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual ModuleExamQuestionSet ModuleExamQuestionSet { get; set; }

        public virtual ModuleExamSet ModuleExamSet { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public virtual Assessment Assessment { get; set; }

        public virtual CandidateAssessment CandidateAssessment { get; set; }
    }
}
