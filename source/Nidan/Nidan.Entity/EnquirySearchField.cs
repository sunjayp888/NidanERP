using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("EnquirySearchField")]
    public partial class EnquirySearchField
    {
        [Key]
        [Column(Order = 0)]
        public int EnquiryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 2)]
        public double ContactNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [Key]
        [Column(Order = 4)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string Qualification { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string Address { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string GuardianName { get; set; }

        [Key]
        [Column(Order = 8)]
        public double GuardianContactNo { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(500)]
        public string Occupation { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(500)]
        public string Religion { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(500)]
        public string CatagoryCode { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(500)]
        public string EducationalQualification { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(100)]
        public string YearOFPassOut { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(100)]
        public string Marks { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(500)]
        public string AreaOfInterest { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(500)]
        public string HowDidYouKnowAboutUs { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(100)]
        public string Promotional { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "datetime2")]
        public DateTime EnquiryDate { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(100)]
        public string Place { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(500)]
        public string CounselledBy { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(500)]
        public string CourseOffered { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "datetime2")]
        public DateTime PreferTiming { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(500)]
        public string Remarks { get; set; }

        [Key]
        [Column(Order = 27)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 28)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
