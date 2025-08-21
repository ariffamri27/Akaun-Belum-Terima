using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.AuditTrail
{
    public class UpdateAuditTrail
    {
        public record Command : IRequest<AuditTrailDTO>
        {
            public Guid Id { get; set; }
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

            public async Task<AuditTrailDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.AuditTrial.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
                if (entity == null) return null;

                entity.NoDoc = request.NoDoc;
                entity.TarikhDoc = request.TarikhDoc;
                entity.NamaPenghutang = request.NamaPenghutang;
                entity.Butiran = request.Butiran;
                entity.KodAkaun = request.KodAkaun;
                entity.KeteranganAkaun = request.KeteranganAkaun;
                entity.Debit = request.Debit;
                entity.Kredit = request.Kredit;
                entity.AuditTrailFilterEntitiesID = request.AuditTrailFilterEntitiesID;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

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
