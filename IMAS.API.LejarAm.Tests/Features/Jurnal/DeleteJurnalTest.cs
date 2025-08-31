using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.Jurnal;

namespace IMAS.API.LejarAm.Tests.Features.Jurnal
{
    public class DeleteJurnalTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.DeleteJurnal.Handler _handler;

        public DeleteJurnalTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.DeleteJurnal.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldDelete_AndReturnTrue()
        {
            var e = new JurnalEntities { ID = Guid.NewGuid(), NoJurnal = "J-DEL", TarikhJurnal = DateTime.UtcNow.Date, Keterangan = "x" };
            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var result = await _handler.Handle(new Feature.DeleteJurnal.Command { Id = e.ID }, CancellationToken.None);

            Assert.True(result);
            Assert.Null(await _db.Jurnal.FindAsync(e.ID));
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new Feature.DeleteJurnal.Command { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new Feature.DeleteJurnal.Command { Id = Guid.Empty }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new JurnalEntities { ID = Guid.NewGuid(), NoJurnal = "J-DEL2", TarikhJurnal = DateTime.UtcNow.Date, Keterangan = "x" };
            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.DeleteJurnal.Command { Id = e.ID }, cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
