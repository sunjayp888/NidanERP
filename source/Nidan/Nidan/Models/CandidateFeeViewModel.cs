using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CandidateFeeViewModel : BaseViewModel
    {
        public CandidateFee CandidateFee { get; set; }
        public int CandidateFeeId { get; set; }
        public int CandidateInstallmentId { get; set; }
        public decimal? CourseFee { get; set; }
        public decimal? TotalPaidFee { get; set; }
        public decimal? BalanceFee { get; set; }
        public string CandidateName { get; set; }
        public SelectList PaymentModes { get; set; }
        public List<CandidateFee> CandidateFeeList { get; set; }
    }
}