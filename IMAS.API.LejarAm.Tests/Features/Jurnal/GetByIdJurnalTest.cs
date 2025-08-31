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
    public class GetByIdJurnalTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly Feature.GetByIdJurnal.Handler _handler;

        public GetByIdJurnalTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new Feature.GetByIdJurnal.Handler(_db);
        }

        [Fact]
        public async Task Handle_Existing_ShouldReturnDTO()
        {
            var e = new JurnalEntities
            {
                ID = Guid.NewGuid(),
                NoJurnal = "J-EX",
                NoRujukan = "R-EX",
                TarikhJurnal = new DateTime(2025, 2, 1),
                Keterangan = "Exist"
            };

            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var dto = await _handler.Handle(new Feature.GetByIdJurnal.Query { Id = e.ID }, CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(e.ID, dto!.ID);
            Assert.Equal("J-EX", dto.NoJurnal);
            Assert.Equal("Exist", dto.Keterangan);
        }

        [Fact]
        public async Task Handle_NonExisting_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new Feature.GetByIdJurnal.Query { Id = Guid.NewGuid() }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_EmptyGuid_ShouldReturnNull()
        {
            var dto = await _handler.Handle(new Feature.GetByIdJurnal.Query { Id = Guid.Empty }, CancellationToken.None);
            Assert.Null(dto);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            var e = new JurnalEntities { ID = Guid.NewGuid(), NoJurnal = "J-CAN", TarikhJurnal = DateTime.UtcNow.Date, Keterangan = "x" };
            _db.Jurnal.Add(e);
            await _db.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new Feature.GetByIdJurnal.Query { Id = e.ID }, cts.Token)
            );
        }

        public void Dispose() => _db.Dispose();
    }
}
