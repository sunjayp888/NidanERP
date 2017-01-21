namespace HR.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DivisionCountryAbsenceTypeEntitlement")]
    public partial class DivisionCountryAbsenceTypeEntitlement
    {
        public int DivisionCountryAbsenceTypeEntitlementId { get; set; }

        public int OrganisationId { get; set; }

        public int DivisionId { get; set; }

        public int CountryAbsenceTypeId { get; set; }

        public int FrequencyId { get; set; }

        public double Entitlement { get; set; }

        public double MaximumCarryForward { get; set; }

        public bool IsUnplanned { get; set; }

        public bool IsPaid { get; set; }

        public virtual CountryAbsenceType CountryAbsenceType { get; set; }

        public virtual Division Division { get; set; }

        public virtual Frequency Frequency { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
