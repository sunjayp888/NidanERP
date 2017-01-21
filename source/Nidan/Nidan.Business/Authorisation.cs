using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;

namespace Nidan.Business
{
    public class Authorisation : IAuthorisation
    {
        public int? OrganisationId { get; set; }

        public Role Role => (Role)RoleId;

        public int RoleId { get; set; }
    }
}
