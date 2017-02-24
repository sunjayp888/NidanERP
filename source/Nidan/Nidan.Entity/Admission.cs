namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admission")]
    public partial class Admission
    {
        public int AdmissionId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public int EnquiryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Salutation { get; set; }

        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(500)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public long Mobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; } = "Male";

        public int CasteCategoryId { get; set; }

        public int ReligionId { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        public long? FatherMobile { get; set; }

        public long? ResidentialNo { get; set; }

        public string PermanentAddress { get; set; }

        public int PTalukaId { get; set; }

        public int PDistrictId { get; set; }

        public int PStateId { get; set; }

        public int? PPinCode { get; set; }

        public string CommunicationAddress { get; set; }

        public int CTalukaId { get; set; }

        public int CDistrictId { get; set; }

        public int CStateId { get; set; }

        public int? CPinCode { get; set; }

        public int EducaticationalQualificationId { get; set; }

        [StringLength(500)]
        public string ProfessionalQualification { get; set; }

        [StringLength(500)]
        public string TechnicalQualification { get; set; }

        [StringLength(500)]
        public string PreTrainingStatus { get; set; } = "Fresher";

        [StringLength(500)]
        public string EmploymentStatus { get; set; } = "UnEmployed";

        [StringLength(500)]
        public string EmployerName { get; set; }

        public long? EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public int? AnnualIncome { get; set; }

        [Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        public int? YearOfExperience { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual CasteCategory CasteCategory { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual Religion Religion { get; set; }
    }
}
