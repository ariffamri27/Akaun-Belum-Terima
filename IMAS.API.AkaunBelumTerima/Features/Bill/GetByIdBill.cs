using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;

namespace IMAS.API.AkaunBelumTerima.Features.Bill;

public class GetByIdBill
{
    public record Query : IRequest<BillDTO>
    {
        public Guid Id { get; init; }
    }

    public class Handler : IRequestHandler<Query, BillDTO>
    {
        private readonly AkaunBelumTerimaDbContext _context;

        public Handler(AkaunBelumTerimaDbContext context)
        {
            _context = context;
        }

        public async Task<BillDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _context.BillEntities.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) return null;

            return new BillDTO
            {
                ID = entity.ID,
                NoBil = entity.NoBil,
                Tarikh = entity.Tarikh,
                StatusPos = entity.StatusPos,
                NoFixedBil = entity.NoFixedBil,
                TarikhMula = entity.TarikhMula,
                TarikhAkhir = entity.TarikhAkhir,
                NoArahanKerja = entity.NoArahanKerja,
                StatusJana = entity.StatusJana,
                Penyedia = entity.Penyedia,
                Keterangan = entity.Keterangan,
                Jumlah = entity.Jumlah,
                StatusSah = entity.StatusSah,
                PenyelenggaraanPenghutangEntitiesID = entity.PenyelenggaraanPenghutangEntitiesID
            };
        }
    }
}
