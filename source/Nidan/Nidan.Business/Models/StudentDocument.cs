using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class StudentDocument
    {
        public string StudentCode { get; set; }
        public Guid? Guid { get; set; }
        public int DocumentTypeId { get; set; }
        public string Name { get; set; }
        public bool IsPending { get; set; }
    }
}
