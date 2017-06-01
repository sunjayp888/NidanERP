namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Holiday")]
    public partial class Holiday
    {
        public int HolidayId { get; set; }

        public int CentreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime HolidayDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        [StringLength(100)]
        public string WeekDay { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }
    }
}
