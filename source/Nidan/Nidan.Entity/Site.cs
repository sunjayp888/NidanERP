namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Site")]
    public partial class Site
    {
        public int SiteId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public int OrganisationId { get; set; }
    }
}
