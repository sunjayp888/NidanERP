namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyFollowUpGrid")]
    public partial class CompanyFollowUpGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyFollowUpId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyBranchId { get; set; }

        [StringLength(500)]
        public string CompanyBranchName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }

        [StringLength(500)]
        public string CompanyName { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime FollowUpDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsClosed { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
