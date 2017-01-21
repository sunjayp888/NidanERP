using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Nidan.Entity;

namespace Nidan.Models
{
    public class QuestionViewModel : BaseViewModel
    {
        public Question Question { get; set; }
        public List<EventActivityType> EventActivityTypes { get; set; }
    }
}