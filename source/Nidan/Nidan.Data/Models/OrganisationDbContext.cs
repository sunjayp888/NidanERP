using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EntityFramework.DynamicFilters;
using Nidan.Entity.Interfaces;

namespace Nidan.Data
{
    public class OrganisationDbContext : DbContext
    {
        private int? _organisationId;
        public int? OrganisationId
        {
            get
            {
                return _organisationId;
            }
            set
            {
                SetOrganisationId(value);
            }
        }

        private void SetOrganisationId(int? organisationId)
        {
            _organisationId = organisationId;
            this.SetFilterScopedParameterValue("OrganisationFilterable", "organisationId", _organisationId);
            this.SetFilterGlobalParameterValue("OrganisationFilterable", "organisationId", _organisationId);

            var test = this.GetFilterParameterValue("OrganisationFilterable", "organisationId");
        }

        public OrganisationDbContext()
        {
            Init();
        }

        public OrganisationDbContext(string connectionString) : base(connectionString)
        {
            Init();
        }

        public OrganisationDbContext(string connectionString, DbCompiledModel model) : base(connectionString, model)
        {
            Init();
        }

        public OrganisationDbContext(int organisationId)
        {
            Init();
            OrganisationId = organisationId;
        }

        public OrganisationDbContext(string connectionString, int organisationId) : base(connectionString)
        {
            Init();
            OrganisationId = organisationId;
        }

        public OrganisationDbContext(string connectionString, DbCompiledModel model, int organisationId) : base(connectionString, model)
        {
            Init();
            OrganisationId = organisationId;
        }

        protected internal virtual void Init()
        {
            this.InitializeDynamicFilters();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //if (_organisationId != null)
                modelBuilder.Filter("OrganisationFilterable",
                    (IOrganisationFilterable OrganisationFilterable, int? organisationId) => OrganisationFilterable.OrganisationId == organisationId,
                    () => null);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var createdModifiedEntries = GetCreatedModifiedEntries();

            if (createdModifiedEntries.Any())
            {
                foreach (var entity in createdModifiedEntries)
                {
                    var organisationFilterable = entity.Entity as IOrganisationFilterable;
                    if (organisationFilterable != null && _organisationId.HasValue)
                        organisationFilterable.OrganisationId = _organisationId.Value;
                }
            }

            return base.SaveChanges();
        }

        private IEnumerable<DbEntityEntry> GetCreatedModifiedEntries()
        {
            var createdEntries = ChangeTracker.Entries().Where(
                e => EntityState.Added.HasFlag(e.State) ||
                EntityState.Modified.HasFlag(e.State) ||
                EntityState.Deleted.HasFlag(e.State));
            return createdEntries;
        }
    }
}
