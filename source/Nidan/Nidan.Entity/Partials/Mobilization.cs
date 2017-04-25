using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(MobilizationMetadata))]
    public partial class Mobilization : IOrganisationFilterable
    {
        [NotMapped]
        public string HighestQualification { get; set; }
        [NotMapped]
        public string InterestedCourse { get; set; }
      
        private class MobilizationMetadata
        {
            [Display(Name = "Event")]
            public int EventId { get; set; }

            [Display(Name = "Interested Course")]
            public int InterestedCourseId { get; set; }

            [Display(Name = "Created Date")]
            [Column(TypeName = "date")]
            public DateTime CreatedDate { get; set; }

            [Display(Name = "Follow-Up Date")]
            [Column(TypeName = "date")]
            public DateTime? FollowUpDate { get; set; }

            [Display(Name = "Student Location")]
            [StringLength(500)]
            public string StudentLocation { get; set; }

            [Display(Name = "Other Interested Course")]
            [StringLength(1000)]
            public string OtherInterestedCourse { get; set; }

            [Display(Name = "Mobilized Date")]
            [Column(TypeName = "date")]
            public DateTime GeneratedDate { get; set; }

            [Display(Name = "Mobilization Type")]
            public int MobilizationTypeId { get; set; }

            [Display(Name = "Qualification")]
            public int QualificationId { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            public string Remark { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Closing Remarks")]
            public string ClosingRemark { get; set; }

            [Display(Name = "Alternate Mobile")]
            public long? AlternateMobile { get; set; }
        }
    }
}
