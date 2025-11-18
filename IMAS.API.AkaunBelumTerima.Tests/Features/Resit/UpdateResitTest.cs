using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.Resit
{
    public class UpdateResitTest
    {
        public record Command : IRequest<ResitDTO>
        {
            public Guid Id { get; set; }

            public string? NoResit { get; init; }
            public string? NoBankSlip { get; init; }
            public DateTime? Tarikh { get; init; }
            public string? StatusPos { get; init; }
            public string? StatusSah { get; init; }
            public decimal? Jumlah { get; init; }
            public string? Butiran { get; init; }
            public Guid? PenyelenggaraanPenghutangEntitiesID { get; init; }
        }

        public class Handler : IRequestHandler<Command, ResitDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<ResitDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.ResitEntities
                    .FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);

                if (entity == null) return null;

                entity.NoResit = request.NoResit ?? entity.NoResit;
                entity.NoBankSlip = request.NoBankSlip ?? entity.NoBankSlip;
                entity.Tarikh = request.Tarikh;
                entity.StatusPos = string.IsNullOrWhiteSpace(request.StatusPos)
                    ? entity.StatusPos
                    : request.StatusPos;
                entity.StatusSah = string.IsNullOrWhiteSpace(request.StatusSah)
                    ? entity.StatusSah
                    : request.StatusSah;
                entity.Jumlah = request.Jumlah;
                entity.Butiran = request.Butiran ?? entity.Butiran;
                entity.PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new ResitDTO
                {
                    ID = entity.ID,
                    NoResit = entity.NoResit,
                    NoBankSlip = entity.NoBankSlip,
                    Tarikh = entity.Tarikh,
                    StatusPos = entity.StatusPos,
                    StatusSah = entity.StatusSah,
                    Jumlah = entity.Jumlah,
                    Butiran = entity.Butiran,
                    PenyelenggaraanPenghutangEntitiesID = entity.PenyelenggaraanPenghutangEntitiesID
                };
            }
        }
    }
}
