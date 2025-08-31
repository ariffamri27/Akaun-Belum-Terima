using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.Jurnal
{
    public class GetAllJurnal
    {
        public record Query : IRequest<List<JurnalDTO>>;

        public class Handler : IRequestHandler<Query, List<JurnalDTO>>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<List<JurnalDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Jurnal
                    .Select(j => new JurnalDTO
                    {
                        ID = j.ID,
                        NoJurnal = j.NoJurnal,
                        NoRujukan = j.NoRujukan,
                        TarikhJurnal = j.TarikhJurnal,
                        StatusPos = j.StatusPos,
                        StatusSemak = j.StatusSemak,
                        StatusSah = j.StatusSah,
                        JenisJurnal = j.JenisJurnal,
                        SumberTransaksi = j.SumberTransaksi,
                        Keterangan = j.Keterangan,
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
