using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.Bill;

public class GetAllBillTest
{
    public record Query : IRequest<List<BillDTO>>;

    public class Handler : IRequestHandler<Query, List<BillDTO>>
    {
        private readonly AkaunBelumTerimaDbContext _context;

        public Handler(AkaunBelumTerimaDbContext context)
        {
            _context = context;
        }

        public async Task<List<BillDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.BillEntities
                .Select(b => new BillDTO
                {
                    ID = b.ID,
                    NoBil = b.NoBil,
                    Tarikh = b.Tarikh,
                    StatusPos = b.StatusPos,
                    NoFixedBil = b.NoFixedBil,
                    TarikhMula = b.TarikhMula,
                    TarikhAkhir = b.TarikhAkhir,
                    NoArahanKerja = b.NoArahanKerja,
                    StatusJana = b.StatusJana,
                    Penyedia = b.Penyedia,
                    Keterangan = b.Keterangan,
                    Jumlah = b.Jumlah,
                    StatusSah = b.StatusSah,
                    PenyelenggaraanPenghutangEntitiesID = b.PenyelenggaraanPenghutangEntitiesID
                })
                .ToListAsync(cancellationToken);
        }
    }
}
