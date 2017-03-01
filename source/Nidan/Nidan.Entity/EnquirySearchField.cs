namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnquirySearchField")]
    public partial class EnquirySearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ContactNo { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Address { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OccupationId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReligionId { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CasteCategoryId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EducationalQualificationId { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IntrestedCourseId { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HowDidYouKnowAboutId { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(100)]
        public string Promotional { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnquiryDate { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(100)]
        public string EnquiryStatus { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        [StringLength(50)]
        public string EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public int? AnnualIncome { get; set; }

        public int? SchemeId { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryTypeId { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentTypeId { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorId { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchTimePreferId { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        public int? YearOfExperience { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        public string Remarks { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [StringLength(500)]
        public string PreferredMonthForJoining { get; set; }

        [StringLength(6270)]
        public string SearchField { get; set; }
    }
}
