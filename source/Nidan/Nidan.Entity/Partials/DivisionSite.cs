namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(DivisionSiteMetadata))]
    public partial class DivisionSite : IOrganisationFilterable
    {
        private class DivisionSiteMetadata
        {

        }
    }
}
