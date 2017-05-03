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
        public SelectList PaymentModes { get; set; }
    }
}