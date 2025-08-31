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
    public class GetByIdPenyelenggaraanLejarTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.GetByIdPenyelenggaraanLejar.Handler _handler;

        public GetByIdPenyelenggaraanLejarTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.GetByIdPenyelenggaraanLejar.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldReturnDTO()
        {
            var e = new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "EX-01", Paras = 2, Kategori = null };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var dto = await _handler.Handle(new Feature.GetByIdPenyelenggaraanLejar.Query { Id = e.ID }, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(e.ID, dto!.ID);
            Assert.Equal("EX-01", dto.KodAkaun);
            Assert.Equal(2, dto.Paras);
            Assert.Equal(string.Empty, dto.Kategori);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new Feature.GetByIdPenyelenggaraanLejar.Query { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new Feature.GetByIdPenyelenggaraanLejar.Query { Id = Guid.Empty }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "CANCEL" };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.GetByIdPenyelenggaraanLejar.Query { Id = e.ID }, cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
