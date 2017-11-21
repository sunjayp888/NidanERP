using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class AvailablePettyCashReport
    {
        public int CentreId { get; set; }
        public string CentreName { get; set; }
        public decimal AvailablePettyCash { get; set; }
    }
}
