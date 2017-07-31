using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class CandidateFeeReceipt
    {
        public string OrganisationName { get; set; }
        public string CentreAddress { get; set; }
        public string CentreName { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }
        public string CandidateAddress { get; set; }
        public string MobileNumber { get; set; }
        public string CourseName { get; set; }
        public string FeeTypeName { get; set; }
        public string CourseDuration { get; set; }
        public string TotalCourseFee { get; set; }
        public string RecievedAmount { get; set; }
        public string PaymentDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string TotalInstallment { get; set; }
        public string InstallmentNumber { get; set; }
        public string State { get; set; }
        public string Gstin { get; set; }
        public string GstStateCode { get; set; }
        public string FatherName { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNumber { get; set; }
        public string RupeesInWords { get; set; }
    }
}
