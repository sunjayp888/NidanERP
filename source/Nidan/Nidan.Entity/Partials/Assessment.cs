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
    [MetadataType(typeof(AssessmentMetadata))]
    public partial class Assessment : IOrganisationFilterable
    {
        private class AssessmentMetadata
        {
            [Display(Name = "Assessment Name")]
            public string Name { get; set; }

            [Display(Name = "Assessment Type")]
            public int AssessmentTypeId { get; set; }

            [Display(Name = "Assessment Date")]
            public DateTime AssessmentDate { get; set; }

            [Display(Name = "Centre")]
            public int CentreId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Batch")]
            public int BatchId { get; set; }
        }
    }
}
