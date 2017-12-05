using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nidan.Entity;
using Nidan.Entity.Dto;

namespace Nidan.Models
{
    public class CandidateAssessmentQuestionAnswerViewModel : BaseViewModel
    {
        public CandidateAssessmentQuestionAnswer CandidateAssessmentQuestionAnswer { get; set; }
        public CandidateAssessment CandidateAssessment { get; set; }
        public Assessment Assessment { get; set; }
        public ModuleExamQuestionSet ModuleExamQuestionSet { get; set; }
        public ModuleExamSet ModuleExamSet { get; set; }
        public PagedResult<ModuleExamQuestionAnswerGrid> ModuleExamQuestionAnswerGrid { get; set; }
        //public IEnumerator<ModuleExamQuestionAnswerGrid> ModuleExamQuestionAnswerGrid { get; set; }
        public string QuestionDescription { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CandidateAssessmentId { get; set; }
    }
}