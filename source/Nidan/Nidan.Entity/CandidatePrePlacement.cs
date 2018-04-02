namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacement")]
    public partial class CandidatePrePlacement
    {
        public CandidatePrePlacement()
        {
            CreatedDate = DateTime.UtcNow.Date;
            CandidatePrePlacementReports = new HashSet<CandidatePrePlacementReport>();
        }
        [Key]
        public int CandidatePrePlacementId { get; set; }

        public int BatchPrePlacementId { get; set; }

        public int PrePlacementActivityId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScheduledStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScheduledEndDate { get; set; }

        public string Remark { get; set; }

        public int CentreId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual PrePlacementActivity PrePlacementActivity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidatePrePlacementReport> CandidatePrePlacementReports { get; set; }
    }
}
