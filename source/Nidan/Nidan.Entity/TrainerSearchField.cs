namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrainerSearchField")]
    public partial class TrainerSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrainerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string MiddleName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string LastName { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(12)]
        public string PinCode { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TalukaId { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictId { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AadharNo { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(500)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string Certified { get; set; }

        [StringLength(500)]
        public string CertificationNo { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorId { get; set; }

        public int? CourseId { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        public int? PersonnelId { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(3500)]
        public string SearchField { get; set; }
    }
}
