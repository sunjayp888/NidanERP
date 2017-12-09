namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleExamQuestionSetGrid")]
    public partial class ModuleExamQuestionSetGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamQuestionSetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleExamSetId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ModuleExamSetName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionTypeId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string QuestionTypeName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string QuestionDescription { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Key]
        [Column(Order = 7)]
        public bool IsOptionA { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsOptionB { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsOptionC { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool IsOptionD { get; set; }

        [StringLength(500)]
        public string SubjectiveAnswer { get; set; }

        [StringLength(500)]
        public string CorrectAnswer { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
