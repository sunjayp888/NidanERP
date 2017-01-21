

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(FollowUpMetaData))]
    public partial class FollowUp : IOrganisationFilterable
    {
        private class FollowUpMetaData
        {
            
        }
    }
}