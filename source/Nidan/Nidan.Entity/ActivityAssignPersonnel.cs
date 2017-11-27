namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityAssignPersonnel")]
    public partial class ActivityAssignPersonnel
    {
        public int ActivityAssignPersonnelId { get; set; }

        public int ActivityAssigneeGroupId { get; set; }

        public int PersonnelId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual ActivityAssigneeGroup ActivityAssigneeGroup { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
