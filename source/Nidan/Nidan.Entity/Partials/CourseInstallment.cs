using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CourseInstallmentMetadata))]
    public partial class CourseInstallment : IOrganisationFilterable
    {
        private class CourseInstallmentMetadata
        {
            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Course Fee")]
            public int Fee { get; set; }

            [Display(Name = "Course Down Payment")]
            public int DownPayment { get; set; }

            [Display(Name = "Course Lumpsum Amount")]
            public int LumpsumAmount { get; set; }
            
        }
    }
}
