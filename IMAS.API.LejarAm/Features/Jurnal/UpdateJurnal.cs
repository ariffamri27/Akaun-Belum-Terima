using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.Jurnal
{
    public class UpdateJurnal
    {
        public record Command : IRequest<JurnalDTO>
        {
            public Guid Id { get; set; }
            public string NoJurnal { get; init; } = default!;
            public string? NoRujukan { get; init; }
            public DateTime TarikhJurnal { get; init; }
            public string StatusPos { get; set; } = "BELUM POS";
            public string StatusSemak { get; set; } = "BELUM SEMAK";
            public string StatusSah { get; set; } = "BELUM SAH"; 
            public string JenisJurnal { get; init; } = "MANUAL";
            public string SumberTransaksi { get; init; } = "GENERAL LEDGER";
            public string Keterangan { get; init; } = default!;
            public Guid? PegawaiPengesahanID { get; init; }
        }

        public class Handler : IRequestHandler<Command, JurnalDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<JurnalDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jurnal.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
                if (entity == null) return null;

                entity.NoJurnal = request.NoJurnal;
                entity.NoRujukan = request.NoRujukan;
                entity.TarikhJurnal = request.TarikhJurnal;
                entity.StatusPos = request.StatusPos;
                entity.StatusSemak = request.StatusSemak;
                entity.StatusSah = request.StatusSah;
                entity.JenisJurnal = request.JenisJurnal;
                entity.SumberTransaksi = request.SumberTransaksi;
                entity.Keterangan = request.Keterangan;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new JurnalDTO
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
