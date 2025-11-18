using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using BillEntity = IMAS.API.AkaunBelumTerima.Shared.Domain.Entities.BillEntities;

namespace IMAS.API.AkaunBelumTerima.Features.Bill;

public class CreateBill
{
    public record Command : IRequest<BillDTO>
    {
        // BILL
        public string? NoBil { get; init; }
        public DateTime? Tarikh { get; init; }
        public string? StatusPos { get; init; }

        // FIXED BILLING
        public string? NoFixedBil { get; init; }
        public DateTime? TarikhMula { get; init; }
        public DateTime? TarikhAkhir { get; init; }
        public string? NoArahanKerja { get; init; }
        public string? StatusJana { get; init; }

        // PENGESAHAN BILL
        public string? Penyedia { get; init; }
        public string? Keterangan { get; init; }
        public decimal? Jumlah { get; init; }
        public string? StatusSah { get; init; }

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; init; }
    }

    public class Handler : IRequestHandler<Command, BillDTO>
    {
        private readonly AkaunBelumTerimaDbContext _context;

        public Handler(AkaunBelumTerimaDbContext context)
        {
            _context = context;
        }

        public async Task<BillDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new BillEntity
            {
                ID = Guid.NewGuid(),

                // BILL
                NoBil = request.NoBil,
                Tarikh = request.Tarikh,
                StatusPos = string.IsNullOrWhiteSpace(request.StatusPos)
                    ? "BELUM POS"
                    : request.StatusPos,

                // FIXED BILLING
                NoFixedBil = request.NoFixedBil,
                TarikhMula = request.TarikhMula,
                TarikhAkhir = request.TarikhAkhir,
                NoArahanKerja = request.NoArahanKerja,
                StatusJana = string.IsNullOrWhiteSpace(request.StatusJana)
                    ? "BELUM JANA"
                    : request.StatusJana,

                // PENGESAHAN BILL
                Penyedia = request.Penyedia,
                Keterangan = request.Keterangan,
                Jumlah = request.Jumlah,
                StatusSah = string.IsNullOrWhiteSpace(request.StatusSah)
                    ? "BELUM SAH"
                    : request.StatusSah,

                PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            _context.BillEntities.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

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
