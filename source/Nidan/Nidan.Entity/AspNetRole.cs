namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetRole
    {
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Discriminator { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
