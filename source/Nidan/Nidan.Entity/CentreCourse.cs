namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CentreCourse")]
    public partial class CentreCourse
    {
        public int CentreCourseId { get; set; }

        public int CourseId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

    }
}
