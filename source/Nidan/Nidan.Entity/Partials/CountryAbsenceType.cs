namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CountryAbsenceTypeMetadata))]
    public partial class CountryAbsenceType : IOrganisationFilterable
    {
        private class CountryAbsenceTypeMetadata
        {

        }

    }
}
