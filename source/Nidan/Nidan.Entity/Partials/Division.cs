namespace HR.Entity
{
    using Interfaces;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(DivisionMetadata))]
    public partial class Division : IOrganisationFilterable
    {
        [NotMapped]
        public string Hex => Colour?.Hex ?? string.Empty;

        private class DivisionMetadata
        {
            [DisplayName("Division Id")]
            public int DivisionId { get; set; }

            [DisplayName("Company")]
            public int CompanyId { get; set; }

            [Display(Name = "Colour")]
            [Required]
            public int? ColourId { get; set; }

        }
    }

}

