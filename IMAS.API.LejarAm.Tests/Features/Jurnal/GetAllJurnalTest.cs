using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.Jurnal;

namespace IMAS.API.LejarAm.Tests.Features.Jurnal
{
    public class GetAllJurnalTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.GetAllJurnal.Handler _handler;

        public GetAllJurnalTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.GetAllJurnal.Handler(_db);
        }

        [Fact]
        public async Task Handle_WithData_ShouldReturnAll()
        {
            var e1 = new JurnalEntities
            {
                ID = Guid.NewGuid(),
                NoJurnal = "J-1",
                NoRujukan = "R-1",
                TarikhJurnal = new DateTime(2025, 1, 1),
                StatusPos = "BELUM POS",
                StatusSemak = "BELUM SEMAK",
                StatusSah = "BELUM SAH",
                JenisJurnal = "MANUAL",
                SumberTransaksi = "GENERAL LEDGER",
                Keterangan = "K1",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };
            var e2 = new JurnalEntities
            {
                ID = Guid.NewGuid(),
                NoJurnal = "J-2",
                TarikhJurnal = new DateTime(2025, 1, 2),
                StatusPos = "POS",
                StatusSemak = "SEMAK",
                StatusSah = "SAH",
                JenisJurnal = "AUTO",
                SumberTransaksi = "GL",
                Keterangan = "K2",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            _db.Jurnal.AddRange(e1, e2);
            await _db.SaveChangesAsync();

            var list = await _handler.Handle(new Feature.GetAllJurnal.Query(), CancellationToken.None);

            Assert.Equal(2, list.Count);
            Assert.Contains(list, d => d.NoJurnal == "J-1" && d.Keterangan == "K1");
            Assert.Contains(list, d => d.NoJurnal == "J-2" && d.StatusSah == "SAH");
        }

        [Fact]
        public async Task Handle_EmptyDb_ShouldReturnEmpty()
        {
            var list = await _handler.Handle(new Feature.GetAllJurnal.Query(), CancellationToken.None);
            Assert.NotNull(list);
            Assert.Empty(list);
        }

        [Fact]
        public async Task Handle_LargeDataset_ShouldReturnAll()
        {
            var items = new List<JurnalEntities>();
            for (int i = 1; i <= 100; i++)
            {
                items.Add(new JurnalEntities
                {
                    ID = Guid.NewGuid(),
                    NoJurnal = $"J-{i:D3}",
                    TarikhJurnal = DateTime.UtcNow.Date.AddDays(-i),
                    StatusPos = "BELUM POS",
                    StatusSemak = "BELUM SEMAK",
                    StatusSah = "BELUM SAH",
                    JenisJurnal = "MANUAL",
                    SumberTransaksi = "GENERAL LEDGER",
                    Keterangan = $"K{i}",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                });
            }
            _db.Jurnal.AddRange(items);
            await _db.SaveChangesAsync();

            var list = await _handler.Handle(new Feature.GetAllJurnal.Query(), CancellationToken.None);

            Assert.Equal(100, list.Count);
            Assert.All(list, d => Assert.NotEqual(Guid.Empty, d.ID));
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            _db.Jurnal.Add(new JurnalEntities
            {
                ID = Guid.NewGuid(),
                NoJurnal = "J-CAN",
                TarikhJurnal = DateTime.UtcNow.Date,
                Keterangan = "x"
            });
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.GetAllJurnal.Query(), cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
