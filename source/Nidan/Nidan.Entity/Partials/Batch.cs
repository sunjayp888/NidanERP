using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

            [Display(Name = "Training Type")]
            public string TrainingType { get; set; }

            [Display(Name = "Sector")]
            public int SectorId { get; set; }

            [Display(Name = "Sub-Sector")]
            public string SubSector { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Training Hrs Per Day")]
            public int TrainingHrsPerDay { get; set; }

            [Display(Name = "Batch Start Date")]
            public DateTime BatchStartDate { get; set; }

            [Display(Name = "Batch End Date")]
            public DateTime BatchEndDate { get; set; }

            [Display(Name = "Prefferd Assesment Date")]
            public DateTime PrefferdAssesmentDate { get; set; }
        }
    }
}
