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

        public long ContactNo { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        public int Age { get; set; }

        [Required]
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
        public string Gender { get; set; }

        public int EducationalQualificationId { get; set; }

        [StringLength(100)]
        public string YearOFPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        public int IntrestedCourseId { get; set; }

        public int HowDidYouKnowAboutId { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(100)]
        public string Promotional { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnquiryDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Place { get; set; }

        [Required]
        [StringLength(500)]
        public string CounselledBy { get; set; }

        public int CourseOfferedId { get; set; }

        public DateTime? PreferTiming { get; set; }

        public string Remarks { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        [StringLength(100)]
        public string EnquiryStatus { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        [StringLength(50)]
        public string EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public int AnnualIncome { get; set; }

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

    }
}
