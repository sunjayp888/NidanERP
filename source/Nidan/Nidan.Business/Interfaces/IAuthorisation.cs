using Nidan.Entity.Dto;

namespace Nidan.Business.Interfaces
{
    public interface IAuthorisation
    {
        int? OrganisationId { get; set; }        
        int RoleId { get; set; }
        Role Role { get; }
    }
}
