using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(MobilizationMetadata))]
    public partial class Mobilization : IOrganisationFilterable
    {
        public static implicit operator Mobilization(Personnel v)
        {
            throw new NotImplementedException();
        }

        private class MobilizationMetadata
        {
            
        }
    }
}
