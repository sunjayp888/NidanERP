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
    [MetadataType(typeof(ActivityTask.ActivityTaskMetadata))]
    public partial class ActivityTask : IOrganisationFilterable
    {
        private class ActivityTaskMetadata
        {
            [Display(Name = "Task Name")]
            public string Name { get; set; }

            [Display(Name = "Activity Name")]
            public int ActivityId { get; set; }

            [Display(Name = "Centre")]
            public int CentreId { get; set; }

            [Display(Name = "Assign To")]
            public int AssignTo { get; set; }

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
