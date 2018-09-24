namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementReport")]
    public partial class CandidatePrePlacementReport
    {
        public CandidatePrePlacementReport()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int CandidatePrePlacementReportId { get; set; }

        public int CandidatePrePlacementId { get; set; }

        public int AdmissionId { get; set; }

        [StringLength(500)]
        public string StudentCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActualStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActualEndDate { get; set; }

        public int? MarkObtained { get; set; }

        public int? TotalMark { get; set; }

        public string Remark { get; set; }

        public bool IsDocumentUploaded { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual CandidatePrePlacement CandidatePrePlacement { get; set; }

        public virtual Admission Admission { get; set; }
    }
}
