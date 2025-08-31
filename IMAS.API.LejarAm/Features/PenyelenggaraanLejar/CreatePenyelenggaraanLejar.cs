using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public static class CreatePenyelenggaraanLejar
    {
        public record Command : IRequest<PenyelenggaraanLejarDTO>
        {
            public string KodAkaun { get; init; } = string.Empty;
            public string? Keterangan { get; init; }
            public int? Paras { get; init; }
            public string? Kategori { get; init; } = string.Empty;
            public string? JenisAkaun { get; init; } = string.Empty;
            public string? JenisAkaunParas2 { get; init; }
            public string? JenisAliran { get; init; } = string.Empty;
            public string? JenisKedudukanPenyata { get; init; } = string.Empty;
            public int? Tahun { get; set; }
            public int? Bulan { get; set; }
            public string? Status { get; set; }
            public DateTime? TarikhTutup { get; set; }
        }

        public sealed class Handler : IRequestHandler<Command, PenyelenggaraanLejarDTO>
        {
            private readonly FinancialDbContext _db;
            public Handler(FinancialDbContext db) => _db = db;

            public async Task<PenyelenggaraanLejarDTO> Handle(Command req, CancellationToken ct)
            {
                var entity = new PenyelenggaraanLejarEntities
                {
                    ID = Guid.NewGuid(),
                    KodAkaun = req.KodAkaun,
                    Keterangan = req.Keterangan,
                    Paras = req.Paras, // maps to int?
                    Kategori = req.Kategori,
                    JenisAkaun = req.JenisAkaun,
                    JenisAkaunParas2 = req.JenisAkaunParas2,
                    JenisAliran = req.JenisAliran,
                    JenisKedudukanPenyata = req.JenisKedudukanPenyata,
                    Tahun = req.Tahun,
                    Bulan = req.Bulan,
                    Status = req.Status,
                    TarikhTutup = req.TarikhTutup,
                };

                _db.PenyelenggaraanLejar.Add(entity);
                await _db.SaveChangesAsync(ct);

                // re-read to be safe with triggers/defaults
                var saved = await _db.PenyelenggaraanLejar
                    .AsNoTracking()
                    .FirstAsync(x => x.ID == entity.ID, ct);

                return saved.ToDto();
            }
        }
    }
}
