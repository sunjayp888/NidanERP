namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CounsellingDataGrid")]
    public partial class CounsellingDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CounsellingId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string LeadSourceName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(202)]
        public string CounselledBy { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(1000)]
        public string CourseOffered { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        public string Remarks { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string RemarkByBranchManager { get; set; }

        [StringLength(100)]
        public string PsychomatricTest { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConversionProspect { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        public string RemarkByBm { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(3)]
        public string IsRegistrationDone { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        public int? OccupationId { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(500)]
        public string OccupationName { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EducationalQualificationId { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(1000)]
        public string QualificationName { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        public int? YearOfExperience { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PreferredMonthForJoining { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchTimePreferId { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(500)]
        public string BatchTimePreferName { get; set; }

        [Key]
        [Column(Order = 19)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchemeId { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(500)]
        public string SchemeName { get; set; }

        [Key]
        [Column(Order = 21)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorId { get; set; }

        [StringLength(500)]
        public string SectorName { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(100)]
        public string Promotional { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        [StringLength(50)]
        public string EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnnualIncome { get; set; }
    }
}
