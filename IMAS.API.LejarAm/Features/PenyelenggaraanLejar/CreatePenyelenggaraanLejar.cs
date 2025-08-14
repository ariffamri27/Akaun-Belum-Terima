using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using PenyelenggaraanLejarEntity = IMAS.API.LejarAm.Shared.Domain.Entities.PenyelenggaraanLejarEntities;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public class CreatePenyelenggaraanLejar
    {
        public record Command : IRequest<PenyelenggaraanLejarDTO>
        {
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

            public async Task<PenyelenggaraanLejarDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new PenyelenggaraanLejarEntity
                {
                    ID = Guid.NewGuid(),
                    KodAkaun = request.KodAkaun,
                    Keterangan = request.Keterangan,
                    Paras = request.Paras,
                    Kategori = request.Kategori,
                    JenisAkaun = request.JenisAkaun,
                    JenisAkaunParas2 = request.JenisAkaunParas2,
                    JenisAliran = request.JenisAliran,
                    JenisKedudukanPenyata = request.JenisKedudukanPenyata,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system" // replace with current user if available
                };

                _context.PenyelenggaraanLejar.Add(entity);
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
