namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnquiryCourse")]
    public partial class EnquiryCourse
    {
        public int EnquiryCourseId { get; set; }

        public int EnquiryId { get; set; }

        public int CourseId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Course Course { get; set; }
    }
}
