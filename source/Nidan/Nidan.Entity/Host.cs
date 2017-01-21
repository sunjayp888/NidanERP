using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("Host")]
    public partial class Host
    {
        public short HostId { get; set; }

        [Required]
        [StringLength(100)]
        public string HostName { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
