namespace Nidan.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CandidateInstallment")]
    public partial class CandidateInstallment
    {
        public int CandidateInstallmentId { get; set; }

        public int AdmissionId { get; set; }

        public int Fee { get; set; }

        public int DownPayment { get; set; }

        public int? Discount { get; set; }

        public int? DiscountFee { get; set; }

        public int Month { get; set; }

        public int NoOfInstallment { get; set; }

        public int? FirstInstallment { get; set; }

        public int? SecondInstallment { get; set; }

        public int? ThirdInstallment { get; set; }

        public int? FourthInstallment { get; set; }

        public int? FifthInstallment { get; set; }

        public int? SixthInstallment { get; set; }

        public int? SeventhInstallment { get; set; }

        public int? EighthInstallment { get; set; }

        public int? NinethInstallment { get; set; }

        public int? TenthInstallment { get; set; }

        public int? EleventhInstallment { get; set; }

        public int? TwelvethInstallment { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public int BatchId { get; set; }

        public virtual Admission Admission { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
