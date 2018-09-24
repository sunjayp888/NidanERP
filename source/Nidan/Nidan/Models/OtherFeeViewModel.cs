using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class OtherFeeViewModel : BaseViewModel
    {
        public OtherFee OtherFee { get; set; }
        public SelectList FeeTypes { get; set; }
        public SelectList OnlineExams { get; set; }
        public int EnquiryId { get; set; }
        public Enquiry Enquiry { get; set; }
        public string StudentCode { get; set; }
        public string CandidateName { get; set; }
        public SelectList PaymentModes { get; set; }
    }
}