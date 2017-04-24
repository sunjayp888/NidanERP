using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class AdmissionViewModel : BaseViewModel
    {
        public Admission Admission { get; set; }
        public RegistrationPaymentReceipt RegistrationPaymentReceipt { get; set; }
        public Batch Batch { get; set; }
        public SelectList PaymentModes { get; set; }
    }
}