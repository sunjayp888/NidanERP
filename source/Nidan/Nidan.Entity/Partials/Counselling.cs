using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;

namespace Nidan.Entity
{
    [MetadataType(typeof(CounsellingMetadata))]
    public partial class Counselling : IOrganisationFilterable
    {
        private class CounsellingMetadata
        {
            [Display(Name = "Course Offered")]
            public int CourseOfferedId { get; set; }

            [Display(Name = "Prefer Timing")]
            public string PreferTiming { get; set; }

            [Display(Name = "Remarks")]
            public string Remarks { get; set; }

            [Display(Name = "Created Date")]
            public DateTime CreatedDate { get; set; }

            [Display(Name = "Follow-Up Date")]
            public DateTime? FollowUpDate { get; set; }

            [Display(Name = "Remark By Branch Manager")]
            public string RemarkByBranchManager { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Psychomatric Test")]
            public string PsychomatricTest { get; set; } = "No";

            [Display(Name = "Conversion Prospect")]
            public int ConversionProspect { get; set; }

            [Display(Name = "Close")]
            public string Close { get; set; }

            [Display(Name = "Closing Remark")]
            public string ClosingRemark { get; set; }

            [Display(Name = "Remark By Bm")]
            public string RemarkByBm { get; set; }

            [Display(Name = "Guardian Name")]
            public string GuardianName { get; set; }

            [Display(Name = "Guardian Contact No")]
            public long? GuardianContactNo { get; set; }

            [Display(Name = "Occupation")]
            public int? OccupationId { get; set; }

            [Display(Name = "Educational Qualification")]
            public int EducationalQualificationId { get; set; }

            [Display(Name = "Year Of Pass Out")]
            public string YearOfPassOut { get; set; }

            [Display(Name = "Marks")]
            public string Marks { get; set; }

            [Display(Name = "Appearing Qualification")]
            public string AppearingQualification { get; set; }

            [Display(Name = "Year Of Experience")]
            public int? YearOfExperience { get; set; }

            [Display(Name = "Placement Needed")]
            public string PlacementNeeded { get; set; }

            [Display(Name = "Preferred Month For Joining")]
            public int PreferredMonthForJoining { get; set; }

            [Display(Name = "Batch Time Prefer")]
            public int BatchTimePreferId { get; set; }

            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }

            [Display(Name = "Pre-Training Status")]
            public string PreTrainingStatus { get; set; } = "Fresher";

            [Display(Name = "Employment Status")]
            public string EmploymentStatus { get; set; } = "UnEmployed";

            [Display(Name = "Promotional")]
            public string Promotional { get; set; }

            [Display(Name = "Employer Name")]
            public string EmployerName { get; set; }

            [Display(Name = "Employer Contact No")]
            public string EmployerContactNo { get; set; }

            [Display(Name = "Employer Address")]
            public string EmployerAddress { get; set; }

            [Display(Name = "Annual Income")]
            public int AnnualIncome { get; set; }
        }
    }
}
