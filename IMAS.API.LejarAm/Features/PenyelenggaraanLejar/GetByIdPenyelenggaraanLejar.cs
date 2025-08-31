using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public static class GetByIdPenyelenggaraanLejar
    {
        public record Query : IRequest<PenyelenggaraanLejarDTO?> { public Guid Id { get; init; } }

        public sealed class Handler : IRequestHandler<Query, PenyelenggaraanLejarDTO?>
        {
            private readonly FinancialDbContext _db;
            public Handler(FinancialDbContext db) => _db = db;

            public async Task<PenyelenggaraanLejarDTO?> Handle(Query request, CancellationToken ct)
            {
                var e = await _db.PenyelenggaraanLejar
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ID == request.Id, ct);

                return e?.ToDto();
            }
        }
    }
}
