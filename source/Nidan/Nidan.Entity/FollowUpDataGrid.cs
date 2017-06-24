namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FollowUpDataGrid")]
    public partial class FollowUpDataGrid
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
        public int FollowUpHistoryId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FollowUpId { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string FollowUpType { get; set; }

        public string Remarks { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemarks { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime FollowUpDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        //public virtual FollowUp FollowUp { get; set; }

        //public virtual Course Course { get; set; }
    }
}
