namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mobilization")]
    public partial class Mobilization
    {
        public int MobilizationId { get; set; }

        public int MobilizationTypeId { get; set; }

        public int EventId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        public int InterestedCourseId { get; set; }

        public int QualificationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string MobilizerStatus { get; set; }

        public int PersonnelId { get; set; }

        [StringLength(500)]
        public string StudentLocation { get; set; }

        [StringLength(1000)]
        public string OtherInterestedCourse { get; set; }

        [Column(TypeName = "date")]
        public DateTime GeneratedDate { get; set; }

        //public virtual Mobilization Mobilization1 { get; set; }

        //public virtual Mobilization Mobilization2 { get; set; }

        public virtual Course Course { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual MobilizationType MobilizationType { get; set; }

        public virtual Personnel Personnel { get; set; }
    }
}
