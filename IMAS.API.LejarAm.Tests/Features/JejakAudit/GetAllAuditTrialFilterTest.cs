using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrailFilter;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrailFilter
{
    public class GetAllAuditTrailFilterTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly GetAllAuditTrailFilter.Handler _handler;

        public GetAllAuditTrailFilterTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new GetAllAuditTrailFilter.Handler(_db);
        }

        [Fact]
        public async Task Handle_WithData_ShouldReturnAll()
        {
            var list = new List<AuditTrailFilterEntities>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new AuditTrailFilterEntities
                {
                    ID = Guid.NewGuid(),
                    TahunKewangan = 2020 + i,
                    StatusDokumen = i % 2 == 0 ? "DRAFT" : "POS",
                    NoMula = $"M{i}",
                    NoAkhir = $"A{i}",
                    TarikhMula = new DateTime(2025, 1, 1).AddDays(i),
                    TarikhAkhir = new DateTime(2025, 12, 31).AddDays(-i),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                });
            }

            _db.AuditTrailFilter.AddRange(list);
            await _db.SaveChangesAsync();

            var result = await _handler.Handle(new GetAllAuditTrailFilter.Query(), CancellationToken.None);

            Assert.Equal(5, result.Count);
            Assert.Contains(result, r => r.StatusDokumen == "DRAFT");
            Assert.Contains(result, r => r.StatusDokumen == "POS");
        }

        [Fact]
        public async Task Handle_Empty_ShouldReturnEmptyList()
        {
            var result = await _handler.Handle(new GetAllAuditTrailFilter.Query(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            _db.AuditTrailFilter.Add(new AuditTrailFilterEntities
            {
                ID = Guid.NewGuid(),
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "X",
                NoAkhir = "Y",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            });
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new GetAllAuditTrailFilter.Query(), cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
