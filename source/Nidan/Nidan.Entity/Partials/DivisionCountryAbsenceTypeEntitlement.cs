namespace HR.Entity
{
    using Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(DivisionCountryAbsenceTypeEntitlementMetadata))]
    public partial class DivisionCountryAbsenceTypeEntitlement : IOrganisationFilterable
    {
        [NotMapped]
        public DateTime? StartDate { get; set; }

        [NotMapped]
        public DateTime? EndDate { get; set; }

        [NotMapped]
        [Display(Name = "Has Entitlement")]
        public bool HasEntitlement => DivisionCountryAbsenceTypeEntitlementId > 0;

        private class DivisionCountryAbsenceTypeEntitlementMetadata 
        {
            [Display(Name ="Frequency")]
            public int FrequencyId { get; set; }

            [Display(Name = "Maximum Carry Forward")]
            [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
            public double MaximumCarryForward { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
            public double Entitlement { get; set; }

            [Display(Name = "Is Unplanned")]
            public bool IsUnplanned { get; set; }

            [Display(Name = "Is Paid")]
            public bool IsPaid { get; set; }
        }
    }
}
