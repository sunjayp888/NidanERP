namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreCourseInstallment")]
    public partial class CentreCourseInstallment
    {
        public int CentreCourseInstallmentId { get; set; }

        public int CourseInstallmentId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual CourseInstallment CourseInstallment { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
