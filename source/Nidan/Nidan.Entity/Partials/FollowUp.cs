

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

            [Display(Name = "Remarks")]
            public string Remark { get; set; }
        }
    }
}