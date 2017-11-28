namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FollowUpHistoryData")]
    public partial class FollowUpHistoryData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FollowUpHistoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FollowUpId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string FollowUpType { get; set; }

        public string Remarks { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemarks { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime FollowUpDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }
    }
}
