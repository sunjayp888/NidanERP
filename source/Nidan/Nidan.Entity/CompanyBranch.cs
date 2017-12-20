namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBranch")]
    public partial class CompanyBranch
    {
        public CompanyBranch()
        {
            CreatedDate = DateTime.UtcNow.Date;
            CompanyFollowUps = new HashSet<CompanyFollowUp>();
        }
        public int CompanyBranchId { get; set; }

        public int CompanyId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string HRName1 { get; set; }

        [Required]
        [StringLength(100)]
        public string HREmail1 { get; set; }

        public long HRContact1 { get; set; }

        [StringLength(100)]
        public string HRName2 { get; set; }

        [StringLength(100)]
        public string HREmail2 { get; set; }

        public long? HRContact2 { get; set; }

        public int SectorId { get; set; }

        public int CentreId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Company Company { get; set; }

        public virtual Sector Sector { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyFollowUp> CompanyFollowUps { get; set; }
    }
}
