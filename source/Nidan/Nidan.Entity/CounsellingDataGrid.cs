namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CounsellingDataGrid")]
    public partial class CounsellingDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CounsellingId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(202)]
        public string CounselledBy { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1000)]
        public string CourseOffered { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        public string Remarks { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string RemarkByBranchManager { get; set; }

        [StringLength(100)]
        public string PsychomatricTest { get; set; }

        public int? ConversionProspect { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        public string RemarkByBm { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(3)]
        public string IsRegistrationDone { get; set; }
    }
}
