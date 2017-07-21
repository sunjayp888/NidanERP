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
        public FollowUp()
        {
            CreatedDateTime= DateTime.UtcNow.Date;
        }
        public int FollowUpId { get; set; }

        public DateTime FollowUpDateTime { get; set; }

        public int? MobilizationId { get; set; }

        public int? EnquiryId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public string Remark { get; set; }

        public bool? Closed { get; set; }

        public DateTime ReadDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        public long? Mobile { get; set; }

        public int IntrestedCourseId { get; set; }

        [StringLength(200)]
        public string FollowUpType { get; set; }

        public long? AlternateMobile { get; set; }

        [StringLength(2000)]
        public string FollowUpUrl { get; set; }

        public int? CounsellingId { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        public int? RegistrationId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Mobilization Mobilization { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Registration Registration { get; set; }

        public virtual Centre Centre { get; set; }

        public int? AdmissionId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowUpHistory> FollowUpHistories { get; set; }
    }
}
