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
        public Mobilization()
        {
            CreatedDate = DateTime.UtcNow.Date;
            FollowUpDate= DateTime.UtcNow.Date.AddDays(2);
        }
        public int MobilizationId { get; set; }

        public int EventId { get; set; }

        public int OrganisationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public int CentreId { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public long Mobile { get; set; }

        public long? AlternateMobile { get; set; }

        public int InterestedCourseId { get; set; }

        public int QualificationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string MobilizerStatus { get; set; }

        [StringLength(500)]
        public string StudentLocation { get; set; }

        [StringLength(1000)]
        public string OtherInterestedCourse { get; set; }

        [Column(TypeName = "date")]
        public DateTime GeneratedDate { get; set; }

        public int MobilizationTypeId { get; set; }

        public int PersonnelId { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        public virtual Course Course { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual MobilizationType MobilizationType { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Event Event { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
