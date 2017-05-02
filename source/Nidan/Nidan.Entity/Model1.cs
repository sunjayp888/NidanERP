namespace Nidan.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CandidateFee> CandidateFees { get; set; }
        public virtual DbSet<CandidateInstallment> CandidateInstallments { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateFee>()
                .Property(e => e.ChequeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CandidateFee>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<CandidateFee>()
                .Property(e => e.StudentCode)
                .IsUnicode(false);

            modelBuilder.Entity<CandidateFee>()
                .Property(e => e.FiscalYear)
                .IsUnicode(false);

            modelBuilder.Entity<CandidateFee>()
                .Property(e => e.Particulars)
                .IsUnicode(false);

            modelBuilder.Entity<CandidateFee>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.CandidateFee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

        }
    }
}
