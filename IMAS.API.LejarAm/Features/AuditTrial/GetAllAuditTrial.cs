using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.AuditTrail
{
    public class GetAllAuditTrail
    {
        public record Query : IRequest<List<AuditTrailDTO>>;

        public class Handler : IRequestHandler<Query, List<AuditTrailDTO>>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<List<AuditTrailDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.AuditTrial
                    .Select(a => new AuditTrailDTO
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
                        JejakAuditEntitiesID = a.JejakAuditEntitiesID
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
