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

            [Display(Name = "No Of Installment")]
            public int NoOfInstallment { get; set; }

            [Display(Name = "First Installment")]
            public int? FirstInstallment { get; set; }

            [Display(Name = "Second Installment")]
            public int? SecondInstallment { get; set; }

            [Display(Name = "Third Installment")]
            public int? ThirdInstallment { get; set; }

            [Display(Name = "Forth Installment")]
            public int? ForthInstallment { get; set; }

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

            [Display(Name = "CourseFeeBreakUp")]
            public int CourseFeeBreakUpId { get; set; }
        }
    }
}
