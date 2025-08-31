using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrail
{
    public class GetByIdAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly GetByIdAuditTrail.Handler _handler;

        public GetByIdAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new GetByIdAuditTrail.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldReturnDTO()
        {
            var e = new AuditTrailEntities
            {
                ID = Guid.NewGuid(),
                NoDoc = "GBI-1",
                TarikhDoc = new DateTime(2025, 2, 2),
                Debit = 1,
                Kredit = 1
            };
            _db.AuditTrial.Add(e);
            await _db.SaveChangesAsync();

            var dto = await _handler.Handle(new GetByIdAuditTrail.Query { Id = e.ID }, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(e.ID, dto!.ID);
            Assert.Equal("GBI-1", dto.NoDoc);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new GetByIdAuditTrail.Query { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new GetByIdAuditTrail.Query { Id = Guid.Empty }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new AuditTrailEntities { ID = Guid.NewGuid(), NoDoc = "CAN", TarikhDoc = DateTime.UtcNow.Date };
            _db.AuditTrial.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new GetByIdAuditTrail.Query { Id = e.ID }, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
