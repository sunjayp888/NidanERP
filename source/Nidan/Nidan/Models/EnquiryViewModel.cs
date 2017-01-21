using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Nidan.Entity;

namespace Nidan.Models
{
    public class EnquiryViewModel : BaseViewModel
    {
        public Enquiry Enquiry { get; set; }
    }
}