using Nidan.Entity.Dto;

namespace Nidan.Models
{
    public class BaseViewModel
    {
        public string OrganisationName { get; set; }
        public int PersonnelId { get; set; }
        public Permissions Permissions { get; set; }
    }
}