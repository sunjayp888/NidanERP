using Nidan.Data.Models;


namespace Nidan.Data.Interfaces
{
    public interface INidanDatabaseFactory
    {
        NidanDatabase Create();
        NidanDatabase Create(int organisationId);
    }
}
