namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateAttemptedQuestionAnswerGrid")]
    public partial class CandidateAttemptedQuestionAnswerGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateAssessmentQuestionAnswerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateAssessmentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentId { get; set; }

        [StringLength(100)]
        public string AssessmentName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamSetId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamQuestionSetId { get; set; }

        [StringLength(500)]
        public string Question { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string ModuleExamSetName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionTypeId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string QuestionTypeName { get; set; }

        [StringLength(100)]
        public string AnswerType { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsOptionA { get; set; }

        [StringLength(100)]
        public string OptionA { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsOptionB { get; set; }

        [StringLength(100)]
        public string OptionB { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool IsOptionC { get; set; }

        [StringLength(100)]
        public string OptionC { get; set; }

        [Key]
        [Column(Order = 11)]
        public bool IsOptionD { get; set; }

        [StringLength(100)]
        public string OptionD { get; set; }

        public string CandidateSubjectiveAnswer { get; set; }

        public string CandidateMCQAnswer { get; set; }

        [StringLength(500)]
        public string CorrectAnswer { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkPerQuestion { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkObtained { get; set; }

        [Key]
        [Column(Order = 14)]
        public bool IsAttempted { get; set; }

        [Key]
        [Column(Order = 15)]
        public bool IsExamined { get; set; }

        public int? ExaminedBy { get; set; }

        public string Remark { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExaminedDate { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 19)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "date")]
        public DateTime CreatedDate { get; set; }
    }
}
