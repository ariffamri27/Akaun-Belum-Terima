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
    public class UpdatePenyelenggaraanLejarTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.UpdatePenyelenggaraanLejar.Handler _handler;

        public UpdatePenyelenggaraanLejarTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.UpdatePenyelenggaraanLejar.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldUpdate_AndReturnDTO()
        {
            var e = new PenyelenggaraanLejarEntities
            {
                ID = Guid.NewGuid(),
                KodAkaun = "OLD",
                Keterangan = "Lama",
                Paras = 1,
                Kategori = "KAT",
                Tahun = 2024,
                Bulan = 12,
                Status = "AKTIF"
            };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.UpdatePenyelenggaraanLejar.Command
            {
                Id = e.ID,
                KodAkaun = "NEW",
                Keterangan = "Baru",
                Paras = 2,
                Kategori = "KATBARU",
                JenisAkaun = "Ledger",
                JenisAkaunParas2 = "Sub",
                JenisAliran = "Debit",
                JenisKedudukanPenyata = "Kunci",
                Tahun = 2025,
                Bulan = 1,
                Status = "AKTIF",
                TarikhTutup = DateTime.UtcNow.Date
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal("NEW", dto.KodAkaun);
            Assert.Equal(2, dto.Paras);
            Assert.Equal(2025, dto.Tahun);

            var saved = await _db.PenyelenggaraanLejar.FindAsync(e.ID);
            Assert.NotNull(saved);
            Assert.Equal("NEW", saved!.KodAkaun);
            Assert.Equal(2, saved.Paras);
            Assert.Equal(2025, saved.Tahun);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldThrowKeyNotFound()
        {
            var cmd = new Feature.UpdatePenyelenggaraanLejar.Command
            {
                Id = Guid.NewGuid(),
                KodAkaun = "X"
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(cmd, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UpdateWithNullables_ShouldPersistNulls_AndMapToDefaultsInDto()
        {
            var e = new PenyelenggaraanLejarEntities
            {
                ID = Guid.NewGuid(),
                KodAkaun = "W-APP",
                Paras = 3,
                Kategori = "KAT",
                Tahun = 2024,
                Bulan = 12,
                Status = "AKTIF"
            };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.UpdatePenyelenggaraanLejar.Command
            {
                Id = e.ID,
                KodAkaun = "W-APP-NEW",
                Paras = null,
                Kategori = null,
                JenisAkaun = null,
                JenisAliran = null,
                JenisKedudukanPenyata = null,
                Tahun = null,
                Bulan = null,
                Status = null,
                Keterangan = null,
                JenisAkaunParas2 = null,
                TarikhTutup = null
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Paras);
            Assert.Equal(string.Empty, dto.Kategori);
            Assert.Equal(string.Empty, dto.JenisAkaun);
            Assert.Equal(string.Empty, dto.JenisAliran);
            Assert.Equal(string.Empty, dto.Status);

            var saved = await _db.PenyelenggaraanLejar.FindAsync(e.ID);
            Assert.Null(saved!.Paras);
            Assert.Null(saved.Kategori);
            Assert.Null(saved.JenisAkaun);
            Assert.Null(saved.JenisAliran);
            Assert.Null(saved.Status);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new PenyelenggaraanLejarEntities { ID = Guid.NewGuid(), KodAkaun = "CAN", Paras = 1 };
            _db.PenyelenggaraanLejar.Add(e);
            await _db.SaveChangesAsync();

            var cmd = new Feature.UpdatePenyelenggaraanLejar.Command { Id = e.ID, KodAkaun = "CAN-NEW", Paras = 2 };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldThrowKeyNotFound()
        {
            var cmd = new Feature.UpdatePenyelenggaraanLejar.Command { Id = Guid.Empty, KodAkaun = "IGNORED" };
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(cmd, CancellationToken.None));
        }

        public void Dispose() => _db.Dispose();
    }
}
