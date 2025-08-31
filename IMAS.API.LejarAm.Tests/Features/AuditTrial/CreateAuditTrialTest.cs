using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Entity = IMAS.API.LejarAm.Shared.Domain.Entities.AuditTrailEntities;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrail
{
    public class CreateAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly CreateAuditTrail.Handler _handler;

        public CreateAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new FinancialDbContext(options);
            _handler = new CreateAuditTrail.Handler(_db);
        }

        [Fact]
        public async Task Handle_Valid_ShouldCreate_AndReturnDTO()
        {
            var cmd = new CreateAuditTrail.Command
            {
                NoDoc = "C-001",
                TarikhDoc = new DateTime(2025, 1, 1),
                NamaPenghutang = "Ali",
                Butiran = "Desc",
                KodAkaun = "1000",
                KeteranganAkaun = "Cash",
                Debit = 12.34m,
                Kredit = 0m
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);
            Assert.NotNull(dto);
            Assert.Equal(cmd.NoDoc, dto.NoDoc);
            Assert.Equal(12.34m, dto.Debit);

            var saved = await _db.AuditTrial.FindAsync(dto.ID);
            Assert.NotNull(saved);
            Assert.Equal("Cash", saved!.KeteranganAkaun);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var cmd = new CreateAuditTrail.Command
            {
                NoDoc = "CAN",
                TarikhDoc = DateTime.UtcNow.Date,
                NamaPenghutang = "X",
                Butiran = "Y",
                KodAkaun = "Z",
                KeteranganAkaun = "ZZ",
                Debit = 1,
                Kredit = 1
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
