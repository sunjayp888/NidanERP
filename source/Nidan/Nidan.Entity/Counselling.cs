namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Counselling")]
    public partial class Counselling
    {
        public Counselling()
        {
            CreatedDate=DateTime.UtcNow.Date;
            FollowUpDate= DateTime.UtcNow.AddDays(2);
        }
        public int CounsellingId { get; set; }

        public int EnquiryId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int PersonnelId { get; set; }

        public int CourseOfferedId { get; set; }

        [StringLength(500)]
        public string PreferTiming { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowUpDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string RemarkByBranchManager { get; set; }

        public int SectorId { get; set; }

        [StringLength(100)]
        public string PsychomatricTest { get; set; } = "No";

        public int ConversionProspect { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        [DataType(DataType.MultilineText)]
        public string ClosingRemark { get; set; }

        public bool IsRegistrationDone { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Enquiry Enquiry { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Personnel Personnel { get; set; }
    }
}
