using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class BatchMonth
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int Month { get; set; }
        public int Holiday { get; set; }
        public int NumberOfInstallment { get; set; }
        public int InstallmentAmount { get; set; }
        public int MaximumCandidate { get; set; }
        public int NumberOfBatch { get; set; }
    }
}
