using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;

namespace IMAS.API.AkaunBelumTerima.Features.PenyelenggaraanPenghutang
{
    public class GetByIdPenyelenggaraanPenghutangTest
    {
        public record Query : IRequest<PenyelenggaraanPenghutangDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, PenyelenggaraanPenghutangDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<PenyelenggaraanPenghutangDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.PenyelenggaraanPenghutangEntities.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null) return null;

                return new PenyelenggaraanPenghutangDTO
                {
                    ID = entity.ID,
                    Kod = entity.Kod,
                    KeteranganKod = entity.KeteranganKod,
                    Status = entity.Status,
                    KodPenghutang = entity.KodPenghutang,
                    Nama = entity.Nama,
                    NamaKedua = entity.NamaKedua,
                    Bank = entity.Bank,
                    NoAkaun = entity.NoAkaun,
                    TahunKewangan = entity.TahunKewangan,
                    TarikhJanaan = entity.TarikhJanaan
                };
            }
        }
    }
}
