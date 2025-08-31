using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public static class GetAllPenyelenggaraanLejar
    {
        public record Query : IRequest<List<PenyelenggaraanLejarDTO>>;

        public sealed class Handler : IRequestHandler<Query, List<PenyelenggaraanLejarDTO>>
        {
            private readonly FinancialDbContext _db;
            public Handler(FinancialDbContext db) => _db = db;

            public async Task<List<PenyelenggaraanLejarDTO>> Handle(Query request, CancellationToken ct)
            {
                return await _db.PenyelenggaraanLejar
                    .AsNoTracking()
                    .Select(e => e.ToDto())
                    .ToListAsync(ct);
            }
        }
    }

    internal static class PenyelenggaraanLejarMapping
    {
        public static PenyelenggaraanLejarDTO ToDto(this PenyelenggaraanLejarEntities e) => new()
        {
            ID = e.ID,
            KodAkaun = e.KodAkaun ?? string.Empty,
            Keterangan = e.Keterangan,
            Paras = e.Paras ?? 0, // ✅ avoid null
            Kategori = e.Kategori ?? string.Empty,
            JenisAkaun = e.JenisAkaun ?? string.Empty,
            JenisAkaunParas2 = e.JenisAkaunParas2,
            JenisAliran = e.JenisAliran ?? string.Empty,
            JenisKedudukanPenyata = e.JenisKedudukanPenyata ?? string.Empty,
            Tahun = e.Tahun,
            Bulan = e.Bulan,
            Status = e.Status ?? string.Empty,
            TarikhTutup = e.TarikhTutup,
        };
    }
}
