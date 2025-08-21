using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using AuditTrailEntity = IMAS.API.LejarAm.Shared.Domain.Entities.AuditTrailEntities;

namespace IMAS.API.LejarAm.Features.AuditTrail
{
    public class CreateAuditTrail
    {
        public record Command : IRequest<AuditTrailDTO>
        {
            public string NoDoc { get; init; } = default!;
            public DateTime TarikhDoc { get; init; }
            public string NamaPenghutang { get; init; } = default!;
            public string Butiran { get; init; }
            public string KodAkaun { get; init; } = default!;
            public string KeteranganAkaun { get; init; } = default!;
            public decimal Debit { get; init; }
            public decimal Kredit { get; init; }
            public Guid? AuditTrailFilterEntitiesID { get; init; }
        }

        public class Handler : IRequestHandler<Command, AuditTrailDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<AuditTrailDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new AuditTrailEntity
                {
                    ID = Guid.NewGuid(),
                    NoDoc = request.NoDoc,
                    TarikhDoc = request.TarikhDoc,
                    NamaPenghutang = request.NamaPenghutang,
                    Butiran = request.Butiran,
                    KodAkaun = request.KodAkaun,
                    KeteranganAkaun = request.KeteranganAkaun,
                    Debit = request.Debit,
                    Kredit = request.Kredit,
                    AuditTrailFilterEntitiesID = request.AuditTrailFilterEntitiesID,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system" // replace with current user if available
                };

                _context.AuditTrial.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return new AuditTrailDTO
                {
                    ID = entity.ID,
                    NoDoc = entity.NoDoc,
                    TarikhDoc = entity.TarikhDoc,
                    NamaPenghutang = entity.NamaPenghutang,
                    Butiran = entity.Butiran,
                    KodAkaun = entity.KodAkaun,
                    KeteranganAkaun = entity.KeteranganAkaun,
                    Debit = entity.Debit,
                    Kredit = entity.Kredit,
                    AuditTrailFilterEntitiesID = entity.AuditTrailFilterEntitiesID
                };
            }
        }
    }
}
