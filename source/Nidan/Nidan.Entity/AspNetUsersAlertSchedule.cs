using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("AspNetUsersAlertSchedule")]
    public partial class AspNetUsersAlertSchedule
    {
        public int AspnetUsersAlertScheduleId { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUsersId { get; set; }

        public int AlertId { get; set; }

        public virtual Alert Alert { get; set; }
    }
}
