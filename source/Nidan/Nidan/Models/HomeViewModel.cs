using System.Collections.Generic;
using HR.Entity;

namespace Nidan.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public int AbsencesRequiringApproval { get; set; }
        public int FollowUpCount { get; set; }

        public IEnumerable<int> SelectedCompanyIds { get; set; }        
        public string CompanyIdsArray => SelectedCompanyIds != null ? string.Format("[{0}]", string.Join(",", SelectedCompanyIds)) : "null";

        public IEnumerable<int> SelectedDepartmentIds { get; set; }
        public string DepartmentIdsArray => SelectedDepartmentIds != null ? string.Format("[{0}]", string.Join(",", SelectedDepartmentIds)) : "null";

        //public IEnumerable<Division> Divisions { get; set; }
        public IEnumerable<int> SelectedDivisionIds { get; set; }
        public string DivisionIdsArray => SelectedDivisionIds != null ? string.Format("[{0}]", string.Join(",", SelectedDivisionIds)) : "null";
    }
}