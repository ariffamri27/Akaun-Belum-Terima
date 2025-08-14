using IMAS.API.LejarAm.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Persistence
{
    public class FinancialDbContext : DbContext
    {
        public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
            : base(options) { }

        public DbSet<JurnalEntities> Jurnal { get; set; }
        public DbSet<PenyelenggaraanLejarEntities> PenyelenggaraanLejar { get; set; }
        public DbSet<JejakAuditEntities> JejakAudit { get; set; }
        public DbSet<AuditTrailEntities> AuditTrial { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add model configurations here if needed
        }
    }
}
