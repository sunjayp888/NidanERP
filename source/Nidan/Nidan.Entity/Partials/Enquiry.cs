

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

            public long? AlternateMobile { get; set; }

            [Display(Name = "Email Id")]
            public string EmailId { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public int Age { get; set; }

            public string Address1 { get; set; }

            public string Address2 { get; set; }

            public string Address3 { get; set; }

            public string Address4 { get; set; }

            public int CityId { get; set; }

            public int TalukaId { get; set; }

            public int StateId { get; set; }

            public int DistrictId { get; set; }

            public int PinCode { get; set; }

            [Display(Name = "Father Name")]
            public string GuardianName { get; set; }

            [Display(Name = "Parent's Contact No")]
            public long? GuardianContactNo { get; set; }

            //public int? OccupationId { get; set; }

            [Display(Name = "Religion")]
            public int ReligionId { get; set; }

            [Display(Name = "Category Code")]
            public int CasteCategoryId { get; set; }

            public string Gender { get; set; }

            //public int? EducationalQualificationId { get; set; }

            //public string YearOfPassOut { get; set; }

            //public string Marks { get; set; }

            [Display(Name = "Intrested Course")]
            public int IntrestedCourseId { get; set; }

            //public int? HowDidYouKnowAboutId { get; set; }

            //public string PreTrainingStatus { get; set; } = "Fresher";

            //public string EmploymentStatus { get; set; } = "UnEmployed";

            //public string Promotional { get; set; }

            //public DateTime EnquiryDate { get; set; }

            //public string StudentCode { get; set; }

            //public string EnquiryStatus { get; set; }

            //public string EmployerName { get; set; }

            //public string EmployerContactNo { get; set; }

            //public string EmployerAddress { get; set; }

            //public int? AnnualIncome { get; set; }

            //public int? SchemeId { get; set; }

            //public int? EnquiryTypeId { get; set; }

            //public int? StudentTypeId { get; set; }

            //public int? SectorId { get; set; }

            //public int? BatchTimePreferId { get; set; }

            //public string AppearingQualification { get; set; }

            //public int? YearOfExperience { get; set; }

            //public string PlacementNeeded { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            public string Remarks { get; set; }

            //public DateTime? FollowUpDate { get; set; }

            //public int? PreferredMonthForJoining { get; set; }

            //public string Close { get; set; }

            //public string ClosingRemark { get; set; }

            public int ConversionProspect { get; set; }

            //public string OtherInterestedCourse { get; set; }

            //public string RemarkByBranchManager { get; set; }

            //public bool IsCounsellingDone { get; set; }

            //public bool IsRegistrationDone { get; set; }

            //public bool IsAdmissionDone { get; set; }

            //public int CentreId { get; set; }

            //public int OrganisationId { get; set; }

            //public int CreatedBy { get; set; }

            [Display(Name = "Lead Source")]
            public int LeadSourceId { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }
        }
    }
}
