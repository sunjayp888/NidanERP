namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeType")]
    public partial class FeeType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }
        
    }
}
