using System.Collections.Generic;
using Nidan.Entity.Dto;

namespace Nidan.Business.Interfaces
{
    public interface ITenantOrganisationService
    {
        IEnumerable<TenantOrganisation> RetrieveTenantOrganisations();
    }
}
