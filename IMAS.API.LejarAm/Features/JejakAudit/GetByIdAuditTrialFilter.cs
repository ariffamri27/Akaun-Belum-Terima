using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.AuditTrailFilter
{
    public class GetByIdAuditTrailFilter
    {
        public record Query : IRequest<AuditTrailFilterDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, AuditTrailFilterDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<AuditTrailFilterDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.AuditTrailFilter.FindAsync(new object[] { request.Id }, cancellationToken);
                return entity == null ? null : new AuditTrailFilterDTO
                {
                    ID = entity.ID,
                    TahunKewangan = entity.TahunKewangan,
                    StatusDokumen = entity.StatusDokumen,
                    NoMula = entity.NoMula,
                    NoAkhir = entity.NoAkhir,
                    TarikhMula = entity.TarikhMula,
                    TarikhAkhir = entity.TarikhAkhir,
                };
            }
        }
    }
}
