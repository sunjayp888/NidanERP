using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(Entity.ModuleExamSet.ModuleExamSetMetadata))]
    public partial class ModuleExamSet : IOrganisationFilterable
    {
        private class ModuleExamSetMetadata
        {
            [Display(Name = "Question Set Name")]
            public string QuestionSetName { get; set; }

            [Display(Name = "Subject")]
            public int SubjectId { get; set; }
        }
    }
}
