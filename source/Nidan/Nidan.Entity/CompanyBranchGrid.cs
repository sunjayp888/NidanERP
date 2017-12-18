namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBranchGrid")]
    public partial class CompanyBranchGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyBranchId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }

        [StringLength(500)]
        public string CompanyName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string CompanyBranchName { get; set; }

        [StringLength(403)]
        public string Address { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string City { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string HRName1 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string HREmail1 { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long HRContact1 { get; set; }

        [StringLength(100)]
        public string HRName2 { get; set; }

        [StringLength(100)]
        public string HREmail2 { get; set; }

        public long? HRContact2 { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorId { get; set; }

        [StringLength(500)]
        public string SectorName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        public string Remark { get; set; }

    }
}
