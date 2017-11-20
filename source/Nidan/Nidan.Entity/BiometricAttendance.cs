namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BiometricAttendance")]
    public partial class BiometricAttendance
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string StudentCode { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime LogDateTime { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Direction { get; set; }
    }
}
