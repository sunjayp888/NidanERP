namespace HR.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CountryAbsenceType")]
    public partial class CountryAbsenceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CountryAbsenceType()
        {
            //DivisionCountryAbsenceTypeEntitlements = new HashSet<DivisionCountryAbsenceTypeEntitlement>();
        }

        public int CountryAbsenceTypeId { get; set; }

        public int CountryId { get; set; }

        public int AbsenceTypeId { get; set; }

        public int OrganisationId { get; set; }

        public virtual AbsenceType AbsenceType { get; set; }

        public virtual Country Country { get; set; }

        public virtual Organisation Organisation { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DivisionCountryAbsenceTypeEntitlement> DivisionCountryAbsenceTypeEntitlements { get; set; }
    }
}
