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
            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Course Offered")]
            public int CourseOfferedId { get; set; }

            [Display(Name = "Prefer Timing")]
            public string PreferTiming { get; set; }

            [Display(Name = "Conversion Prospect (%)")]
            public int ConversionProspect { get; set; }

            [Display(Name = "Follow-Up Date")]
            public DateTime? FollowUpDate { get; set; }

            [Display(Name = "Closing Remarks")]
            public string ClosingRemark { get; set; }

            [Display(Name = "Remarks By Branch Manager")]
            public string RemarkByBranchManager { get; set; }

            [Display(Name = "Father Name")]
            public string GuardianName { get; set; }

            [Display(Name = "Parent's Contact No")]
            public long? GuardianContactNo { get; set; }

            [Display(Name = "Occupation")]
            public int OccupationId { get; set; }

            [Display(Name = " Scheme")]
            public int SchemeId { get; set; }

            [Display(Name = "Batch Time Prefer")]
            public int BatchTimePreferId { get; set; }

            [Display(Name = " Preferred Month For Joining")]
            public int PreferredMonthForJoining { get; set; }
        }
    }
}
