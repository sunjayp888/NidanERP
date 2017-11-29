namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BiometricAttendanceGrid")]
    public partial class BiometricAttendanceGrid
    {
        [StringLength(50)]
        public string StudentCode { get; set; }

        public DateTime? LogDateTime { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [StringLength(50)]
        public string Direction { get; set; }

        public int? BatchId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }
    }
}
