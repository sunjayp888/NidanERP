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
        public Trainer()
        {
            CreatedDate=DateTime.UtcNow.Date;
        }
        public int TrainerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        public int TalukaId { get; set; }

        public int StateId { get; set; }

        public int DistrictId { get; set; }

        [Required]
        [StringLength(12)]
        public string PinCode { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        public long AadharNo { get; set; }

        public long Mobile { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string Certified { get; set; } = "No";

        [StringLength(500)]
        public string CertificationNo { get; set; }

        public int SectorId { get; set; }

        public int? CourseId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int? PersonnelId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Course Course { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Taluka Taluka { get; set; }

        public virtual District District { get; set; }

        public virtual State State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTrainer> SubjectTrainers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchTrainer> BatchTrainers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainerAvailable> TrainerAvailables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchPlanner> BatchPlanners { get; set; }
    }
}
