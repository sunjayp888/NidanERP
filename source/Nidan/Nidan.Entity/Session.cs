namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        public int SessionId { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string Duration { get; set; }

        public int CourseTypeId { get; set; }

        public string Description { get; set; }

        public int SubjectId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual CourseType CourseType { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchAttendance> BatchAttendances { get; set; }
    }
}
