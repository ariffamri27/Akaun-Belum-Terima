using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using ResitEntity = IMAS.API.AkaunBelumTerima.Shared.Domain.Entities.ResitEntities;

namespace IMAS.API.AkaunBelumTerima.Features.Resit
{
    public class CreateResit
    {
        public record Command : IRequest<ResitDTO>
        {
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

            public async Task<ResitDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new ResitEntity
                {
                    ID = Guid.NewGuid(),
                    NoResit = request.NoResit ?? string.Empty,
                    NoBankSlip = request.NoBankSlip ?? string.Empty,
                    Tarikh = request.Tarikh,
                    StatusPos = string.IsNullOrWhiteSpace(request.StatusPos) ? "BARU" : request.StatusPos,
                    StatusSah = string.IsNullOrWhiteSpace(request.StatusSah) ? "BELUM SAH" : request.StatusSah,
                    Jumlah = request.Jumlah,
                    Butiran = request.Butiran ?? string.Empty,
                    PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                };

                _context.ResitEntities.Add(entity);
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
