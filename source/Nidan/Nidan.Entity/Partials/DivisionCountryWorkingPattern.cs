namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(DivisionCountryWorkingPatternMetadata))]
    public partial class DivisionCountryWorkingPattern : IOrganisationFilterable
    {
        private class DivisionCountryWorkingPatternMetadata
        {
        }
    }
}
