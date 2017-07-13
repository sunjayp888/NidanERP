namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilizationDataGrid")]
    public partial class MobilizationDataGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobilizationId { get; set; }

        [StringLength(200)]
        public string EventName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1000)]
        public string InterestedCourse { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1000)]
        public string Qualification { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(500)]
        public string StudentLocation { get; set; }

        [StringLength(1000)]
        public string OtherInterestedCourse { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        [StringLength(3580)]
        public string SearchField { get; set; }
    }
}
