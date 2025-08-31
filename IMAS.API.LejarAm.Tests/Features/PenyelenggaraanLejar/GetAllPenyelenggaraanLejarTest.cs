using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.PenyelenggaraanLejar;

namespace IMAS.API.LejarAm.Tests.Features.PenyelenggaraanLejar
{
    public class GetAllPenyelenggaraanLejarTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.GetAllPenyelenggaraanLejar.Handler _handler;

        public GetAllPenyelenggaraanLejarTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.GetAllPenyelenggaraanLejar.Handler(_db);
        }

        [Fact]
        public async Task Handle_WithData_ShouldReturnAll_DTOs()
        {
            var e1 = new PenyelenggaraanLejarEntities
            {
                ID = Guid.NewGuid(),
                KodAkaun = "1000-01",
                Keterangan = "Aset",
                Paras = 1,
                Kategori = null,
                JenisAkaun = "Ledger",
                JenisAliran = null,
                JenisKedudukanPenyata = "Kunci",
                Tahun = 2025,
                Bulan = 1,
                Status = null
            };
            var e2 = new PenyelenggaraanLejarEntities
            {
                ID = Guid.NewGuid(),
                KodAkaun = "2000-01",
                Keterangan = "Liabiliti",
                Paras = null
            };

            _db.PenyelenggaraanLejar.AddRange(e1, e2);
            await _db.SaveChangesAsync();

            var list = await _handler.Handle(new Feature.GetAllPenyelenggaraanLejar.Query(), CancellationToken.None);

            Assert.Equal(2, list.Count);

            var d1 = list.Single(x => x.ID == e1.ID);
            Assert.Equal(string.Empty, d1.Kategori);
            Assert.Equal(string.Empty, d1.JenisAliran);
            Assert.Equal(string.Empty, d1.Status);
            Assert.Equal(1, d1.Paras);

            var d2 = list.Single(x => x.ID == e2.ID);
            Assert.Equal(0, d2.Paras);
        }

        [Fact]
        public async Task Handle_EmptyDb_ShouldReturnEmptyList()
        {
            var list = await _handler.Handle(new Feature.GetAllPenyelenggaraanLejar.Query(), CancellationToken.None);
            Assert.NotNull(list);
            Assert.Empty(list);
        }

        [Fact]
        public async Task Handle_LargeDataset_ShouldReturnAll()
        {
            var items = new List<PenyelenggaraanLejarEntities>();
            for (int i = 1; i <= 100; i++)
            {
                items.Add(new PenyelenggaraanLejarEntities
                {
                    ID = Guid.NewGuid(),
                    KodAkaun = $"ACC-{i:D3}",
                    Paras = i % 3,
                    Tahun = 2025,
                    Bulan = (i % 12) + 1
                });
            }

            _db.PenyelenggaraanLejar.AddRange(items);
            await _db.SaveChangesAsync();

            var list = await _handler.Handle(new Feature.GetAllPenyelenggaraanLejar.Query(), CancellationToken.None);
            Assert.Equal(100, list.Count);
            Assert.All(list, d => Assert.NotEqual(Guid.Empty, d.ID));
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            _db.PenyelenggaraanLejar.Add(new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "X" });
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.GetAllPenyelenggaraanLejar.Query(), cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
