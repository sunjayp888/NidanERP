namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(DivisionCountryAbsencePeriodMetadata))]
    public partial class DivisionCountryAbsencePeriod : IOrganisationFilterable
    {
        private class DivisionCountryAbsencePeriodMetadata
        {
        }
    }
}
