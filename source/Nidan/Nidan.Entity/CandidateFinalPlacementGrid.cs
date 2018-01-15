namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateFinalPlacementGrid")]
    public partial class CandidateFinalPlacementGrid
    {
        public int? CandidateFinalPlacementId { get; set; }

        public int? AdmissionId { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        public int? BatchId { get; set; }

        [StringLength(500)]
        public string BatchName { get; set; }

        public int? CompanyId { get; set; }

        [StringLength(500)]
        public string CompanyName { get; set; }

        public int? CompanyBranchId { get; set; }

        [StringLength(500)]
        public string CompanyBranchName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InterviewDate { get; set; }

        public int? PlacementStatusId { get; set; }

        [StringLength(100)]
        public string PlacementStatusName { get; set; }

        public string Remark { get; set; }

        public bool? IsFinalPlacementDone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        public int? CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        public int? OrganisationId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string IsPostPlacementDone { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1800)]
        public string SearchField { get; set; }
    }
}
