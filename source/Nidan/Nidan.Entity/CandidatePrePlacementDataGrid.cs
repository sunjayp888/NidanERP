namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidatePrePlacementDataGrid")]
    public partial class CandidatePrePlacementDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidatePrePlacementReportId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CandidatePrePlacementId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string PrePlacementActivityName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(500)]
        public string StudentCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActualStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActualEndDate { get; set; }

        public int? MarkObtained { get; set; }

        public int? TotalMark { get; set; }

        public string Remark { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool IsDocumentUploaded { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(3)]
        public string IsDocumentUploadedDone { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
