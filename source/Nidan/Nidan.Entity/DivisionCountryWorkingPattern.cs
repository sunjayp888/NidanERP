namespace HR.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DivisionCountryWorkingPattern")]
    public partial class DivisionCountryWorkingPattern
    {
        public int DivisionCountryWorkingPatternId { get; set; }

        public int OrganisationId { get; set; }

        public int DivisionId { get; set; }

        public int CountryId { get; set; }

        public int WorkingPatternId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Division Division { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual WorkingPattern WorkingPattern { get; set; }
    }
}
