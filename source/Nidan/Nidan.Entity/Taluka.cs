namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Taluka")]
    public partial class Taluka
    {
        public int TalukaId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int DistrictId { get; set; }

        public int StateId { get; set; }

        public int OrganisationId { get; set; }

        public virtual District District { get; set; }

        public virtual State State { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
