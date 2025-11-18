using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.NotaDebitKredit
{
    public class GetAllNotaDebitKredit
    {
        public record Query : IRequest<List<NotaDebitKreditDTO>>;

        public class Handler : IRequestHandler<Query, List<NotaDebitKreditDTO>>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<List<NotaDebitKreditDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.NotaDebitKreditEntities
                    .Select(n => new NotaDebitKreditDTO
                    {
                        ID = n.ID,
                        NoNota = n.NoNota,
                        Tarikh = n.Tarikh,
                        StatusPos = n.StatusPos,
                        StatusSah = n.StatusSah,
                        ButiranNota = n.ButiranNota,
                        PenyelenggaraanPenghutangEntitiesID = n.PenyelenggaraanPenghutangEntitiesID
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
