using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.NotaDebitKredit
{
    public class UpdateNotaDebitKredit
    {
        public record Command : IRequest<NotaDebitKreditDTO>
        {
            public Guid Id { get; set; }

            public string? NoNota { get; init; }
            public DateTime? Tarikh { get; init; }
            public string? StatusPos { get; init; }
            public string? StatusSah { get; init; }
            public string? ButiranNota { get; init; }
            public Guid? PenyelenggaraanPenghutangEntitiesID { get; init; }
        }

        public class Handler : IRequestHandler<Command, NotaDebitKreditDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<NotaDebitKreditDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.NotaDebitKreditEntities
                    .FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);

                if (entity == null) return null;

                entity.NoNota = request.NoNota;
                entity.Tarikh = request.Tarikh;
                entity.StatusPos = string.IsNullOrWhiteSpace(request.StatusPos)
                    ? entity.StatusPos
                    : request.StatusPos;
                entity.StatusSah = string.IsNullOrWhiteSpace(request.StatusSah)
                    ? entity.StatusSah
                    : request.StatusSah;
                entity.ButiranNota = request.ButiranNota;
                entity.PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new NotaDebitKreditDTO
                {
                    ID = entity.ID,
                    NoNota = entity.NoNota,
                    Tarikh = entity.Tarikh,
                    StatusPos = entity.StatusPos,
                    StatusSah = entity.StatusSah,
                    ButiranNota = entity.ButiranNota,
                    PenyelenggaraanPenghutangEntitiesID = entity.PenyelenggaraanPenghutangEntitiesID
                };
            }
        }
    }
}
