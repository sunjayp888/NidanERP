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
        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestedCourseId { get; set; }

        public int QualificationId { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string MobilizerStatus { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(500)]
        public string MobilizedBy { get; set; }

        [StringLength(500)]
        public string StudentLocation { get; set; }

        [StringLength(1000)]
        public string OtherInterestedCourse { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime GeneratedDate { get; set; }

        [StringLength(3605)]
        public string SearchField { get; set; }

        public virtual Course Course { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual Mobilization Mobilization1 { get; set; }

        public virtual Mobilization Mobilization2 { get; set; }

        public virtual Organisation Organisation { get; set; }
    }

}
