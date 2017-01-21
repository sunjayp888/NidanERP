using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nidan.Entity
{
    [Table("Personnel")]
    public partial class Personnel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personnel()
        {
        }

        public int PersonnelId { get; set; }

        public int OrganisationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Forenames { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DOB { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Required]
        [StringLength(12)]
        public string Postcode { get; set; }

        [Required]
        [StringLength(15)]
        public string Telephone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
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

        public int? CurrentEmploymentId { get; set; }


        public virtual Organisation Organisation { get; set; }

    }
}
