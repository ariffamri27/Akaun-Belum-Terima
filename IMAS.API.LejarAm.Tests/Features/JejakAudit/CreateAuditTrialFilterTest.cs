using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrailFilter;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrailFilter
{
    public class CreateAuditTrailFilterTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly CreateAuditTrailFilter.Handler _handler;

        public CreateAuditTrailFilterTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new CreateAuditTrailFilter.Handler(_db);
        }

        [Fact]
        public async Task Handle_Valid_ShouldCreate_AndReturnDTO()
        {
            var cmd = new CreateAuditTrailFilter.Command
            {
                TahunKewangan = 2025,
                StatusDokumen = "DRAFT",
                NoMula = "001",
                NoAkhir = "010",
                TarikhMula = new DateTime(2025, 1, 1),
                TarikhAkhir = new DateTime(2025, 12, 31)
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(cmd.TahunKewangan, dto.TahunKewangan);
            Assert.Equal(cmd.StatusDokumen, dto.StatusDokumen);

            var saved = await _db.AuditTrailFilter.FindAsync(dto.ID);
            Assert.NotNull(saved);
            Assert.Equal(cmd.NoMula, saved!.NoMula);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var cmd = new CreateAuditTrailFilter.Command
            {
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "100",
                NoAkhir = "200",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(1)
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
