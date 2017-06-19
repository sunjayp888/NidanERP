namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilizationSearchField")]
    public partial class MobilizationSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobilizationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string MiddleName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string LastName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        public long? AlternateMobile { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestedCourseId { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QualificationId { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string MobilizerStatus { get; set; }

        [Key]
        [Column("Mobilized By", Order = 12)]
        [StringLength(100)]
        public string Mobilized_By { get; set; }

        [StringLength(500)]
        public string StudentLocation { get; set; }

        [StringLength(1000)]
        public string OtherInterestedCourse { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobilizationTypeId { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "date")]
        public DateTime GeneratedDate { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        [StringLength(4000)]
        public string SearchField { get; set; }
    }
}
