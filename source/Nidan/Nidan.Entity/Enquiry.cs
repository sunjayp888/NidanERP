namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enquiry")]
    public partial class Enquiry
    {
        public int EnquiryId { get; set; }

        [Required]
        [StringLength(500)]
        public string CandidateName { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public long Mobile { get; set; }

        public long? AlternateMobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        public int Age { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        public int OccupationId { get; set; }

        public int ReligionId { get; set; }

        public int CasteCategoryId { get; set; }
        
        public int SchemeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Gender { get; set; } = "Female";

        public int EducationalQualificationId { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        public int IntrestedCourseId { get; set; }

        public int HowDidYouKnowAboutId { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; } = "Fresher";

        [StringLength(100)]
        public string EmploymentStatus { get; set; } = "UnEmployed";

        [StringLength(100)]
        public string Promotional { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnquiryDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(100)]
        public string EnquiryStatus { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        [StringLength(50)]
        public string EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public int AnnualIncome { get; set; }

        public int EnquiryTypeId { get; set; }

        public int StudentTypeId { get; set; }

        public int SectorId { get; set; }

        public int BatchTimePreferId { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        public int YearOfExperience { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [StringLength(5)]
        public string Close { get; set; } = "No";

        [DataType(DataType.MultilineText)]
        public string ClosingRemark { get; set; }

        [DataType(DataType.MultilineText)]
        public string RemarkByBm { get; set; }

        public int ConversionProspect { get; set; }

        public bool Registered { get; set; }

        [StringLength(500)]
        public string OtherInterestedCourse { get; set; }

        public virtual CasteCategory CasteCategory { get; set; }

        public virtual Centre Centre { get; set; }

        //public virtual Enquiry Enquiry1 { get; set; }

        //public virtual Enquiry Enquiry2 { get; set; }

        public virtual HowDidYouKnowAbout HowDidYouKnowAbout { get; set; }

        public virtual Occupation Occupation { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual Course Course { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual BatchTimePrefer BatchTimePrefer { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual EnquiryType EnquiryType { get; set; }

        public virtual StudentType StudentType { get; set; }

        public int PreferredMonthForJoining { get; set; }
  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Counselling> Counsellings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrationPaymentReceipt> RegistrationPaymentReceipts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryCourse> EnquiryCourses { get; set; }
    }
}
