

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(AbsenceTypeMetadata))]
    public partial class AbsenceType : IOrganisationFilterable
    {

        [NotMapped]
        public string Hex => Colour?.Hex ?? string.Empty;

        private class AbsenceTypeMetadata
        {
            [Display(Name = "Colour")]
            [Required]
            public int? ColourId { get; set; }
        }
    }
}