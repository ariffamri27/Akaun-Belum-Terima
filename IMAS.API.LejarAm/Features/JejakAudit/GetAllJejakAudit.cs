using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Models;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.JejakAudit
{
    public class GetAllJejakAudit
    {
        public record Query : IRequest<List<JejakAuditDTO>>;

        public class Handler : IRequestHandler<Query, List<JejakAuditDTO>>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<List<JejakAuditDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.JejakAudit
                    .Select(j => new JejakAuditDTO
                    {
                        ID = j.ID,
                        TahunKewangan = j.TahunKewangan,
                        StatusDokumen = j.StatusDokumen,
                        NoMula = j.NoMula,
                        NoAkhir = j.NoAkhir,
                        TarikhMula = j.TarikhMula,
                        TarikhAkhir = j.TarikhAkhir,
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
