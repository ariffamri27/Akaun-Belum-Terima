using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;
using MediatR;

namespace IMAS.API.AkaunBelumTerima.Features.NotaDebitKredit
{
    public class DeleteNotaDebitKredit
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly AkaunBelumTerimaDbContext _context;

            public Handler(AkaunBelumTerimaDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.NotaDebitKreditEntities.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null) return false;

                _context.NotaDebitKreditEntities.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
