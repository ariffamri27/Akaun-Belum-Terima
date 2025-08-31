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
    public class UpdateJurnalTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.UpdateJurnal.Handler _handler;

        public UpdateJurnalTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.UpdateJurnal.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldUpdate_AndReturnDTO()
        {
            var e = new JurnalEntities
            {
                ID = Guid.NewGuid(),
                NoJurnal = "OLD",
                NoRujukan = "R1",
                TarikhJurnal = new DateTime(2025, 1, 1),
                StatusPos = "BELUM POS",
                StatusSemak = "BELUM SEMAK",
                StatusSah = "BELUM SAH",
                JenisJurnal = "MANUAL",
                SumberTransaksi = "GENERAL LEDGER",
                Keterangan = "Old"
            };
            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.UpdateJurnal.Command
            {
                Id = e.ID,
                NoJurnal = "NEW",
                NoRujukan = "R2",
                TarikhJurnal = e.TarikhJurnal.AddDays(1),
                StatusPos = "POS",
                StatusSemak = "SEMAK",
                StatusSah = "SAH",
                JenisJurnal = "AUTO",
                SumberTransaksi = "GL",
                Keterangan = "Updated"
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal("NEW", dto!.NoJurnal);
            Assert.Equal("Updated", dto.Keterangan);

            var saved = await _db.Jurnal.FindAsync(e.ID);
            Assert.NotNull(saved);
            Assert.Equal("NEW", saved!.NoJurnal);
            Assert.Equal("Updated", saved.Keterangan);
            Assert.Equal("system", saved.UpdatedBy);
            Assert.True(saved.UpdatedAt > e.CreatedAt);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var cmd = new Feature.UpdateJurnal.Command
            {
                Id = Guid.NewGuid(),
                NoJurnal = "X",
                TarikhJurnal = DateTime.UtcNow.Date,
                Keterangan = "x"
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new JurnalEntities { ID = Guid.NewGuid(), NoJurnal = "CAN", TarikhJurnal = DateTime.UtcNow.Date, Keterangan = "x" };
            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.UpdateJurnal.Command
            {
                Id = e.ID,
                NoJurnal = "CAN-NEW",
                TarikhJurnal = e.TarikhJurnal,
                Keterangan = "upd"
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
