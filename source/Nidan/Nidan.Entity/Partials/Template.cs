using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Entity
{
    public partial class Template
    {
        [NotMapped]
        public string FilePath { get; set; }
    }
}
