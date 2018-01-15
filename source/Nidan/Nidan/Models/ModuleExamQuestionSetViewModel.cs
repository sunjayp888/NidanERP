using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class ModuleExamQuestionSetViewModel :BaseViewModel
    {
        public ModuleExamQuestionSet ModuleExamQuestionSet { get; set; }
        public ModuleExamSet ModuleExamSet { get; set; }
        public int ModuleExamSetId { get; set; }
        public SelectList Subjects { get; set; }
        public SelectList QuestionTypes { get; set; }
    }

    
}