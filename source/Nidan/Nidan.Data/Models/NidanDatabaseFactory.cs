using System;
using Nidan.Data.Interfaces;

namespace Nidan.Data.Models
{
    public class NidanDatabaseFactory : INidanDatabaseFactory
    {
        public string NameOrConnectionString { get; }

        public NidanDatabaseFactory(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
        }

        public NidanDatabase Create()
        {
            ValidateConnectionString();
            return new NidanDatabase(NameOrConnectionString);
        }

        public NidanDatabase Create(int organisationId)
        {
            ValidateConnectionString();
            return new NidanDatabase(NameOrConnectionString, organisationId);
        }

        private void ValidateConnectionString()
        {
            if (string.IsNullOrWhiteSpace(NameOrConnectionString))
                throw new NullReferenceException("OmbrosDatabaseFactory expects a valid NameOrConnectionString");
        }
    }
}
