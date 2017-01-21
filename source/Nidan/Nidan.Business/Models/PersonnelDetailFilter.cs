using System.Collections.Generic;

namespace Nidan.Business.Models
{
    public class PersonnelDetailFilter
    {
        public List<CompanyFilter> Company { get; set; }
        public List<DepartmentFilter> Department { get; set; }
        public List<DivisionFilter> Division { get; set; }
    }
}
