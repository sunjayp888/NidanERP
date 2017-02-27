namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CounsellingSearchField")]
    public partial class CounsellingSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CounsellingId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseOfferedId { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string RemarkByBranchManager { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public int? SectorId { get; set; }

        [StringLength(100)]
        public string PsychomatricTest { get; set; }

        public int? SearchField { get; set; }
    }
}
