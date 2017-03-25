using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class QuestionViewModel : BaseViewModel
    {
        public Question Question { get; set; }
        public SelectList EventFunctionTypes { get; set; }
    }
}