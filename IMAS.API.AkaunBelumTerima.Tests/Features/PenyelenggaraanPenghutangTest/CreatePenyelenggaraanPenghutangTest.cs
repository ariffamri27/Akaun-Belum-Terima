using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using IMAS.API.AkaunBelumTerima.Shared.Domain.Entities;

namespace IMAS.API.AkaunBelumTerima.Features.PenyelenggaraanPenghutang
{
    public class CreatePenyelenggaraanPenghutangTest
    {
        public record Command : IRequest<PenyelenggaraanPenghutangDTO>
        {
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

            public async Task<PenyelenggaraanPenghutangDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new PenyelenggaraanPenghutangEntities
                {
                    ID = Guid.NewGuid(),
                    Kod = request.Kod,
                    KeteranganKod = request.KeteranganKod,
                    Status = string.IsNullOrWhiteSpace(request.Status) ? "Aktif" : request.Status,
                    KodPenghutang = request.KodPenghutang,
                    Nama = request.Nama,
                    NamaKedua = request.NamaKedua,
                    Bank = request.Bank,
                    NoAkaun = request.NoAkaun,
                    TahunKewangan = request.TahunKewangan,
                    TarikhJanaan = request.TarikhJanaan,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                };

                _context.PenyelenggaraanPenghutangEntities.Add(entity);
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
