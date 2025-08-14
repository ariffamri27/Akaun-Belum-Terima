using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using JejakAuditEntity = IMAS.API.LejarAm.Shared.Domain.Entities.JejakAuditEntities;

namespace IMAS.API.LejarAm.Features.JejakAudit
{
    public class CreateJejakAudit
    {
        public record Command : IRequest<JejakAuditDTO>
        {
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

            public async Task<JejakAuditDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new JejakAuditEntity
                {
                    ID = Guid.NewGuid(),
                    TahunKewangan = request.TahunKewangan,
                    StatusDokumen = request.StatusDokumen,
                    NoMula = request.NoMula,
                    NoAkhir = request.NoAkhir,
                    TarikhMula = request.TarikhMula,
                    TarikhAkhir = request.TarikhAkhir,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system" // replace with current user if available
                };

                _context.JejakAudit.Add(entity);
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
