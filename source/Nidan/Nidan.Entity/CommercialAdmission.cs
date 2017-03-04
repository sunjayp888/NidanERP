namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommercialAdmission")]
    public partial class CommercialAdmission
    {
        public int CommercialAdmissionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public int EnquiryId { get; set; }

        public int? BatchId { get; set; }

        public int? SectorId { get; set; }

        public int? CourseId { get; set; }

        public int? StudentTypeId { get; set; }

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

        public long Mobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Gender { get; set; } = "Male";

        public int CasteCategoryId { get; set; }

        public int ReligionId { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        public long? FatherMobile { get; set; }

        public long? ResidentialNo { get; set; }

        [Required]
        public string Address { get; set; }

        public int TalukaId { get; set; }

        public int DistrictId { get; set; }

        public int StateId { get; set; }

        public int? PinCode { get; set; }

        public string CommunicationAddress { get; set; }

        public int? CommunicationTalukaId { get; set; }

        public int? CommunicationDistrictId { get; set; }

        public int? CommunicationStateId { get; set; }

        public int? CommunicationPinCode { get; set; }

        public int QualificationId { get; set; }

        [StringLength(500)]
        public string ProfessionalQualification { get; set; }

        [StringLength(500)]
        public string TechnicalQualification { get; set; }

        [Required]
        [StringLength(100)]
        public string PreTrainingStatus { get; set; } = "Fresher";

        public decimal? YearOfExperience { get; set; }

        [Required]
        [StringLength(100)]
        public string EmploymentStatus { get; set; } = "UnEmployed";

        [StringLength(500)]
        public string EmployerName { get; set; }

        public long? EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public long? AnnualIncome { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Course Course { get; set; }

        public virtual StudentType StudentType { get; set; }

        public virtual CasteCategory CasteCategory { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual Taluka Taluka { get; set; }

        public virtual District District { get; set; }

        public virtual State State { get; set; }

        public virtual Qualification Qualification { get; set; }
    }
}
