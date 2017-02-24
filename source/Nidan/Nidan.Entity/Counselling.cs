namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Counselling")]
    public partial class Counselling
    {
        public int CounsellingId { get; set; }

        public int EnquiryId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(500)]
        public string CounselledBy { get; set; }

        public int CourseOfferedId { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string RemarkByBm { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }
    }
}
