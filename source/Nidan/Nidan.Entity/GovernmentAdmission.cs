namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GovernmentAdmission")]
    public partial class GovernmentAdmission
    {
        public int GovernmentAdmissionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AdmissionDate { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public int EnquiryId { get; set; }

        public int? BatchId { get; set; }

        public int SchemeId { get; set; }

        [Required]
        [StringLength(500)]
        public string TrainingType { get; set; } = "Short Term";

        public int SectorId { get; set; }

        public int SubSectorId { get; set; }

        public int CourseId { get; set; }

        public int HowDidYouKnowAboutId { get; set; }

        [Required]
        [StringLength(500)]
        public string ConveyanceAndBoardingPreference { get; set; } = "Conveyance";

        [Required]
        [StringLength(50)]
        public string Salutation { get; set; }

        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }

        [StringLength(500)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        [Required]
        [StringLength(100)]
        public string Gender { get; set; } = "Male";

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public int? YearOfBirth { get; set; }

        public long AadhaarNo { get; set; }

        [StringLength(1000)]
        public string AadhaarVerificationStatus { get; set; }

        public int DisabilityId { get; set; }

        public int QualificationId { get; set; }

        public int CasteCategoryId { get; set; }

        public int ReligionId { get; set; }

        public int? AlternateIdTypeId { get; set; }

        public long? AlternateIdNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public long Mobile { get; set; }

        public long? LandlineNo { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(500)]
        public string NameAsInBank { get; set; }

        public long? BankAccountNo { get; set; }

        [StringLength(500)]
        public string IfscCode { get; set; }

        [StringLength(500)]
        public string BankName { get; set; }

        [StringLength(500)]
        public string TcName { get; set; }

        [StringLength(500)]
        public string TcId { get; set; }

        [StringLength(500)]
        public string PartnerName { get; set; }

        public string TcAddress { get; set; }

        [StringLength(500)]
        public string SdmsCandidateId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual SubSector SubSector { get; set; }

        public virtual Course Course { get; set; }

        public virtual HowDidYouKnowAbout HowDidYouKnowAbout { get; set; }

        public virtual Disability Disability { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual CasteCategory CasteCategory { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual AlternateIdType AlternateIdType { get; set; }
    }
}
