

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(FollowUpMetaData))]
    public partial class FollowUp : IOrganisationFilterable
    {
        private class FollowUpMetaData
        {
            [Display(Name = "Interested Course")]
            public int IntrestedCourseId { get; set; }

            [Display(Name = "Follow-Up Date")]
            public DateTime FollowUpDateTime { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            public string Remark { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Closing Remarks")]
            public string ClosingRemark { get; set; }
        }
    }
}