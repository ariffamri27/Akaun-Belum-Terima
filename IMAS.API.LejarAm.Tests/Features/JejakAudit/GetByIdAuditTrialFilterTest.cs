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
    public class GetByIdAuditTrailFilterTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly GetByIdAuditTrailFilter.Handler _handler;

        public GetByIdAuditTrailFilterTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new GetByIdAuditTrailFilter.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldReturnDTO()
        {
            var e = new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2025,
                StatusDokumen = "DRAFT",
                NoMula = "S",
                NoAkhir = "E",
                TarikhMula = new DateTime(2025, 1, 1),
                TarikhAkhir = new DateTime(2025, 12, 31),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrailFilter.Add(e);
            await _db.SaveChangesAsync();

            var result = await _handler.Handle(new GetByIdAuditTrailFilter.Query { Id = e.ID }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(e.ID, result!.ID);
            Assert.Equal("DRAFT", result.StatusDokumen);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var result = await _handler.Handle(new GetByIdAuditTrailFilter.Query { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnNull()
        {
            var result = await _handler.Handle(new GetByIdAuditTrailFilter.Query { Id = Guid.Empty }, CancellationToken.None);
            Assert.Null(result);
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
                () => _handler.Handle(new GetByIdAuditTrailFilter.Query { Id = e.ID }, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
