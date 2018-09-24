namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GovernmentMobilizationGrid")]
    public partial class GovernmentMobilizationGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GovernmentMobilizationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1000)]
        public string FatherName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string ParentMobile { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictId { get; set; }

        [StringLength(100)]
        public string DistrictName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictBlockId { get; set; }

        [StringLength(500)]
        public string DistrictBlockName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BlockPanchayatId { get; set; }

        [StringLength(500)]
        public string BlockPanchayatName { get; set; }

        [StringLength(1000)]
        public string UserCurrentLocation { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QualificationId { get; set; }

        [StringLength(1000)]
        public string QualificationName { get; set; }

        [StringLength(500)]
        public string OtherQualification { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReligionId { get; set; }

        [StringLength(500)]
        public string ReligionName { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CasteCategoryId { get; set; }

        [StringLength(500)]
        public string CasteCategoryName { get; set; }

        [StringLength(500)]
        public string SubCaste { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "date")]
        public DateTime DateofBirth { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(100)]
        public string MaritalStatus { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CreatedBy { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(4000)]
        public string SearchField { get; set; }
    }
}
