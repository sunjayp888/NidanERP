namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admission")]
    public partial class Admission
    {
        public Admission()
        {
            AdmissionDate = DateTime.UtcNow.Date;
            CandidateAssessments = new HashSet<CandidateAssessment>();
        }

    public int AdmissionId { get; set; }

        [StringLength(500)]
        public string EnrollmentNumber { get; set; }

        public int RegistrationId { get; set; }

        public int? BatchId { get; set; }

        public int CentreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        public int OrganisationId { get; set; }

        public int? PersonnelId { get; set; }

        public int CreatedBy { get; set; }


        public virtual Registration Registration { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Personnel Personnel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssessment> CandidateAssessments { get; set; }
    }
}
