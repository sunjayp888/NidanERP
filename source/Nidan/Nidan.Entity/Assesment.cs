namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assesment")]
    public partial class Assesment
    {
        public Assesment()
        {
            CreatedDate= DateTime.UtcNow.Date;
            CandidateAssesments = new HashSet<CandidateAssesment>();
        }

        public int AssesmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int AssesmentTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime AssesmentDate { get; set; }

        public int StartTimeHour { get; set; }

        public int StartTimeMinute { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTimeSpan { get; set; }

        public int EndTimeHour { get; set; }

        public int EndTimeMinute { get; set; }

        [Required]
        [StringLength(10)]
        public string EndTimeSpan { get; set; }

        public int CentreId { get; set; }

        public int CourseId { get; set; }

        public int BatchId { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int OrganisationId { get; set; }

        public virtual AssesmentType AssesmentType { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Course Course { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments { get; set; }
    }
}
