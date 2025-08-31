using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using JurnalEntity = IMAS.API.LejarAm.Shared.Domain.Entities.JurnalEntities;

namespace IMAS.API.LejarAm.Features.Jurnal
{
    public class CreateJurnal
    {
        public record Command : IRequest<JurnalDTO>
        {
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

            public async Task<JurnalDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new JurnalEntity
                {
                    ID = Guid.NewGuid(),
                    NoJurnal = request.NoJurnal,
                    NoRujukan = request.NoRujukan,
                    TarikhJurnal = request.TarikhJurnal,
                    StatusPos = request.StatusPos,
                    StatusSemak = request.StatusSemak,
                    StatusSah = request.StatusSah,
                    JenisJurnal = request.JenisJurnal,
                    SumberTransaksi = request.SumberTransaksi,
                    Keterangan = request.Keterangan,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system" // replace with current user if available
                };

                _context.Jurnal.Add(entity);
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
