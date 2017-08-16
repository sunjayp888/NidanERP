namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gst")]
    public partial class Gst
    {
        public int GstId { get; set; }

        [Required]
        [StringLength(50)]
        public string GstNumber { get; set; }

        public int StateId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public virtual State State { get; set; }
    }
}
