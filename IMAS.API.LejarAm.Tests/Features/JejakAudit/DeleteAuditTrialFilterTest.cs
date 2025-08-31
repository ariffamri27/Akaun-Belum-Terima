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
    public class DeleteAuditTrailFilterTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly DeleteAuditTrailFilter.Handler _handler;

        public DeleteAuditTrailFilterTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new DeleteAuditTrailFilter.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldDeleteAndReturnTrue()
        {
            var e = new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "A",
                NoAkhir = "B",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrailFilter.Add(e);
            await _db.SaveChangesAsync();

            var result = await _handler.Handle(new DeleteAuditTrailFilter.Command { Id = e.ID }, CancellationToken.None);

            Assert.True(result);
            Assert.Null(await _db.AuditTrailFilter.FindAsync(e.ID));
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new DeleteAuditTrailFilter.Command { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new DeleteAuditTrailFilter.Command { Id = Guid.Empty }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "1",
                NoAkhir = "2",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrailFilter.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new DeleteAuditTrailFilter.Command { Id = e.ID }, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
