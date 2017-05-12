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
            public int CourseInstallmentId { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Trainer")]
            public int TrainerId { get; set; }

            [Display(Name = "Batch Start Date")]
            public DateTime BatchStartDate { get; set; }

            [Display(Name = "Batch End Date")]
            public DateTime BatchEndDate { get; set; }

            [Display(Name = "Number Of Hours Daily")]
            public int NumberOfHoursDaily { get; set; }

            [Display(Name = "Number Of Holidays")]
            public int NumberOfHolidays { get; set; }

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

            [Display(Name = "Assessment Date")]
            public DateTime AssessmentDate { get; set; }

            [Display(Name = "Number Of Installment")]
            public int NumberOfInstallment { get; set; }

            [Display(Name = "First Installment")]
            public int? FirstInstallment { get; set; }

            [Display(Name = "Second Installment")]
            public int? SecondInstallment { get; set; }

            [Display(Name = "Third Installment")]
            public int? ThirdInstallment { get; set; }

            [Display(Name = "Fourth Installment")]
            public int? FourthInstallment { get; set; }

            [Display(Name = "Fifth Installment")]
            public int? FifthInstallment { get; set; }

            [Display(Name = "Sixth Installment")]
            public int? SixthInstallment { get; set; }

            [Display(Name = "Seventh Installment")]
            public int? SeventhInstallment { get; set; }

            [Display(Name = "Eighth Installment")]
            public int? EighthInstallment { get; set; }

            [Display(Name = "Nineth Installment")]
            public int? NinethInstallment { get; set; }

            [Display(Name = "Tenth Installment")]
            public int? TenthInstallment { get; set; }

            [Display(Name = "Eleventh Installment")]
            public int? EleventhInstallment { get; set; }

            [Display(Name = "Twelveth Installment")]
            public int? TwelvethInstallment { get; set; }
        }
    }
}
