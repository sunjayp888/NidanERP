using System.Data.Entity;
using HR.Entity;
using Nidan.Data.Models;

namespace Nidan.Data.Models
{
    /// Ensure the generated NidanDatabase also references OrganisationDbContext
    /// and the OnModelCreating has the following as its last line of code:  base.OnModelCreating(modelBuilder);
    public partial class NidanDatabase : OrganisationDbContext
    {
        public NidanDatabase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Initialise();
        }

        public NidanDatabase(string nameOrConnectionString, int organisationId) : base(nameOrConnectionString, organisationId)
        {
            Initialise();
        }

        private void Initialise()
        {
            //Disable initializer
            Database.SetInitializer<NidanDatabase>(null);
            Database.CommandTimeout = 300;
            Configuration.ProxyCreationEnabled = false;
        }

        // Ensure this function is called with in the generated NidanDatabase
        
        protected void PersonnelModelCreating(DbModelBuilder modelBuilder)
        {            
          
        }

    }
}
