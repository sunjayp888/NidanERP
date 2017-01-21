namespace HR.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CountryPublicHoliday")]
    public partial class CountryPublicHoliday
    {
        public int CountryPublicHolidayId { get; set; }

        public int CountryId { get; set; }

        public int PublicHolidayId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Country Country { get; set; }
        
        public virtual Organisation Organisation { get; set; }

        public virtual PublicHoliday PublicHoliday { get; set; }
    }
}
