using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrailFilter;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrailFilter
{
    public class UpdateAuditTrailFilterTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly UpdateAuditTrailFilter.Handler _handler;

        public UpdateAuditTrailFilterTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new UpdateAuditTrailFilter.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldUpdate_AndReturnDTO()
        {
            var e = new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2024,
                StatusDokumen = "DRAFT",
                NoMula = "001",
                NoAkhir = "010",
                TarikhMula = new DateTime(2024, 1, 1),
                TarikhAkhir = new DateTime(2024, 12, 31),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrailFilter.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new UpdateAuditTrailFilter.Command
            {
                Id = e.ID,
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "100",
                NoAkhir = "200",
                TarikhMula = new DateTime(2025, 1, 1),
                TarikhAkhir = new DateTime(2025, 6, 30)
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(2025, dto!.TahunKewangan);
            Assert.Equal("POS", dto.StatusDokumen);

            var saved = await _db.AuditTrailFilter.FindAsync(e.ID);
            Assert.NotNull(saved);
            Assert.Equal("100", saved!.NoMula);
            Assert.Equal("200", saved.NoAkhir);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var cmd = new UpdateAuditTrailFilter.Command
            {
                Id = Guid.NewGuid(),
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "X",
                NoAkhir = "Y",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(5)
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2023,
                StatusDokumen = "BARU",
                NoMula = "S",
                NoAkhir = "E",
                TarikhMula = DateTime.UtcNow.Date.AddMonths(-1),
                TarikhAkhir = DateTime.UtcNow.Date.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrailFilter.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new UpdateAuditTrailFilter.Command
            {
                Id = e.ID,
                TahunKewangan = 2026,
                StatusDokumen = "DRAFT",
                NoMula = "AA",
                NoAkhir = "ZZ",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(10)
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
