using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(RoomMetadata))]
    public partial class Room : IOrganisationFilterable
    {
        private class RoomMetadata
        {
            [Display(Name = "Class Room Type")]
            public int CourseTypeId { get; set; }
        }
    }
}
