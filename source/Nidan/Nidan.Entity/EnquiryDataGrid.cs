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
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeadSourceId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string LeadSourceName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(500)]
        public string Address4 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string Taluka { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string State { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string District { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string City { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PinCode { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        public long? GuardianContactNo { get; set; }

        [StringLength(500)]
        public string Occupation { get; set; }

        [StringLength(500)]
        public string Religion { get; set; }

        [Key]
        [Column("Caste Category", Order = 15)]
        [StringLength(500)]
        public string Caste_Category { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(100)]
        public string Gender { get; set; }

        [StringLength(1000)]
        public string Qualification { get; set; }

        [StringLength(100)]
        public string YearOfPassOut { get; set; }

        [StringLength(100)]
        public string Marks { get; set; }

        [StringLength(500)]
        public string Scheme { get; set; }

        [StringLength(500)]
        public string Sector { get; set; }

        [Column("How Did You Know About Us")]
        [StringLength(1000)]
        public string How_Did_You_Know_About_Us { get; set; }

        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(100)]
        public string Promotional { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "date")]
        public DateTime EnquiryDate { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(100)]
        public string EnquiryStatus { get; set; }

        [Column("Enquiry Type")]
        [StringLength(500)]
        public string Enquiry_Type { get; set; }

        [Column("Student Type")]
        [StringLength(500)]
        public string Student_Type { get; set; }

        [Column("Batch Time Prefer")]
        [StringLength(500)]
        public string Batch_Time_Prefer { get; set; }

        [StringLength(500)]
        public string AppearingQualification { get; set; }

        [StringLength(100)]
        public string PlacementNeeded { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConversionProspect { get; set; }

        public string Remarks { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public int? PreferredMonthForJoining { get; set; }

        [StringLength(500)]
        public string OtherInterestedCourse { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(3)]
        public string IsCounsellingDone { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(3)]
        public string IsRegistrationDone { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(3)]
        public string IsAdmissionDone { get; set; }
    }
}
