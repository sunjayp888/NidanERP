using System.Collections.Generic;
using Nidan.Entity.Dto;

namespace Nidan.Interfaces
{
    public interface ITenantsService
    {        
        IEnumerable<TenantOrganisation> TenantOrganisations();
        TenantOrganisation CurrentTenantOrganisation(string hostname);
    }
}
