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

        public int EventId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

       
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [Required]
        [StringLength(500)]
        public string InterestedCourse { get; set; }

        [Required]
        [StringLength(255)]
        public string Qualification { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime FollowUpDate { get; set; }

        public string Remark { get; set; }

        [StringLength(100)]
        public string MobilizerStatus { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Event Event { get; set; }

    }
}
