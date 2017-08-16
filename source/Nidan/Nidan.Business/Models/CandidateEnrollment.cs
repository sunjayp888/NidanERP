using System;
using System.Collections.Generic;
using iTextSharp.text;

namespace Nidan.Business.Models
{
    public class CandidateEnrollment
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
        public string BatchStartDate { get; set; }
        public string BatchEndDate { get; set; }
        public List<FeeDetail> FeeDetails { get; set; }
        public List<ModuleDetail> Modules { get; set; }
        public string TotalAmountPaid { get; set; }
        public string BalanceFee { get; set; }
        public string State { get; set; }
        public string Gstin { get; set; }
        public string GstStateCode { get; set; }
        public string FatherName { get; set; }
        public string EnrollmentDate { get; set; }
    }

    public class FeeDetail
    {
        public string InstallmentDate { get; set; }
        public string InstallmentAmount { get; set; }
        public string Status { get; set; }
        public string Paymentdate { get; set; }
        public string Type { get; set; }
        public string AmountPaid { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNumber { get; set; }
        public string RupeesInWords { get; set; }
        public string PaymentDetail => PaymentMode == "Cheque"
            ? string.Format("Cheque \n Cheque No : {0} \n ChequeDate : {1} \n Bank Name : {2}", ChequeNumber,
                ChequeDate, BankName) : Paymentdate != String.Empty ? "Cash" : String.Empty;
    }

    public class ModuleDetail
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string TotalMarks { get; set; }
        public string PassingMarks { get; set; }
        public string AttemptAllowed { get; set; }
        public string NumberOfHours { get; set; }
    }
}