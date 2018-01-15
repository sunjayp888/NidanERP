

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;

namespace Nidan.Entity
{
    [MetadataType(typeof(EnquiryMetadata))]
    public partial class Enquiry : IOrganisationFilterable
    {
        [NotMapped]
        public string Fullname => string.Join(" ", new string[] { Title.Trim(), FirstName.Trim(), MiddleName?.Trim() ?? string.Empty, LastName.Trim() }).Trim();
        private class EnquiryMetadata
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Contact No")]
            public long Mobile { get; set; }

            [Display(Name = "Email Id")]
            [StringLength(500)]
            public string EmailId { get; set; }

            [Display(Name = "Father Name")]
            [StringLength(500)]
            public string GuardianName { get; set; }

            [Display(Name = "Parent's Contact No")]
            public long? GuardianContactNo { get; set; }

            [Display(Name = "Occupation")]
            public int OccupationId { get; set; }

            [Display(Name = "Religion")]
            public int ReligionId { get; set; }

            [Display(Name = "Category Code")]
            public int CasteCategoryId { get; set; }

            [Display(Name = "Highest Qualification Completed")]
            public int EducationalQualificationId { get; set; }

            [Display(Name = "Year Of Pass Out")]
            [StringLength(100)]
            public string YearOfPassOut { get; set; }

            [Display(Name = "Intrested Course")]
            public int IntrestedCourseId { get; set; }

            [Display(Name = "How Did You Know About Us")]
            public int HowDidYouKnowAboutId { get; set; }

            [Display(Name = "Pre-Training Status")]
            [StringLength(100)]
            public string PreTrainingStatus { get; set; }

            public string Gender { get; set; }

            [Display(Name = "Employment Status")]
            [StringLength(100)]
            public string EmploymentStatus { get; set; }

            [Display(Name = "Enquiry Follow-Up Date")]
            [Column(TypeName = "date")]
            public DateTime? FollowUpDate { get; set; }

            [Display(Name = "Employer Name")]
            [StringLength(500)]
            public string EmployerName { get; set; }

            [Display(Name = "Employer Contact No")]
            [StringLength(50)]
            public string EmployerContactNo { get; set; }

            [Display(Name = "Employer Address")]
            public string EmployerAddress { get; set; }

            [Display(Name = "Annual Income")]
            public int AnnualIncome { get; set; }

            [Display(Name = "Enquiry Type")]
            public int EnquiryTypeId { get; set; }

            [Display(Name = "Candidate Type")]
            public int StudentTypeId { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Batch Time Prefer")]
            public int BatchTimePreferId { get; set; }

            [Display(Name = "Appearing Qualification")]
            [StringLength(500)]
            public string AppearingQualification { get; set; }

            [Display(Name = "Experience in Year(s)")]
            public int YearOfExperience { get; set; }

            [Display(Name = "Placement Needed")]
            [StringLength(100)]
            public string PlacementNeeded { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            public string Remarks { get; set; }

            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }

            [Display(Name = "Preferred Month For Joining")]
            public int PreferredMonthForJoining { get; set; }

            [Display(Name = "Conversion Prospect (%)")]
            public int ConversionProspect { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Closing Remarks")]
            public string ClosingRemark { get; set; }

            [Display(Name = "Alternate Mobile")]
            public long? AlternateMobile { get; set; }

            [Display(Name = "Other Interested Course")]
            public int OtherInterestedCourse { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks By Branch Manager")]
            public string RemarkByBranchManager { get; set; }

            [Display(Name = "Address")]
            public string Address1 { get; set; }

            [Display(Name = "Address")]
            public string Address2 { get; set; }

            [Display(Name = "Address")]
            public string Address3 { get; set; }

            [Display(Name = "Address")]
            public string Address4 { get; set; }

            [Display(Name = "Taluka")]
            public int TalukaId { get; set; }

            [Display(Name = "State")]
            public int StateId { get; set; }

            [Display(Name = "District")]
            public int DistrictId { get; set; }

            [Display(Name = "Pin Code")]
            public int PinCode { get; set; }
        }
    }
}
