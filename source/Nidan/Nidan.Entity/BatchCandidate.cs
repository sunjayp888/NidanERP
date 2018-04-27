namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchCandidate")]
    public partial class BatchCandidate
    {
        public int? BatchId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegistrationId { get; set; }

        [StringLength(100)]
        public string StudentCode { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public long? Mobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }
    }
}
