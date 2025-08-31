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
    public class UpdateAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly UpdateAuditTrail.Handler _handler;

        public UpdateAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new UpdateAuditTrail.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldUpdate_AndReturnDTO()
        {
            var e = new AuditTrailEntities
            {
                ID = Guid.NewGuid(),
                NoDoc = "OLD-1",
                TarikhDoc = new DateTime(2024, 12, 31),
                NamaPenghutang = "P1",
                Butiran = "Old",
                KodAkaun = "1000",
                KeteranganAkaun = "Cash",
                Debit = 1,
                Kredit = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            _db.AuditTrial.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new UpdateAuditTrail.Command
            {
                Id = e.ID,
                NoDoc = "NEW-1",
                TarikhDoc = new DateTime(2025, 1, 1),
                NamaPenghutang = "P2",
                Butiran = "New desc",
                KodAkaun = "2000",
                KeteranganAkaun = "AR",
                Debit = 5,
                Kredit = 2
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal("NEW-1", dto!.NoDoc);
            Assert.Equal("AR", dto.KeteranganAkaun);

            var saved = await _db.AuditTrial.FindAsync(e.ID);
            Assert.NotNull(saved);
            Assert.Equal(5, saved!.Debit);
            Assert.Equal(2, saved.Kredit);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var cmd = new UpdateAuditTrail.Command
            {
                Id = Guid.NewGuid(),
                NoDoc = "X",
                TarikhDoc = DateTime.UtcNow.Date,
                NamaPenghutang = "N",
                Butiran = "B",
                KodAkaun = "1",
                KeteranganAkaun = "2",
                Debit = 1,
                Kredit = 1
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new AuditTrailEntities { ID = Guid.NewGuid(), NoDoc = "CAN", TarikhDoc = DateTime.UtcNow.Date, Debit = 1 };
            _db.AuditTrial.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new UpdateAuditTrail.Command
            {
                Id = e.ID,
                NoDoc = "CAN-NEW",
                TarikhDoc = DateTime.UtcNow.Date,
                NamaPenghutang = "X",
                Butiran = "Y",
                KodAkaun = "Z",
                KeteranganAkaun = "ZZ",
                Debit = 2,
                Kredit = 3
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
