namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleExamQuestionAnswerGrid")]
    public partial class ModuleExamQuestionAnswerGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidateAssessmentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string StudentCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamSetId { get; set; }

        public int? AdmissionId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamQuestionSetId { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionTypeId { get; set; }

        [StringLength(100)]
        public string AnswerType { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MarkPerQuestion { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(500)]
        public string QuestionDescription { get; set; }

        [Key]
        [Column(Order = 12)]
        public bool IsOptionA { get; set; }

        [Key]
        [Column(Order = 13)]
        public bool IsOptionB { get; set; }

        [Key]
        [Column(Order = 14)]
        public bool IsOptionC { get; set; }

        [Key]
        [Column(Order = 15)]
        public bool IsOptionD { get; set; }

        [StringLength(500)]
        public string SubjectiveAnswer { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(100)]
        public string QuestionSet { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(100)]
        public string QuestionTypeName { get; set; }

        [StringLength(100)]
        public string OptionA { get; set; }

        [StringLength(100)]
        public string OptionB { get; set; }

        [StringLength(100)]
        public string OptionC { get; set; }

        [StringLength(100)]
        public string OptionD { get; set; }

        [StringLength(500)]
        public string CorrectAnswer { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
