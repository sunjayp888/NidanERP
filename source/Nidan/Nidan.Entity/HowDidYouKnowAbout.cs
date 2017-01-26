namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HowDidYouKnowAbout")]
    public partial class HowDidYouKnowAbout
    {
        [Key]
        public int HowDidYouKnowAboutUsId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
