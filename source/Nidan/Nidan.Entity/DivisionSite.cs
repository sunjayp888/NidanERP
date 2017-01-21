namespace HR.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DivisionSite")]
    public partial class DivisionSite
    {
        public int DivisionSiteId { get; set; }

        public int OrganisationId { get; set; }

        public int DivisionId { get; set; }

        public int SiteId { get; set; }

        public virtual Division Division { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Site Site { get; set; }
    }
}
