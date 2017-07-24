namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expense")]
    public partial class Expense
    {
        public Expense()
        {
            ExpenseProjects=new HashSet<ExpenseProject>();
            CreatedDate=DateTime.UtcNow.Date;
        }
        public int ExpenseId { get; set; }

        [StringLength(500)]
        public string VoucherNumber { get; set; }

        public int ExpenseHeaderId { get; set; }

        public int PersonnelId { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(500)]
        public string CashMemoNumbers { get; set; }

        [Required]
        [StringLength(500)]
        public string RupeesInWord { get; set; }

        public decimal DebitAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string PaidTo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Particulars { get; set; }

        public int PaymentModeId { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual ExpenseHeader ExpenseHeader { get; set; }

        public  virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Personnel Personnel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpenseProject> ExpenseProjects { get; set; }
    }
}
