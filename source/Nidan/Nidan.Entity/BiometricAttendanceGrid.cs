using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Entity
{
    [Table("BiometricAttendanceGrid")]
    public partial class BiometricAttendanceGrid
    {
        [StringLength(100)]
        public string StudentCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LogDateTime { get; set; }

        [StringLength(353)]
        public string CandidateName { get; set; }

        [StringLength(100)]
        public string Direction { get; set; }

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
