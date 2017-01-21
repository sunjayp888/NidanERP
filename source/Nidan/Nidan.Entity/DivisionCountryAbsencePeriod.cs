namespace HR.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DivisionCountryAbsencePeriod")]
    public partial class DivisionCountryAbsencePeriod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DivisionCountryAbsencePeriod()
        {
            PersonnelAbsenceEntitlements = new HashSet<PersonnelAbsenceEntitlement>();
        }

        public int DivisionCountryAbsencePeriodId { get; set; }

        public int OrganisationId { get; set; }

        public int DivisionId { get; set; }

        public int CountryId { get; set; }

        public int AbsencePeriodId { get; set; }

        public virtual AbsencePeriod AbsencePeriod { get; set; }

        public virtual Country Country { get; set; }

        public virtual Division Division { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelAbsenceEntitlement> PersonnelAbsenceEntitlements { get; set; }
    }
}
