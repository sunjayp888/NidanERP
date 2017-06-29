namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnquiryDataGrid")]
    public partial class EnquiryDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column("Centre Name", Order = 1)]
        [StringLength(500)]
        public string Centre_Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(500)]
        public string Address4 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string Taluka { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string State { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string District { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PinCode { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(500)]
        public string Occupation { get; set; }

        [StringLength(500)]
        public string Religion { get; set; }

        [Key]
        [Column("Caste Category", Order = 13)]
        [StringLength(500)]
        public string Caste_Category { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(1000)]
        public string Qualification { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(500)]
        public string Scheme { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(500)]
        public string Sector { get; set; }

        [Key]
        [Column("How Did You Know About Us", Order = 18)]
        [StringLength(1000)]
        public string How_Did_You_Know_About_Us { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(100)]
        public string Promotional { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnquiryDate { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(100)]
        public string EnquiryStatus { get; set; }

        [Key]
        [Column("Enquiry Type", Order = 19)]
        [StringLength(500)]
        public string Enquiry_Type { get; set; }

        [Key]
        [Column("Student Type", Order = 20)]
        [StringLength(500)]
        public string Student_Type { get; set; }

        [Key]
        [Column("Batch Time Prefer", Order = 21)]
        [StringLength(500)]
        public string Batch_Time_Prefer { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        public int? ConversionProspect { get; set; }

        public string Remarks { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public int? PreferredMonthForJoining { get; set; }

        [StringLength(500)]
        public string OtherInterestedCourse { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(3)]
        public string IsCounsellingDone { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(3)]
        public string IsRegistrationDone { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(3)]
        public string IsAdmissionDone { get; set; }
    }
}
