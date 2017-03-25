using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class RegistrationPaymentReceiptViewModel : BaseViewModel
    {
        public RegistrationPaymentReceipt RegistrationPaymentReceipt { get; set; }
        public SelectList PaymentModes { get; set; }
       // public SelectList Enquiries { get; set; }
        public int EnquiryId { get; set; }
        public int CounsellingId { get; set; }
        public int SectorId { get; set; }
        public int IntrestedCourseId { get; set; }
        public int BatchTimePreferId { get; set; }
        public SelectList Schemes { get; set; }
        public SelectList Sectors { get; set; }
        public SelectList Courses { get; set; }
        public SelectList BatchTimePrefers { get; set; }
        public string CandidateName { get; set; }
    }
}