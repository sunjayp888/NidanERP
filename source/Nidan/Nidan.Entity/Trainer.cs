namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trainer")]
    public partial class Trainer
    {
        public int TrainerId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        public long AadharNo { get; set; }

        public long Mobile { get; set; }

        [Required]
        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string Certified { get; set; } = "No";

        [StringLength(500)]
        public string CertificationNo { get; set; }

        public int SectorId { get; set; }

        public int CourseId { get; set; }

        public int OrganisationId { get; set; }

        public int? PersonnelId { get; set; }

        public int CentreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Course Course { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Centre Centre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Batch> Batches { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Subject> Subjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTrainer> SubjectTrainers { get; set; }

    }
}
