namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Counselling")]
    public partial class Counselling
    {
        public Counselling()
        {
            CreatedDate = DateTime.UtcNow.Date;
            FollowUpDate = DateTime.UtcNow.AddDays(2);
        }
        public int CounsellingId { get; set; }

        public int EnquiryId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int PersonnelId { get; set; }

        public int CourseOfferedId { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string RemarkByBranchManager { get; set; }

        public int SectorId { get; set; }

        [StringLength(100)]
        public string PsychomatricTest { get; set; } = "No";

        public int ConversionProspect { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        [DataType(DataType.MultilineText)]
        public string ClosingRemark { get; set; }

        public string RemarkByBm { get; set; }

        public bool IsRegistrationDone { get; set; }

        public int CreatedBy { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        public int? OccupationId { get; set; }

        public int EducationalQualificationId { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        public int? YearOfExperience { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        public int PreferredMonthForJoining { get; set; }

        public int BatchTimePreferId { get; set; }

        public int SchemeId { get; set; }

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

        public int AnnualIncome { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Occupation Occupation { get; set; }

        //public virtual Qualification Qualification { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual BatchTimePrefer BatchTimePrefer { get; set; }
    }
}
