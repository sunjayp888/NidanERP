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
    [MetadataType(typeof(AssesmentMetadata))]
    public partial class Assesment : IOrganisationFilterable
    {
        private class AssesmentMetadata
        {
            [Display(Name = "Assesment Name")]
            public string Name { get; set; }

            [Display(Name = "Assesment Type")]
            public int AssesmentTypeId { get; set; }

            [Display(Name = "Assesment Date")]
            public DateTime AssesmentDate { get; set; }

            [Display(Name = "Centre")]
            public int CentreId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Batch")]
            public int BatchId { get; set; }
        }
    }
}
