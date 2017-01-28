namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Colour")]
    public partial class Colour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColourId { get; set; }

        [Required]
        [StringLength(20)]
        public string Hex { get; set; }
    }
}
