using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public static class DeletePenyelenggaraanLejar
    {
        public record Command : IRequest<bool> { public Guid Id { get; init; } }

        public sealed class Handler : IRequestHandler<Command, bool>
        {
            private readonly FinancialDbContext _db;
            public Handler(FinancialDbContext db) => _db = db;

            public async Task<bool> Handle(Command req, CancellationToken ct)
            {
                var entity = await _db.PenyelenggaraanLejar.FirstOrDefaultAsync(x => x.ID == req.Id, ct);
                if (entity is null) return false;

                _db.PenyelenggaraanLejar.Remove(entity);
                await _db.SaveChangesAsync(ct);
                return true;
            }
        }
    }
}
