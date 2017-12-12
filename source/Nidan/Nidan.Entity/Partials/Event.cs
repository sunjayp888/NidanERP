


namespace Nidan.Entity
{
    using System.ComponentModel.DataAnnotations;
    using Nidan.Entity.Interfaces;

    [MetadataType(typeof(EventTypeMetadata))]
    public partial class Event : IOrganisationFilterable
    {
        private class EventTypeMetadata
        {
            [DataType(DataType.MultilineText)]
            public string Remark { get; set; }
        }
    }
}
