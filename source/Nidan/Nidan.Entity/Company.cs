namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public Company()
        {
            CreatedDate = DateTime.UtcNow.Date;
            CompanyBranches = new HashSet<CompanyBranch>();
            CompanyFollowUps = new HashSet<CompanyFollowUp>();
            CandidateFinalPlacements = new HashSet<CandidateFinalPlacement>();
            CandidatePostPlacements = new HashSet<CandidatePostPlacement>();
        }
        public int CompanyId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public int? CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyBranch> CompanyBranches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyFollowUp> CompanyFollowUps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateFinalPlacement> CandidateFinalPlacements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidatePostPlacement> CandidatePostPlacements { get; set; }
    }
}
