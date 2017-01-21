

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(EnquiryMetadata))]
    public partial class Enquiry : IOrganisationFilterable
    {
        private class EnquiryMetadata
        {
           
        }
    }
}
