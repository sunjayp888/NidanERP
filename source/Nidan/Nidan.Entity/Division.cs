namespace HR.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Division")]
    public partial class Division
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Division()
        {
            DivisionCountryAbsencePeriods = new HashSet<DivisionCountryAbsencePeriod>();
            DivisionCountryAbsenceTypeEntitlements = new HashSet<DivisionCountryAbsenceTypeEntitlement>();
            DivisionCountryWorkingPatterns = new HashSet<DivisionCountryWorkingPattern>();
            DivisionSites = new HashSet<DivisionSite>();
            Employments = new HashSet<Employment>();
        }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public int CompanyId { get; set; }

        public int ColourId { get; set; }

        public virtual Colour Colour { get; set; }

        public virtual Company Company { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DivisionCountryAbsencePeriod> DivisionCountryAbsencePeriods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DivisionCountryAbsenceTypeEntitlement> DivisionCountryAbsenceTypeEntitlements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DivisionCountryWorkingPattern> DivisionCountryWorkingPatterns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DivisionSite> DivisionSites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employment> Employments { get; set; }
    }
}
