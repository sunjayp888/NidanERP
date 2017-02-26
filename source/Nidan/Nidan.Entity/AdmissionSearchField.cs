namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdmissionSearchField")]
    public partial class AdmissionSearchField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        public int? EnquiryId { get; set; }

        [StringLength(1000)]
        public string StudentCode { get; set; }

        [StringLength(50)]
        public string Salutation { get; set; }

        [StringLength(500)]
        public string FirstName { get; set; }

        [StringLength(500)]
        public string MiddleName { get; set; }

        [StringLength(500)]
        public string LastName { get; set; }

        public long? Mobile { get; set; }

        public long? LandlineNo { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? YearOfBirth { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        public long? FatherMobile { get; set; }

        public int? CasteCategoryId { get; set; }

        public int? ReligionId { get; set; }

        public string Address { get; set; }

        public int? TalukaId { get; set; }

        public int? DistrictId { get; set; }

        public int? StateId { get; set; }

        public int? PinCode { get; set; }

        public string CommunicationAddress { get; set; }

        public int? CommunicationTalukaId { get; set; }

        public int? CommunicationDistrictId { get; set; }

        public int? CommunicationStateId { get; set; }

        public int? CommunicationPinCode { get; set; }

        public int? DisabilityId { get; set; }

        public long? AadhaarNo { get; set; }

        [StringLength(1000)]
        public string AadhaarVerificationStatus { get; set; }

        public int? AlternateIdTypeId { get; set; }

        public long? AlternateIdNumber { get; set; }

        [StringLength(500)]
        public string NameAsInBank { get; set; }

        public long? BankAccountNo { get; set; }

        [StringLength(500)]
        public string IfscCode { get; set; }

        [StringLength(500)]
        public string BankName { get; set; }

        public int? QualificationId { get; set; }

        [StringLength(500)]
        public string ProfessionalQualification { get; set; }

        [StringLength(500)]
        public string TechnicalQualification { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        public int? YearOfExperience { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        public long? EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public long? AnnualIncome { get; set; }

        public int? SchemeId { get; set; }

        public int? SchemeTypeId { get; set; }

        [StringLength(500)]
        public string TrainingType { get; set; }

        public int? SectorId { get; set; }

        public int? SubSectorId { get; set; }

        public int? WhereDidYouHearAboutTheSchemeId { get; set; }

        [StringLength(500)]
        public string ConveyanceAndBoardingPreference { get; set; }

        public int? CourseId { get; set; }

        public int? CourseFees { get; set; }

        [StringLength(100)]
        public string PaymentType { get; set; }

        [StringLength(500)]
        public string DurationOfCourse { get; set; }

        public int? CentreId { get; set; }

        public int? BatchId { get; set; }

        public int? OrganisationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AdmissionDate { get; set; }

        [StringLength(500)]
        public string TcName { get; set; }

        [StringLength(500)]
        public string TcId { get; set; }

        [StringLength(500)]
        public string PartnerName { get; set; }

        public string TcAddress { get; set; }

        [StringLength(500)]
        public string SdmsCandidateId { get; set; }

        public int? SearchField { get; set; }
    }
}
