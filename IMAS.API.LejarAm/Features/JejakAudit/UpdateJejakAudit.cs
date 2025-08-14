using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.JejakAudit
{
    public class UpdateJejakAudit
    {
        public record Command : IRequest<JejakAuditDTO>
        {
            public Guid Id { get; set; }
            public int TahunKewangan { get; init; }
            public string StatusDokumen { get; init; } = default!;
            public string NoMula { get; init; } = default!;
            public string NoAkhir { get; init; } = default!;
            public DateTime TarikhMula { get; init; }
            public DateTime TarikhAkhir { get; init; }
            public ICollection<AuditTrailDTO>? AuditTrails { get; init; }
        }

        public class Handler : IRequestHandler<Command, JejakAuditDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<JejakAuditDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.JejakAudit.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
                if (entity == null) return null;

                entity.TahunKewangan = request.TahunKewangan;
                entity.StatusDokumen = request.StatusDokumen;
                entity.NoMula = request.NoMula;
                entity.NoAkhir = request.NoAkhir;
                entity.TarikhMula = request.TarikhMula;
                entity.TarikhAkhir = request.TarikhAkhir;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new JejakAuditDTO
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
