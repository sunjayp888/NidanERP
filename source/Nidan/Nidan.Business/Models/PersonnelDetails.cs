namespace Nidan.Business.Models
{
    public class PersonnelDetails
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public int? CompanyId { get; set; }
        string _CompanyName;
        public string CompanyName
        {
            get
            {
                return "Company: " + _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        public int? DepartmentId { get; set; }
        string _DepartmentName;
        public string DepartmentName
        {
            get
            {
                return "Department: " + _DepartmentName;
            }
            set
            {
                _DepartmentName = value;
            }
        }
        public int? DivisionId { get; set; }
        string _DivisionName;
        public string DivisionName
        {
            get
            {
                return "Division: " + _DivisionName;
            }
            set
            {
                _DivisionName = value;
            }
        }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Name
        {
            get
                {
                    return Forename + ' ' + Surname;
                }
        }
        string _JobTitle;
        public string JobTitle
        {
            get
            {
                return "Job Title: " + _JobTitle;
            }
            set
            {
                _JobTitle = value;
            }
        }
        public string Photo { get; set; }
        public string Hex { get; set; }
        public bool showLink { get; set; }
    }
}
