using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(BatchPrePlacementMetadata))]
    public partial class BatchPrePlacement : IOrganisationFilterable
    {
        private class BatchPrePlacementMetadata
        {
            [Display(Name = "Pre-Placement Activity Name")]
            public string Name { get; set; }

            [Display(Name = "Scheduled Start Date")]
            public DateTime ScheduledStartDate { get; set; }

            [Display(Name = "Scheduled End Date")]
            public DateTime ScheduledEndDate { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Remarks")]
            public string Remark { get; set; }
        }
    }
}
