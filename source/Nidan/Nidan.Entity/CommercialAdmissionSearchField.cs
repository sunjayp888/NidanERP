namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommercialAdmissionSearchField")]
    public partial class CommercialAdmissionSearchField
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommercialAdmissionId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnquiryId { get; set; }

        public int? BatchId { get; set; }

        public int? SectorId { get; set; }

        public int? CourseId { get; set; }

        public int? StudentTypeId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string Salutation { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string FirstName { get; set; }

        [StringLength(500)]
        public string MiddleName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string LastName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Mobile { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string Gender { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CasteCategoryId { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReligionId { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        public long? FatherMobile { get; set; }

        public long? ResidentialNo { get; set; }

        [Key]
        [Column(Order = 13)]
        public string Address { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TalukaId { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DistrictId { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }

        public int? PinCode { get; set; }

        public string CommunicationAddress { get; set; }

        public int? CommunicationTalukaId { get; set; }

        public int? CommunicationDistrictId { get; set; }

        public int? CommunicationStateId { get; set; }

        public int? CommunicationPinCode { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QualificationId { get; set; }

        [StringLength(500)]
        public string ProfessionalQualification { get; set; }

        [StringLength(500)]
        public string TechnicalQualification { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(100)]
        public string PreTrainingStatus { get; set; }

        public decimal? YearOfExperience { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [StringLength(500)]
        public string EmployerName { get; set; }

        public long? EmployerContactNo { get; set; }

        public string EmployerAddress { get; set; }

        public long? AnnualIncome { get; set; }

        [StringLength(7150)]
        public string SearchField { get; set; }
    }
}
