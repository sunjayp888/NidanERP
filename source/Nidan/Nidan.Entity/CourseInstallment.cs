namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseInstallment")]
    public partial class CourseInstallment
    {
        public int CourseInstallmentId { get; set; }

        public int CourseId { get; set; }

        public int Duration { get; set; }

        public int? Month { get; set; }

        public int Fee { get; set; }

        public int DownPayment { get; set; }

        public int LumpsumAmt { get; set; }

        public int NoOfInstallment { get; set; }

        public int? FirstInstallment { get; set; }

        public int? SecondInstallment { get; set; }

        public int? ThirdInstallment { get; set; }

        public int? ForthInstallment { get; set; }

        public int? FifthInstallment { get; set; }

        public int? SixthInstallment { get; set; }

        public int? SeventhInstallment { get; set; }

        public int? EighthInstallment { get; set; }

        public int? NinethInstallment { get; set; }

        public int? TenthInstallment { get; set; }

        public int? EleventhInstallment { get; set; }

        public int? TwelvethInstallment { get; set; }

        public int CourseFeeBreakUpId { get; set; }

        public int OrganisationId { get; set; }

        public virtual CourseFeeBreakUp CourseFeeBreakUp { get; set; }

        public virtual Course Course { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
