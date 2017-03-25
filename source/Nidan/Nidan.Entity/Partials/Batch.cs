using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(BatchMetadata))]
    public partial class Batch : IOrganisationFilterable
    {
        private class BatchMetadata
        {
            [Display(Name = "Scheme")]
            public int SchemeId { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "No Of Days")]
            public int? NoOfDays { get; set; }

            [Display(Name = "No Of Hrs")]
            public int NoOfHrs { get; set; }

            [Display(Name = "No Of Holidays")]
            public int? NoOfHolidays { get; set; }

            [Display(Name = "Batch Start Date")]
            public DateTime BatchStartDate { get; set; }

            [Display(Name = "Batch End Date")]
            public DateTime BatchEndDate { get; set; }

            [Display(Name = "Preferred Assesment Date")]
            public DateTime PreferredAssesmentDate { get; set; }

            [Display(Name = "Trainer")]
            public int TrainerId { get; set; }
        }
    }
}
