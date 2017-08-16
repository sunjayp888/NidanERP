namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilizationCentreReportMonthWise")]
    public partial class MobilizationCentreReportMonthWise
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        [StringLength(30)]
        public string MonthName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [StringLength(500)]
        public string CentreName { get; set; }

        public int? MobilizationCount { get; set; }

        public int? EnquiryCount { get; set; }

        public int? CounsellingCount { get; set; }

        public int? RegistrationCount { get; set; }

        public int? AdmissionCount { get; set; }

        public decimal? CourseBooking { get; set; }

        public decimal? FeeCollected { get; set; }
    }
}
