using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrail
{
    public class GetAllAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly GetAllAuditTrail.Handler _handler;

        public GetAllAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new GetAllAuditTrail.Handler(_db);
        }

        [Fact]
        public async Task Handle_WithData_ShouldReturnAll()
        {
            var list = new List<AuditTrailEntities>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(new AuditTrailEntities
                {
                    ID = Guid.NewGuid(),
                    NoDoc = $"DOC{i:000}",
                    TarikhDoc = new DateTime(2025, 1, i),
                    Debit = i,
                    Kredit = 0,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                });
            }
            _db.AuditTrial.AddRange(list);
            await _db.SaveChangesAsync();

            var result = await _handler.Handle(new GetAllAuditTrail.Query(), CancellationToken.None);

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task Handle_Empty_ShouldReturnEmpty()
        {
            var result = await _handler.Handle(new GetAllAuditTrail.Query(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            _db.AuditTrial.Add(new AuditTrailEntities
            {
                ID = Guid.NewGuid(),
                NoDoc = "CANCEL",
                TarikhDoc = DateTime.UtcNow.Date
            });
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new GetAllAuditTrail.Query(), cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
