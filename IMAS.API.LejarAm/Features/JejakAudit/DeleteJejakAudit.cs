using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.LejarAm.Features.JejakAudit
{
    public class DeleteJejakAudit
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly FinancialDbContext _context;

            public Handler(FinancialDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.JejakAudit.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null) return false;

                _context.JejakAudit.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
