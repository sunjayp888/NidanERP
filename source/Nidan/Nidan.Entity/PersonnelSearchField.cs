namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonnelSearchField")]
    public partial class PersonnelSearchField
    {
        [Key]
        [Column(Order = 0)]
        public int PersonnelId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Forenames { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Surname { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "datetime2")]
        public DateTime DOB { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(12)]
        public string Postcode { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(10)]
        public string NINumber { get; set; }

        [StringLength(10)]
        public string BankAccountNumber { get; set; }

        [StringLength(6)]
        public string BankSortCode { get; set; }

        [StringLength(100)]
        public string BankAccountName { get; set; }

        [StringLength(100)]
        public string BankAddress1 { get; set; }

        [StringLength(100)]
        public string BankAddress2 { get; set; }

        [StringLength(100)]
        public string BankAddress3 { get; set; }

        [StringLength(100)]
        public string BankAddress4 { get; set; }

        [StringLength(12)]
        public string BankPostcode { get; set; }

        [StringLength(15)]
        public string BankTelephone { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(256)]
        public string Email { get; set; }

        public int? CurrentEmploymentId { get; set; }

        public int? CentreId { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(603)]
        public string SearchField { get; set; }
    }
}
