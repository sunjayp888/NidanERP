using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    [MetadataType(typeof(SubjectMetadata))]
    public partial class Subject : IOrganisationFilterable
    {
        private class SubjectMetadata
        {
            [Display(Name = "Course")]
            public int CourseId { get; set; }

            [Display(Name = "Trainer")]
            public int TrainerId { get; set; }

            [Display(Name = "Course Type")]
            public int CourseTypeId { get; set; }

            [Display(Name = "Total Marks")]
            public int TotalMarks { get; set; }

            [Display(Name = "Passing Marks")]
            public int PassingMarks { get; set; }

            [Display(Name = "Number Of Attempts Allowed")]
            public int NoOfAttemptsAllowed { get; set; }
        }
    }
}
