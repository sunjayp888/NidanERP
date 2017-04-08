using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CourseFeeBreakUpMetadata))]
    public partial class CourseFeeBreakUp : IOrganisationFilterable
    {
        private class CourseFeeBreakUpMetadata
        {
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Centre")]
            public int CentreId { get; set; }
        }
    }
}
