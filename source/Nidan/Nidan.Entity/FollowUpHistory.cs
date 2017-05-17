namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FollowUpHistory")]
    public partial class FollowUpHistory
    {
        public int FollowUpHistoryId { get; set; }

        public int FollowUpId { get; set; }

        [Required]
        [StringLength(200)]
        public string FollowUpType { get; set; }

        public string Remarks { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemarks { get; set; }

        public DateTime FollowUpDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual FollowUp FollowUp { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
