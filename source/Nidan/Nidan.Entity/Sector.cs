namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sector")]
    public partial class Sector
    {
        public Sector()
        {
            Courses = new HashSet<Course>();
            Enquiries=new HashSet<Enquiry>();
            Counsellings=new HashSet<Counselling>();
            Courses=new HashSet<Course>();
            Trainers=new HashSet<Trainer>();
            CentreSectors=new HashSet<CentreSector>();
            StockPurchases=new HashSet<StockPurchase>();
        }

        public int SectorId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public int SchemeId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enquiry> Enquiries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Counselling> Counsellings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trainer> Trainers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentreSector> CentreSectors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockPurchase> StockPurchases { get; set; }
    }
}
