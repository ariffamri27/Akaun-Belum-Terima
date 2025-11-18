using IMAS.API.AkaunBelumTerima.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence
{
    public class AkaunBelumTerimaDbContext : DbContext
    {
        public AkaunBelumTerimaDbContext(DbContextOptions<AkaunBelumTerimaDbContext> options)
            : base(options) { }

        public DbSet<BillEntities> BillEntities { get; set; }
        public DbSet<PenyelenggaraanPenghutangEntities> PenyelenggaraanPenghutangEntities { get; set; }
        public DbSet<NotaDebitKreditEntities> NotaDebitKreditEntities { get; set; }
        public DbSet<ResitEntities> ResitEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
