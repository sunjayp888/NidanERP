namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GovernmentMobilization")]
    public partial class GovernmentMobilization
    {
        public GovernmentMobilization()
        {
            CreatedDate = DateTime.UtcNow.Date;
        }
        public int GovernmentMobilizationId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string FatherName { get; set; }

        public long Mobile { get; set; }

        [Required]
        [StringLength(500)]
        public string ParentMobile { get; set; }

        public int DistrictId { get; set; }

        public int DistrictBlockId { get; set; }

        public int BlockPanchayatId { get; set; }

        [StringLength(1000)]
        public string UserCurrentLocation { get; set; }

        public int QualificationId { get; set; }

        [StringLength(500)]
        public string OtherQualification { get; set; }

        public int ReligionId { get; set; }

        public int CasteCategoryId { get; set; }

        [StringLength(500)]
        public string SubCaste { get; set; }

        [Required]
        [StringLength(100)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofBirth { get; set; }

        public int? Age { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public string MaritalStatus { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual BlockPanchayat BlockPanchayat { get; set; }

        public virtual DistrictBlock DistrictBlock { get; set; }

        public virtual District District { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual CasteCategory CasteCategory { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}

