using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(Courseadata))]
    public partial class Course : IOrganisationFilterable
    {
        private class Courseadata
        {
            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }

            //[Display(Name = "Course Type")]
            //public string CourseType { get; set; }
        }
    }
}
