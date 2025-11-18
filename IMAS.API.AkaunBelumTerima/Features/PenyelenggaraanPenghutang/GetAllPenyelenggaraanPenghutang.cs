using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.PenyelenggaraanPenghutang
{
    public class GetAllPenyelenggaraanPenghutang
    {
        public record Query : IRequest<List<PenyelenggaraanPenghutangDTO>>;

        public class Handler : IRequestHandler<Query, List<PenyelenggaraanPenghutangDTO>>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<List<PenyelenggaraanPenghutangDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.PenyelenggaraanPenghutangEntities
                    .Select(p => new PenyelenggaraanPenghutangDTO
                    {
                        ID = p.ID,
                        Kod = p.Kod,
                        KeteranganKod = p.KeteranganKod,
                        Status = p.Status,
                        KodPenghutang = p.KodPenghutang,
                        Nama = p.Nama,
                        NamaKedua = p.NamaKedua,
                        Bank = p.Bank,
                        NoAkaun = p.NoAkaun,
                        TahunKewangan = p.TahunKewangan,
                        TarikhJanaan = p.TarikhJanaan
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
