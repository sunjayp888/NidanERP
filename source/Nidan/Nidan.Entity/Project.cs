namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public Project()
        {
            ExpenseProjects=new HashSet<ExpenseProject>();
            Activities=new HashSet<Activity>();
            BankDeposites = new HashSet<BankDeposite>();
        }
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpenseProject> ExpenseProjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }
        
          [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankDeposite> BankDeposites { get; set; }
    }

}
