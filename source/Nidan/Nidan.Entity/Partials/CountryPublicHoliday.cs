namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CountryPublicHolidayMetadata))]
    public partial class CountryPublicHoliday : IOrganisationFilterable
    {
        private class CountryPublicHolidayMetadata
        {

        }
    }
}
