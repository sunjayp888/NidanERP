using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(CandidateInstallmentMetadata))]
    public partial class CandidateInstallment : IOrganisationFilterable
    {
        private class CandidateInstallmentMetadata
        {
            [Display(Name = "Candidate Course Fee")]
            public int CourseFee { get; set; }

            public int DownPayment { get; set; }

            [Display(Name = "Candidate Discount Amount On Course Fee")]
            public int DiscountAmount { get; set; }

            [Display(Name = "Candidate Number Of Installment On Course Fee")]
            public int? NumberOfInstallment { get; set; }

            [Display(Name = "Candidate Lumpsum Amount On Course Fee")]
            public int? LumpsumAmount { get; set; }
        }
    }
}
