using System;

namespace Nidan.Entity.Dto
{
    public class TenantOrganisation : IDisposable
    {
        public int OrganisationId;
        public string HostName;
        public string Email;
        public string FromEmail;
        public string Name;
        public string Telephone;
        public string Theme;

        public void Dispose()
        {
            
        }
    }
}
