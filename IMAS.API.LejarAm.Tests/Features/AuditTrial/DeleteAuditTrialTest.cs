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
    public class DeleteAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly DeleteAuditTrail.Handler _handler;

        public DeleteAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new FinancialDbContext(options);
            _handler = new DeleteAuditTrail.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldDeleteAndReturnTrue()
        {
            var e = new AuditTrailEntities
            {
                ID = Guid.NewGuid(),
                NoDoc = "D-1",
                TarikhDoc = DateTime.UtcNow.Date,
                Debit = 1,
                Kredit = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrial.Add(e);
            await _db.SaveChangesAsync();

            var ok = await _handler.Handle(new DeleteAuditTrail.Command { Id = e.ID }, CancellationToken.None);
            Assert.True(ok);
            Assert.Null(await _db.AuditTrial.FindAsync(e.ID));
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnFalse()
        {
            var ok = await _handler.Handle(new DeleteAuditTrail.Command { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.False(ok);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnFalse()
        {
            var ok = await _handler.Handle(new DeleteAuditTrail.Command { Id = Guid.Empty }, CancellationToken.None);
            Assert.False(ok);
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
                () => _handler.Handle(new DeleteAuditTrail.Command { Id = e.ID }, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
