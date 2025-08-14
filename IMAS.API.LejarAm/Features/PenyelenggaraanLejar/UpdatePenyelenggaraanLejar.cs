using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public class UpdatePenyelenggaraanLejar
    {
        public record Command : IRequest<PenyelenggaraanLejarDTO>
        {
            public Guid Id { get; set; }
            public string KodAkaun { get; init; } = default!;
            public string? Keterangan { get; init; }
            public int Paras { get; init; }
            public string Kategori { get; init; } = default!;
            public string JenisAkaun { get; init; } = default!;
            public string? JenisAkaunParas2 { get; init; }
            public string JenisAliran { get; init; } = default!;
            public string JenisKedudukanPenyata { get; init; } = default!;
        }

        public class Handler : IRequestHandler<Command, PenyelenggaraanLejarDTO>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<PenyelenggaraanLejarDTO?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.PenyelenggaraanLejar.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
                if (entity == null) return null;

                entity.KodAkaun = request.KodAkaun;
                entity.Keterangan = request.Keterangan;
                entity.Paras = request.Paras;
                entity.Kategori = request.Kategori;
                entity.JenisAkaun = request.JenisAkaun;
                entity.JenisAkaunParas2 = request.JenisAkaunParas2;
                entity.JenisAliran = request.JenisAliran;
                entity.JenisKedudukanPenyata = request.JenisKedudukanPenyata;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "system";

                await _context.SaveChangesAsync(cancellationToken);

                return new PenyelenggaraanLejarDTO
                {
                    ID = entity.ID,
                    KodAkaun = entity.KodAkaun,
                    Keterangan = entity.Keterangan,
                    Paras = entity.Paras,
                    Kategori = entity.Kategori,
                    JenisAkaun = entity.JenisAkaun,
                    JenisAkaunParas2 = entity.JenisAkaunParas2,
                    JenisAliran = entity.JenisAliran,
                    JenisKedudukanPenyata = entity.JenisKedudukanPenyata
                };
            }
        }
    }
}
