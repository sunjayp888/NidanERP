using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(Activity.ActivityMetadata))]
    public partial class Activity : IOrganisationFilterable
    {
        private class ActivityMetadata
        {
            [Display(Name = "Activity Status")]
            public int TaskStateId { get; set; }

            [Display(Name = "Activity Type")]
            public int ActivityTypeId { get; set; }

            [Display(Name = "Project")]
            public int ProjectId { get; set; }

            [Display(Name = "Assignee Group")]
            public int ActivityAssigneeGroupId { get; set; }

            [Display(Name = "Start Date")]
            [Column(TypeName = "date")]
            public DateTime StartDate { get; set; }

            [Display(Name = "End Date")]
            [Column(TypeName = "date")]
            public DateTime EndDate { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            [Required]
            public string Remark { get; set; }
        }
    }
}
