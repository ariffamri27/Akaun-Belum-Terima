using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.Resit
{
    public class GetAllResit
    {
        public record Query : IRequest<List<ResitDTO>>;

        public class Handler : IRequestHandler<Query, List<ResitDTO>>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<List<ResitDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ResitEntities
                    .Select(r => new ResitDTO
                    {
                        ID = r.ID,
                        NoResit = r.NoResit,
                        NoBankSlip = r.NoBankSlip,
                        Tarikh = r.Tarikh,
                        StatusPos = r.StatusPos,
                        StatusSah = r.StatusSah,
                        Jumlah = r.Jumlah,
                        Butiran = r.Butiran,
                        PenyelenggaraanPenghutangEntitiesID = r.PenyelenggaraanPenghutangEntitiesID
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
