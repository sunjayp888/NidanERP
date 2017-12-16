namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyFollowUpHistory")]
    public partial class CompanyFollowUpHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanyFollowUpHistory()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CompanyFollowUpHistoryId { get; set; }

        public int CompanyFollowUpId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Remark { get; set; }

        [Column(TypeName = "date")]
        public DateTime FollowUpDate { get; set; }

        public bool IsClosed { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public Centre Centre { get; set; }

        public virtual CompanyFollowUp CompanyFollowUp { get; set; }
    }
}
