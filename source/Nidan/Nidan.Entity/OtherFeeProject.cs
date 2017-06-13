namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherFeeProject")]
    public partial class OtherFeeProject
    {
        public int OtherFeeProjectId { get; set; }

        public int ProjectId { get; set; }

        public int OtherFeeId { get; set; }

        public int? CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual OtherFee OtherFee { get; set; }

        public virtual Project Project { get; set; }
    }
}
