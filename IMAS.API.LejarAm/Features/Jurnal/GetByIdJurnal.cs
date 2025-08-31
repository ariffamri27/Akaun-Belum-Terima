using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.Jurnal
{
    public class GetByIdJurnal
    {
        public record Query : IRequest<JurnalDTO>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, JurnalDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<JurnalDTO?> Handle(Query request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jurnal.FindAsync(new object[] { request.Id }, cancellationToken);
                return entity == null ? null : new JurnalDTO
                {
                    ID = entity.ID,
                    NoJurnal = entity.NoJurnal,
                    NoRujukan = entity.NoRujukan,
                    TarikhJurnal = entity.TarikhJurnal,
                    StatusPos = entity.StatusPos,
                    StatusSemak = entity.StatusSemak,
                    StatusSah = entity.StatusSah,
                    JenisJurnal = entity.JenisJurnal,
                    SumberTransaksi = entity.SumberTransaksi,
                    Keterangan = entity.Keterangan,
                };
            }
        }
    }
}
