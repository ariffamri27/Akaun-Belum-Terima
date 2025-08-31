using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IMAS.API.LejarAm.Tests.Features.AuditTrail
{
    public class SearchAuditTrailTest : IDisposable
    {
        private readonly FinancialDbContext _db;
        private readonly SearchAuditTrail.Handler _handler;

        public SearchAuditTrailTest()
        {
            var options = new DbContextOptionsBuilder<FinancialDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _db = new FinancialDbContext(options);
            _handler = new SearchAuditTrail.Handler(_db);
        }

        private async Task SeedAsync()
        {
            var fPos = new AuditTrailFilterEntities { ID = Guid.NewGuid(), StatusDokumen = "POS" };
            var fDraft = new AuditTrailFilterEntities { ID = Guid.NewGuid(), StatusDokumen = "DRAFT" };

            var rows = new List<AuditTrailEntities>
            {
                new() { ID = Guid.NewGuid(), NoDoc = "DOC001", TarikhDoc = new DateTime(2025,1,1), Debit=10, Kredit=0, AuditTrailFilterEntitiesID = fPos.ID, AuditTrailFilterEntities = fPos },
                new() { ID = Guid.NewGuid(), NoDoc = "DOC050", TarikhDoc = new DateTime(2025,6,15), Debit=0, Kredit=5, AuditTrailFilterEntitiesID = fDraft.ID, AuditTrailFilterEntities = fDraft },
                new() { ID = Guid.NewGuid(), NoDoc = "ABC999", TarikhDoc = new DateTime(2024,12,31), Debit=7, Kredit=7, AuditTrailFilterEntitiesID = fPos.ID, AuditTrailFilterEntities = fPos },
            };

            _db.AuditTrailFilter.AddRange(fPos, fDraft);
            _db.AuditTrial.AddRange(rows);
            await _db.SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_FilterByYear_ShouldReturnOnlyThatYear()
        {
            await SeedAsync();

            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025 };
            var result = await _handler.Handle(new SearchAuditTrail.Query { Filter = filter }, CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal(2025, r.TarikhDoc!.Value.Year));
        }

        [Fact]
        public async Task Handle_FilterByStatusAndYear_ShouldReturnIntersection()
        {
            await SeedAsync();

            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025, StatusDokumen = "POS" };
            var result = await _handler.Handle(new SearchAuditTrail.Query { Filter = filter }, CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("DOC001", result.First().NoDoc);
        }

        [Fact]
        public async Task Handle_FilterByNoRange_ShouldRespectLexicographicRange()
        {
            await SeedAsync();

            var filter = new AuditTrailFilterDTO { NoMula = "DOC010", NoAkhir = "DOC100" };
            var result = await _handler.Handle(new SearchAuditTrail.Query { Filter = filter }, CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("DOC050", result[0].NoDoc);
        }

        [Fact]
        public async Task Handle_NoFilters_ShouldReturnAll()
        {
            await SeedAsync();

            var filter = new AuditTrailFilterDTO();
            var result = await _handler.Handle(new SearchAuditTrail.Query { Filter = filter }, CancellationToken.None);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Handle_CancellationRequested_ShouldThrow()
        {
            await SeedAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();

            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025 };
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _handler.Handle(new SearchAuditTrail.Query { Filter = filter }, cts.Token));
        }

        public void Dispose() => _db.Dispose();
    }
}
