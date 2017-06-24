namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subject")]
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            CourseSubjects = new HashSet<CourseSubject>();
            Sessions = new HashSet<Session>();
            SubjectTrainers = new HashSet<SubjectTrainer>();
            SubjectCourses=new HashSet<SubjectCourse>();
            BatchAttendances = new HashSet<BatchAttendance>();
        }

        public int SubjectId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int? CourseId { get; set; }

        public int? TrainerId { get; set; }

        public int CourseTypeId { get; set; }

        public int TotalMarks { get; set; }

        public int PassingMarks { get; set; }

        public int NoOfAttemptsAllowed { get; set; }

        public int OrganisationId { get; set; }

        public virtual Course Course { get; set; }

        //public virtual Trainer Trainer { get; set; }

        public virtual CourseType CourseType { get; set; }

        public virtual Organisation Organisation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTrainer> SubjectTrainers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectCourse> SubjectCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchAttendance> BatchAttendances { get; set; }
    }
}
