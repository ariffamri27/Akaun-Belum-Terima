using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.AuditTrail
{
    public class SearchAuditTrail
    {
        public record Query : IRequest<List<AuditTrailDTO>>
        {
            public AuditTrailFilterDTO Filter { get; init; } = default!;
        }

        public class Handler : IRequestHandler<Query, List<AuditTrailDTO>>
        {
            private readonly FinancialDbContext _ctx;
            public Handler(FinancialDbContext ctx) => _ctx = ctx;

            public async Task<List<AuditTrailDTO>> Handle(Query req, CancellationToken ct)
            {
                var f = req.Filter;
                var q = _ctx.AuditTrial.AsQueryable();

                if (f.TahunKewangan.HasValue)
                    q = q.Where(a => a.TarikhDoc.HasValue && a.TarikhDoc.Value.Year == f.TahunKewangan.Value);

                if (!string.IsNullOrWhiteSpace(f.StatusDokumen))
                    q = q.Where(a => a.AuditTrailFilterEntities != null &&
                                     a.AuditTrailFilterEntities.StatusDokumen == f.StatusDokumen);

                if (!string.IsNullOrWhiteSpace(f.NoMula))
                    q = q.Where(a => string.Compare(a.NoDoc, f.NoMula) >= 0);

                if (!string.IsNullOrWhiteSpace(f.NoAkhir))
                    q = q.Where(a => string.Compare(a.NoDoc, f.NoAkhir) <= 0);

                if (f.TarikhMula.HasValue)
                    q = q.Where(a => a.TarikhDoc >= f.TarikhMula);

                if (f.TarikhAkhir.HasValue)
                    q = q.Where(a => a.TarikhDoc <= f.TarikhAkhir);

                return await q.Select(a => new AuditTrailDTO
                {
                    ID = a.ID,
                    NoDoc = a.NoDoc,
                    TarikhDoc = a.TarikhDoc,
                    NamaPenghutang = a.NamaPenghutang,
                    Butiran = a.Butiran,
                    KodAkaun = a.KodAkaun,
                    KeteranganAkaun = a.KeteranganAkaun,
                    Debit = a.Debit,
                    Kredit = a.Kredit,
                    AuditTrailFilterEntitiesID = a.AuditTrailFilterEntitiesID
                }).ToListAsync(ct);
            }
        }
    }
}
