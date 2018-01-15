namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyFollowUp")]
    public partial class CompanyFollowUp
    {
        public CompanyFollowUp()
        {
            CreatedDate = DateTime.UtcNow.Date;
            CompanyFollowUpHistories = new HashSet<CompanyFollowUpHistory>();
        }
        public int CompanyFollowUpId { get; set; }

        public int CompanyBranchId { get; set; }

        public int CompanyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime FollowUpDate { get; set; }

        public bool IsClosed { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Company Company { get; set; }

        public virtual CompanyBranch CompanyBranch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyFollowUpHistory> CompanyFollowUpHistories { get; set; }
    }
}
