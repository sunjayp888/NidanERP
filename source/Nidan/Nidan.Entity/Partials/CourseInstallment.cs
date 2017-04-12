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

            [Display(Name = "Fees")]
            public int Fee { get; set; }

            [Display(Name = "Down Payment")]
            public int DownPayment { get; set; }

            [Display(Name = "Lumpsum Amount")]
            public int LumpsumAmt { get; set; }
            
        }
    }
}
