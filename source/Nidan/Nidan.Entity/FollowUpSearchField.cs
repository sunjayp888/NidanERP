namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FollowUpSearchField")]
    public partial class FollowUpSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FollowUpId { get; set; }

        public DateTime? FollowUpDateTime { get; set; }

        public int? MobilizationId { get; set; }

        public int? EnquiryId { get; set; }

        public int? CounsellingId { get; set; }

        public int? RegistrationId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public string Remark { get; set; }

        public bool? Closed { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime ReadDateTime { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDateTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        public long? Mobile { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IntrestedCourseId { get; set; }

        [StringLength(200)]
        public string FollowUpType { get; set; }

        public long? AlternateMobile { get; set; }

        [StringLength(2000)]
        public string FollowUpURL { get; set; }

        [StringLength(5)]
        public string Close { get; set; }

        public string ClosingRemark { get; set; }

        [StringLength(1580)]
        public string SearchField { get; set; }
    }
}
