using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Features.PenyelenggaraanPenghutang
{
    public class UpdatePenyelenggaraanPenghutang
    {
        public record Command : IRequest<PenyelenggaraanPenghutangDTO>
        {
            public Guid Id { get; set; }

            public string Kod { get; init; } = default!;
            public string? KeteranganKod { get; init; }
            public string? Status { get; init; }
            public string? KodPenghutang { get; init; }
            public string? Nama { get; init; }
            public string? NamaKedua { get; init; }
            public string? Bank { get; init; }
            public string? NoAkaun { get; init; }
            public int? TahunKewangan { get; init; }
            public DateTime? TarikhJanaan { get; init; }
        }

        public class Handler : IRequestHandler<Command, PenyelenggaraanPenghutangDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<PenyelenggaraanPenghutangDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.PenyelenggaraanPenghutangEntities
                    .FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);

                if (entity == null) return null;

                entity.Kod = request.Kod;
                entity.KeteranganKod = request.KeteranganKod;
                entity.Status = string.IsNullOrWhiteSpace(request.Status)
                    ? entity.Status
                    : request.Status;
                entity.KodPenghutang = request.KodPenghutang;
                entity.Nama = request.Nama;
                entity.NamaKedua = request.NamaKedua;
                entity.Bank = request.Bank;
                entity.NoAkaun = request.NoAkaun;
                entity.TahunKewangan = request.TahunKewangan;
                entity.TarikhJanaan = request.TarikhJanaan;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new PenyelenggaraanPenghutangDTO
                {
                    ID = entity.ID,
                    Kod = entity.Kod,
                    KeteranganKod = entity.KeteranganKod,
                    Status = entity.Status,
                    KodPenghutang = entity.KodPenghutang,
                    Nama = entity.Nama,
                    NamaKedua = entity.NamaKedua,
                    Bank = entity.Bank,
                    NoAkaun = entity.NoAkaun,
                    TahunKewangan = entity.TahunKewangan,
                    TarikhJanaan = entity.TarikhJanaan
                };
            }
        }
    }
}
