using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Business.Models
{
    public class CandidatePrePlacementSchedule
    {
        public bool IsCVMakingDone { get; set; }
        public bool IsInterviewTechniqueDone { get; set; }
        public bool IsTechnicalKnowledgeDone { get; set; }
        public bool IsCandidateProfilingDone { get; set; }
        public string CandidateName { get; set; }
    }
}
