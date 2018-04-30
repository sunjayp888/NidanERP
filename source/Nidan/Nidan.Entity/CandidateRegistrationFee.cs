namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateRegistrationFee")]
    public partial class CandidateRegistrationFee
    {
        [StringLength(353)]
        public string CandidateName { get; set; }

        public long? Mobile { get; set; }

        [Key]
        [Column(Order = 0)]
        public DateTime RegistrationDate { get; set; }

        public decimal? TotalFee { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegistrationId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsAdmissionDone { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        public decimal? RegistrationFeePaid { get; set; }

        public int? CentreId { get; set; }

        [StringLength(202)]
        public string CreatedByName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(970)]
        public string SearchField { get; set; }
    }
}
