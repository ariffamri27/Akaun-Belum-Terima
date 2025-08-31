using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.PenyelenggaraanLejar;

namespace IMAS.API.LejarAm.Tests.Features.PenyelenggaraanLejar
{
    public class DeletePenyelenggaraanLejarTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.DeletePenyelenggaraanLejar.Handler _handler;

        public DeletePenyelenggaraanLejarTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.DeletePenyelenggaraanLejar.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldDelete_AndReturnTrue()
        {
            var e = new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "DEL-01", Paras = 1 };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.DeletePenyelenggaraanLejar.Command { Id = e.ID };
            var result = await _handler.Handle(cmd, CancellationToken.None);

            Assert.True(result);
            Assert.Null(await _db.PenyelenggaraanLejar.FindAsync(e.ID));
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new Feature.DeletePenyelenggaraanLejar.Command { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnFalse()
        {
            var result = await _handler.Handle(new Feature.DeletePenyelenggaraanLejar.Command { Id = Guid.Empty }, CancellationToken.None);
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "DEL-02" };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.DeletePenyelenggaraanLejar.Command { Id = e.ID }, cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
