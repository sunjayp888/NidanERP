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

        public int CourseDuration { get; set; }

        public int CourseFee { get; set; }

        public int LumpsumAmount { get; set; }

        public int DownPayment { get; set; }

        public int Discount { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Course Course { get; set; }
    }
}
