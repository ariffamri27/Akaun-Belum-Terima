using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using NotaEntity = IMAS.API.AkaunBelumTerima.Shared.Domain.Entities.NotaDebitKreditEntities;

namespace IMAS.API.AkaunBelumTerima.Features.NotaDebitKredit
{
    public class CreateNotaDebitKreditTest
    {
        public record Command : IRequest<NotaDebitKreditDTO>
        {
            public string? NoNota { get; init; }
            public DateTime? Tarikh { get; init; }
            public string? StatusPos { get; init; }
            public string? StatusSah { get; init; }
            public string? ButiranNota { get; init; }
            public Guid? PenyelenggaraanPenghutangEntitiesID { get; init; }
        }

        public class Handler : IRequestHandler<Command, NotaDebitKreditDTO>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<NotaDebitKreditDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new NotaEntity
                {
                    ID = Guid.NewGuid(),
                    NoNota = request.NoNota ?? string.Empty,
                    Tarikh = request.Tarikh,
                    StatusPos = string.IsNullOrWhiteSpace(request.StatusPos) ? "BARU" : request.StatusPos,
                    StatusSah = string.IsNullOrWhiteSpace(request.StatusSah) ? "BELUM SAH" : request.StatusSah,
                    ButiranNota = request.ButiranNota ?? string.Empty,
                    PenyelenggaraanPenghutangEntitiesID = request.PenyelenggaraanPenghutangEntitiesID,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system"
                };

                _context.NotaDebitKreditEntities.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return new NotaDebitKreditDTO
                {
                    ID = entity.ID,
                    NoNota = entity.NoNota,
                    Tarikh = entity.Tarikh,
                    StatusPos = entity.StatusPos,
                    StatusSah = entity.StatusSah,
                    ButiranNota = entity.ButiranNota,
                    PenyelenggaraanPenghutangEntitiesID = entity.PenyelenggaraanPenghutangEntitiesID
                };
            }
        }
    }
}
