using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.PenyelenggaraanLejar;

namespace IMAS.API.LejarAm.Tests.Features.PenyelenggaraanLejar
{
    public class CreatePenyelenggaraanLejarTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.CreatePenyelenggaraanLejar.Handler _handler;

        public CreatePenyelenggaraanLejarTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.CreatePenyelenggaraanLejar.Handler(_db);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreate_AndReturnDTO()
        {
            var cmd = new Feature.CreatePenyelenggaraanLejar.Command
            {
                KodAkaun = "1000-01",
                Keterangan = "Aset",
                Paras = 1,
                Kategori = "Aset",
                JenisAkaun = "Ledger",
                JenisAkaunParas2 = "Sub",
                JenisAliran = "Debit",
                JenisKedudukanPenyata = "Kunci Kira-Kira",
                Tahun = 2025,
                Bulan = 1,
                Status = "AKTIF",
                TarikhTutup = null
            };

            var result = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result.ID);
            Assert.Equal(cmd.KodAkaun, result.KodAkaun);

            var saved = await _db.PenyelenggaraanLejar.FindAsync(result.ID);
            Assert.NotNull(saved);
            Assert.Equal(cmd.Paras, saved!.Paras);
        }

        [Fact]
        public async Task Handle_ParasNull_ShouldPersistNull_AndReturnDtoWithZero()
        {
            var cmd = new Feature.CreatePenyelenggaraanLejar.Command
            {
                KodAkaun = "2000-01",
                Keterangan = "Liabiliti",
                Paras = null
            };

            var result = await _handler.Handle(cmd, CancellationToken.None);

            Assert.Equal(0, result.Paras);

            var entity = await _db.PenyelenggaraanLejar.FindAsync(result.ID);
            Assert.NotNull(entity);
            Assert.Null(entity!.Paras);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var cmd = new Feature.CreatePenyelenggaraanLejar.Command
            {
                KodAkaun = "3000-01",
                Paras = 2
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
