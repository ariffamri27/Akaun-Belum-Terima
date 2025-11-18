using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.Bill;

public class UpdateBill
{
    public record Command : IRequest<BillDTO>
    {
        public Guid Id { get; set; }

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

        public async Task<BillDTO?> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _context.BillEntities.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
            if (entity == null) return null;

            // BILL
            entity.NoBil = request.NoBil;
            entity.Tarikh = request.Tarikh;
            entity.StatusPos = string.IsNullOrWhiteSpace(request.StatusPos)
                ? entity.StatusPos
                : request.StatusPos;

            // FIXED BILLING
            entity.NoFixedBil = request.NoFixedBil;
            entity.TarikhMula = request.TarikhMula;
            entity.TarikhAkhir = request.TarikhAkhir;
            entity.NoArahanKerja = request.NoArahanKerja;
            entity.StatusJana = string.IsNullOrWhiteSpace(request.StatusJana)
                ? entity.StatusJana
                : request.StatusJana;

            // PENGESAHAN BILL
            entity.Penyedia = request.Penyedia;
            entity.Keterangan = request.Keterangan;
            entity.Jumlah = request.Jumlah;
            entity.StatusSah = string.IsNullOrWhiteSpace(request.StatusSah)
                ? entity.StatusSah
                : request.StatusSah;

            entity.PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "system";

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
