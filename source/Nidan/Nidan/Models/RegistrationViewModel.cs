using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class RegistrationViewModel : BaseViewModel
    {
        public CandidateFee CandidateFee { get; set; }
        public Registration Registration { get; set; }
        public Enquiry Enquiry { get; set; }
        public CandidateInstallment CandidateInstallment { get; set; }
        public CourseInstallment CourseInstallment { get; set; }
        public SelectList PaymentModes { get; set; }
        public SelectList CounsellingCourse { get; set; }
        public int EnquiryId { get; set; }
        public string StudentCode { get; set; }
        public SelectList Courses { get; set; }
        public SelectList CourseInstallments { get; set; }
        public SelectList BatchTimePrefers { get; set; }
    }
}