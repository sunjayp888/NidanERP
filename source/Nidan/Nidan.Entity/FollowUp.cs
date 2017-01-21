namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FollowUp")]
    public partial class FollowUp
    {
        public int FollowUpId { get; set; }

        public DateTime? FollowUpDateTime { get; set; }

        public int? MobilizationId { get; set; }

        public int? EnquiryId { get; set; }

        public string Remark { get; set; }

        public bool? Closed { get; set; }

        public DateTime? ReadDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [StringLength(1000)]
        public string AreaOfInterest { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Mobilization Mobilization { get; set; }

        public virtual Enquiry Enquiry { get; set; }

    }
}
