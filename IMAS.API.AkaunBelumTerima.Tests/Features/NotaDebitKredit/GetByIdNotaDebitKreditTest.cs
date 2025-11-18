using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;

namespace IMAS.API.AkaunBelumTerima.Features.NotaDebitKredit
{
    public class GetByIdNotaDebitKreditTest
    {
        public record Query : IRequest<NotaDebitKreditDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, NotaDebitKreditDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<NotaDebitKreditDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.NotaDebitKreditEntities.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null) return null;

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
