using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;

namespace IMAS.API.AkaunBelumTerima.Features.Resit
{
    public class GetByIdResitTest
    {
        public record Query : IRequest<ResitDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, ResitDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<ResitDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.ResitEntities.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null) return null;

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
