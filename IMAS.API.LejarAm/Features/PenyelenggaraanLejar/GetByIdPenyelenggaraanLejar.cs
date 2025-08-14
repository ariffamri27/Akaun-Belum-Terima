using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public class GetByIdPenyelenggaraanLejar
    {
        public record Query : IRequest<PenyelenggaraanLejarDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, PenyelenggaraanLejarDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<PenyelenggaraanLejarDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.PenyelenggaraanLejar.FindAsync(new object[] { request.Id }, cancellationToken);
                return entity == null ? null : new PenyelenggaraanLejarDTO
                {
                    ID = entity.ID,
                    KodAkaun = entity.KodAkaun,
                    Keterangan = entity.Keterangan,
                    Paras = entity.Paras,
                    Kategori = entity.Kategori,
                    JenisAkaun = entity.JenisAkaun,
                    JenisAkaunParas2 = entity.JenisAkaunParas2,
                    JenisAliran = entity.JenisAliran,
                    JenisKedudukanPenyata = entity.JenisKedudukanPenyata
                };
            }
        }
    }
}
