namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrationGrid")]
    public partial class RegistrationGrid
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(353)]
        public string CandidateName { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime RegistrationDate { get; set; }

        public decimal? TotalFee { get; set; }

        public decimal? RegistrationAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegistrationId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string CentreName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string PaymentMode { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }
    }
}
