using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.Jurnal;

namespace IMAS.API.LejarAm.Tests.Features.Jurnal
{
    public class CreateJurnalTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.CreateJurnal.Handler _handler;

        public CreateJurnalTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.CreateJurnal.Handler(_db);
        }

        [Fact]
        public async Task Handle_Valid_ShouldCreate_AndReturnDTO()
        {
            var cmd = new Feature.CreateJurnal.Command
            {
                NoJurnal = "J-1001",
                NoRujukan = "REF-1",
                TarikhJurnal = new DateTime(2025, 1, 2),
                StatusPos = "BELUM POS",
                StatusSemak = "BELUM SEMAK",
                StatusSah = "BELUM SAH",
                JenisJurnal = "MANUAL",
                SumberTransaksi = "GENERAL LEDGER",
                Keterangan = "Desc"
            };

            var dto = await _handler.Handle(cmd, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, dto.ID);
            Assert.Equal(cmd.NoJurnal, dto.NoJurnal);

            var saved = await _db.Jurnal.FindAsync(dto.ID);
            Assert.NotNull(saved);
            Assert.Equal("system", saved!.CreatedBy);
            Assert.True(saved.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var cmd = new Feature.CreateJurnal.Command
            {
                NoJurnal = "J-STOP",
                TarikhJurnal = DateTime.UtcNow.Date,
                Keterangan = "Stop"
            };

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(cmd, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
