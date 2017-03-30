using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Entity.Interfaces;

namespace Nidan.Entity
{
    public partial class Session : IOrganisationFilterable
    {
        [NotMapped]
        public string SubjectName { get; set; }

        [NotMapped]
        public string CourseTypeName { get; set; }
    }
}
