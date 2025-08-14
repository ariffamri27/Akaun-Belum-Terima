using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public class GetAllPenyelenggaraanLejar
    {
        public record Query : IRequest<List<PenyelenggaraanLejarDTO>>;

        public class Handler : IRequestHandler<Query, List<PenyelenggaraanLejarDTO>>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<List<PenyelenggaraanLejarDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.PenyelenggaraanLejar
                    .Select(pl => new PenyelenggaraanLejarDTO
                    {
                        ID = pl.ID,
                        KodAkaun = pl.KodAkaun,
                        Keterangan = pl.Keterangan,
                        Paras = pl.Paras,
                        Kategori = pl.Kategori,
                        JenisAkaun = pl.JenisAkaun,
                        JenisAkaunParas2 = pl.JenisAkaunParas2,
                        JenisAliran = pl.JenisAliran,
                        JenisKedudukanPenyata = pl.JenisKedudukanPenyata
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
