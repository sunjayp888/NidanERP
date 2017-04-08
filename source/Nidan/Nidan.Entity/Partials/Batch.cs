using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;
using System;


namespace Nidan.Entity
{
    [MetadataType(typeof(BatchMetadata))]
    public partial class Batch : IOrganisationFilterable
    {
        private class BatchMetadata
        {
            [Display(Name = "Course Fee Break-Up")]
            public int CourseFeeBreakUpId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Trainer")]
            public int TrainerId { get; set; }

            //[Display(Name = "BatchDay")]
            //public int? BatchDayId { get; set; }

            [Display(Name = "Batch Start Date")]
            public DateTime BatchStartDate { get; set; }

            [Display(Name = "Batch End Date")]
            public DateTime BatchEndDate { get; set; }

            [Display(Name = "No Of Holidays")]
            public int NoOfHolidays { get; set; }

            [Display(Name = "Candidate Name")]
            public int NoOfHoursDaily { get; set; }

            [Display(Name = "Batch Start Time Hours")]
            public int BatchStartTimeHours { get; set; }

            [Display(Name = "Batch Start Time Minutes")]
            public int BatchStartTimeMinutes { get; set; }

            [Display(Name = "Batch Start Time Span")]
            public string BatchStartTimeSpan { get; set; }

            [Display(Name = "Batch End Time Hours")]
            public int BatchEndTimeHours { get; set; }

            [Display(Name = "Batch End Time Minutes")]
            public int BatchEndTimeMinutes { get; set; }

            [Display(Name = "Batch End Time Span")]
            public string BatchEndTimeSpan { get; set; }

            [Display(Name = "Assesment Date")]
            public DateTime AssesmentDate { get; set; }
        }
    }
}
