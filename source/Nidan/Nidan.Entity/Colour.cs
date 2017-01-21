using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("Colour")]
    public partial class Colour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colour()
        {
            AbsenceTypes = new HashSet<AbsenceType>();
            //Divisions = new HashSet<Division>();
        }

        public int ColourId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Hex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbsenceType> AbsenceTypes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Division> Divisions { get; set; }
    }
}
